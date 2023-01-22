using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosGraph
{
    public static class Seed
    {
        public static IList<string> Initialise()
        {
            return new List<string>
            { 
                "g.V().drop()"
            };
        }

        public static IList<string> AddPerson()
        {
            return new List<string>
            {
                "g.addV('person').property('id', 'thomas').property('firstName', 'Thomas').property('lastName', 'T').property('email', 'Thomas.T@gmail.com').property('JobTitle', 'Software Engineer').property('tenant', 'test')",
                "g.addV('person').property('id', 'mary').property('firstName', 'Mary').property('lastName', 'M').property('email', 'Mary.M@outlook.com').property('JobTitle', 'Auditor').property('tenant', 'test')",
                "g.addV('person').property('id', 'john').property('firstName', 'John').property('lastName', 'J').property('email', 'John.J@outlook.com').property('JobTitle', 'DevOps Engineer').property('tenant', 'test')",
                "g.addV('person').property('id', 'smith').property('firstName', 'Smith').property('lastName', 'S').property('email', 'Smith.S@outlook.com').property('JobTitle', 'DevOps Engineer').property('tenant', 'test')",
                "g.addV('person').property('id', 'ben').property('firstName', 'Ben').property('lastName', 'B').property('email', 'Ben.B@outlook.com').property('JobTitle', 'Auditor').property('tenant', 'test')",
                "g.addV('person').property('id', 'daisy').property('firstName', 'Daisy').property('lastName', 'D').property('email', 'Daisy.D@outlook.com').property('JobTitle', 'Software Engineer').property('tenant', 'test')",
            };
        }

        public static IList<string> AddGroup()
        {
            return new List<string>
            {
                "g.addV('group').property('id', 'director').property('title', 'Director').property('Grade', 'A').property('experience', '10').property('tenant', 'test')",
                "g.addV('group').property('id', 'seniorManager').property('title', 'Senior Manager').property('Grade', 'B').property('experience', '8').property('tenant', 'test')",
                "g.addV('group').property('id', 'manager').property('title', 'Manager').property('Grade', 'C').property('experience', '5').property('tenant', 'test')",
                "g.addV('group').property('id', 'assistantManager').property('title', 'Assistant Manager').property('Grade', 'D').property('experience', '3').property('tenant', 'test')",
            };
        }

        public static IList<string> AddCourses()
        {
            return new List<string>
            {
                $"g.addV('course').property('id', 'itw').property('title', 'Introduction to Word').property('Summary', 'Summary for Introduction to Word').property('publishedDate', '{DateTime.UtcNow.AddDays(-1)}').property('price', '10').property('tenant', 'test')",
                $"g.addV('course').property('id', 'efe').property('title', 'Excel for experts').property('Summary', 'Summary for Excel for experts').property('publishedDate', '{DateTime.UtcNow.AddDays(-2)}').property('price', '40').property('tenant', 'test')",
                $"g.addV('course').property('id', 'cc').property('title', 'Cloud Computing').property('Summary', 'Summary for Cloud Computing').property('publishedDate', '{DateTime.UtcNow.AddDays(-3)}').property('price', '50').property('tenant', 'test')",
                $"g.addV('course').property('id', 'itc').property('title', 'Introduction to C#').property('Summary', 'Summary for Introduction to C#').property('publishedDate', '{DateTime.UtcNow.AddDays(-4)}').property('price', '60').property('tenant', 'test')",
                $"g.addV('course').property('id', 'jfd').property('title', 'Java for dummies').property('Summary', 'Summary for Java for dummies').property('publishedDate', '{DateTime.UtcNow.AddDays(-5)}').property('price', '55').property('tenant', 'test')",
                $"g.addV('course').property('id', 'top').property('title', 'Theory of programming').property('Summary', 'Summary for Theory of programming').property('publishedDate', '{DateTime.UtcNow.AddDays(-6)}').property('price', '20').property('tenant', 'test')",
                $"g.addV('course').property('id', 'jur').property('title', 'Javascript using React').property('Summary', 'Summary for Javascript using React').property('publishedDate', '{DateTime.UtcNow.AddDays(-7)}').property('price', '40').property('tenant', 'test')",
            };
        }

        public static IList<string> AssignPersonToGroups()
        {
            return new List<string>
            {
                "g.V('director').addE('enrolled').to(g.V('ben'))",
                "g.V('seniorManager').addE('enrolled').to(g.V('mary'))",
                "g.V('manager').addE('enrolled').to(g.V('thomas'))",
                "g.V('manager').addE('enrolled').to(g.V('daisy'))",
                "g.V('assistantManager').addE('enrolled').to(g.V('smith'))",
                "g.V('assistantManager').addE('enrolled').to(g.V('john'))",
            };
        }

        public static IList<string> EnrolPersonToCourses()
        {
            return new List<string>
            {
                "g.V('thomas').addE('member').to(g.V('itc'))",
                "g.V('thomas').addE('enrolled').to(g.V('jfd'))",
                "g.V('thomas').addE('enrolled').to(g.V('top'))",
                "g.V('mary').addE('enrolled').to(g.V('itw'))",
                "g.V('mary').addE('enrolled').to(g.V('efe'))",
                "g.V('john').addE('enrolled').to(g.V('cc'))",
                "g.V('smith').addE('enrolled').to(g.V('cc'))",
                "g.V('ben').addE('enrolled').to(g.V('top'))",
                "g.V('daisy').addE('enrolled').to(g.V('efe'))",
            };
        }

    }
}
