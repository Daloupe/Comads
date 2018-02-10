using System;
using System.Linq;
using System.Collections;
using EfficientlyLazy.IdentityGenerator.Entity;
using static Comads.FuncExt;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Attributes.Jobs;
using System.Diagnostics;

namespace Comads
{
    class Program
    {
        static readonly IMongoCollection<Person> coll = MongoExtensions.GetCollection<Person>();

        static readonly string[] list = new string[] { "CRT" };

        static readonly string flat = string.Join("|", list);

        static readonly Regex regex = new Regex($"^({flat})_.*");


        static void Main(string[] args)
        {

            //var list = new string[] { "CRT" };

            //var output = MongoExtensions
            //    .GetCollection<Person>()
            //    .ContainsList(list)
            //    .Project(n => new { n.FirstName, n.LastName })
            //    .ToList();


            //var smth = output
            //    .Select(n => (n.FirstName, n.LastName))
            //    .ToArray();


           // var tester = new Tester();

            var sw = Stopwatch.StartNew();
            var output = coll
               .ContainsList(list)
               .Project(n => new { n.FirstName, n.LastName })
               .SortBy(n => n.FirstName)
               .ThenBy(n => n.LastName)
               .ToList()
               .Select(n => (n.FirstName, n.LastName))
               .ToArray();

            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds) ;
            Console.Read();
            Console.WriteLine(sw.ElapsedMilliseconds);
            //  var results = BenchmarkRunner.Run<Tester>();

            //var smth = new[] { 43, 335, 34 }




            //await MongoExtensions.GetCollection<Person>().InsertManyAsync(PersonGenerator.GenerateFamilies(250000, 4));



            // .Concat(PersonGenerator.GenerateFamilies(8, 3))
            // .Select(n => n.Return().
            //.Then(y => (y.FirstName, y.Address)));

            //var a = people.ElementAt(0);
            //a.Address = null;
            //var b = people.ElementAt(1);



            //  //var peoples = people.AsReadable().ReadAllProps().ToArray();
            //  PipelineDefinition<Person, Person> pipeline = new BsonDocument[]
            //  {
            //      new BsonDocument { { "$match", new BsonDocument("HeadAncestorId", "school-id.vic.edu") } },
            //      new BsonDocument { { "$sort", new BsonDocument("FirstName", 1) } },
            //      new BsonDocument { { "$project", new BsonDocument("FirstName",1) } }
            //  };

            //  PipelineDefinition<Person, Person> projectpipeline = new BsonDocument[]
            //{
            //      new BsonDocument { { "$project", new BsonDocument("FirstName",1) } }
            //      //new BsonDocument { { "$sort", new BsonDocument("FirstName", 1) } }
            //};

            //var aggFacet = AggregateFacet.Create("Name", pipeline);
            //var aggProjectFacet = AggregateFacet.Create("Project", projectpipeline);

            //var agg = MongoExtensions.GetCollection<Person>().Aggregate(new AggregateOptions
            //{
            //    TranslationOptions = new ExpressionTranslationOptions
            //    {
            //        StringTranslationMode = AggregateStringTranslationMode.CodePoints
            //    },
            //    AllowDiskUse = true,
            //    BypassDocumentValidation = true
            //});

            //async Task<IEnumerable<(string, string)>> Project(IAggregateFluent<Person> agg)
            //{
            //    var projection = await agg.Project(n => new { n.FirstName, n.LastName }).OutAsync("asd");
            //    return projection.ToEnumerable().Select(n => (n.FirstName, n.LastName));
            //}


            //var output = await Project(MongoExtensions
            //    .GetCollection<Person>()
            //    .Aggregate());

            //var output = await agg.Facet(aggFacet, aggProjectFacet).Out("People").ToListAsync();


        }

       


        //static Expression<Func<Person, Thigny>> exp = (person) => new Thigny { F = person.FirstName, L = person.LastName };
    }

    [SimpleJob(launchCount: 1, warmupCount: 2, targetCount: 3)]
    public class Tester
    {
        //[Benchmark]
        //public static async Task<object> Test1()
        //{

        //    var list = new string[] { "CRT" };

        //    var output = await MongoExtensions
        //        .GetCollection<Person>()
        //        .AsQueryable()
        //        .Iter(list)
        //        .Select(n => new { n.FirstName, n.LastName })
        //        .ToListAsync();


        //    var smth = output
        //        .Select(n => (n.FirstName, n.LastName))
        //        .ToArray();

        //    return smth;
        //}

        //[Benchmark]
        //public static object Test1()
        //{

        //    var list = new string[] { "CRT" };

        //    var output = MongoExtensions
        //        .GetCollection<Person>()
        //        .AsQueryable()
        //        .Iter(list)
        //        .Select(n => new { n.FirstName, n.LastName })
        //        .ToList();


        //    var smth = output
        //        .Select(n => (n.FirstName, n.LastName))
        //        .ToArray();

        //    return smth;
        //}

        static readonly IMongoCollection<Person> coll = MongoExtensions
            .GetCollection<Person>();

        static readonly string[] list = new string[] { "CRT" };

        static readonly string flat = string.Join("|", list);
        //var regex = new Regex($"^({flat})_.*");

        static readonly Regex regex = new Regex($"^(?!{flat})");


        [Benchmark]
        public static List<string> Test2()
        {
            var output = coll
                //.ContainsList(list)
                .Find(n => regex.IsMatch(n.FirstName))
                .Project(n =>n.FirstName)
                .ToList();

            return output;
        }
    }
    public static class MongoExtensions
    {

        public static IFindFluent<Person, Person> ContainsList(this IMongoCollection<Person> source, string[] list)
        {
            var flat = string.Join("|", list);
            //var regex = new Regex($"^({flat})_.*");

            var regex = new Regex($"^(?!{flat})");

            return source.Find(n => regex.IsMatch(n.FirstName));
        }


        public static IMongoQueryable<Person> Iter(this IMongoQueryable<Person> source, string[] list)
        {
            var flat = string.Join("|", list);
            var regex = new Regex($"^({flat})_.*");

            return source.Where(n => !regex.IsMatch(n.FirstName));
        }


        public static IMongoCollection<TModel> GetCollection<TModel>
        (
            string connectionString = "mongodb://localhost:27017",
            string databaseName = "comads",
            string collectionName = null
        )
        => new MongoDB.Driver.MongoClient
        (
            connectionString: connectionString
        )
        .GetDatabase
        (
            name: databaseName
        )
        .GetCollection<TModel>
        (
            name: collectionName ?? typeof(TModel).ToCamelCase()
        );
    }

    public static class TypeExtensions
    {
        public static string ToCamelCase(this Type obj)
            => obj.Name.FirstToLower();
    }

    public static class StringExtensions
    {
        public static string FirstToLower(this string obj)
        {
            if (string.IsNullOrEmpty(obj))
                return obj;

            if (char.IsLower(obj, 0))
                return obj;

            return char.ToLowerInvariant(obj[0]) + obj.Substring(1);
        }
    }


}
