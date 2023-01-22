// See https://aka.ms/new-console-template for more information
using System.Net.WebSockets;
using CosmosGraph;
using Gremlin.Net.Driver;
using Gremlin.Net.Driver.Exceptions;
using Gremlin.Net.Structure.IO.GraphSON;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;


var configuration = new ConfigurationBuilder()
     .AddJsonFile($"appsettings.json")
     .AddEnvironmentVariables()
     .Build();

string Host = configuration["Host"];
string PrimaryKey = configuration["PrimaryKey"];
string Database = configuration["Database"];
string Container = configuration["Container"];
int.TryParse(configuration["Port"], out int Port);
bool.TryParse(configuration["EnableSSL"], out bool EnableSSL);
string containerLink = $"/dbs/{Database}/colls/{Container}";

var connectionString = configuration["ConnectionString"];

var gremlinServer = new GremlinServer(Host, Port, enableSsl: EnableSSL,
                                                   username: containerLink,
                                                   password: PrimaryKey);
ConnectionPoolSettings connectionPoolSettings = new ConnectionPoolSettings()
{
    MaxInProcessPerConnection = 10,
    PoolSize = 30,
    ReconnectionAttempts = 3,
    ReconnectionBaseDelay = TimeSpan.FromMilliseconds(500)
};

var webSocketConfiguration =
    new Action<ClientWebSocketOptions>(options =>
    {
        options.KeepAliveInterval = TimeSpan.FromSeconds(10);
    });

using (var gremlinClient = new GremlinClient(
    gremlinServer,
    new GraphSON2Reader(),
    new GraphSON2Writer(),
    "application/vnd.gremlin-v2.0+json",
    connectionPoolSettings,
    webSocketConfiguration))
{
    RunQuery(gremlinClient, Seed.Initialise());
    RunQuery(gremlinClient, Seed.AddPerson());
    RunQuery(gremlinClient, Seed.AddGroup());
    RunQuery(gremlinClient, Seed.AddCourses());
    RunQuery(gremlinClient, Seed.EnrolPersonToCourses());
    RunQuery(gremlinClient, Seed.AssignPersonToGroups());
}

void RunQuery(GremlinClient gremlinClient, IList<string> query)
{
    // Create async task to execute the Gremlin query.

    foreach (var q in query)
    {
        Console.WriteLine(String.Format("Running this query: {0}", q));
        var resultSet = SubmitRequest(gremlinClient, q).Result;
        if (resultSet.Count > 0)
        {
            Console.WriteLine("\tResult:");
            foreach (var result in resultSet)
            {
                // The vertex results are formed as Dictionaries with a nested dictionary for their properties
                string output = JsonConvert.SerializeObject(result);
                Console.WriteLine($"\t{output}");
            }
            Console.WriteLine();
        } 
    }

}

Task<ResultSet<dynamic>> SubmitRequest(GremlinClient gremlinClient, string query)
{
    try
    {
        return gremlinClient.SubmitAsync<dynamic>(query);
    }
    catch (ResponseException e)
    {
        // Print the Gremlin status code.
        Console.WriteLine($"\tError StatusCode: {e.StatusCode}");

        throw;
    }
}