using System;
using System.Linq;
using System.Collections;
using EfficientlyLazy.IdentityGenerator.Entity;
using static Comads.FuncExt;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Linq;
namespace Comads
{
    class Program
    {
        static void Main(string[] args)
        {

            //var smth = new[] { 43, 335, 34 }

            // var people = PersonGenerator.GenerateFamilies(7, 4)

            // .Concat(PersonGenerator.GenerateFamilies(8, 3))
            // .Select(n => n.Return().
            //.Then(y => (y.FirstName, y.Address)));

            //var a = people.ElementAt(0);
            //a.Address = null;
            //var b = people.ElementAt(1);



            //var peoples = people.AsReadable().ReadAllProps().ToArray();

            var agg = MongoExtensions.GetCollection<Person>().Aggregate(new AggregateOptions{TranslationOptions=new ExpressionTranslationOptions{StringTranslationMode = AggregateStringTranslationMode.);

            agg
        }
    }

}

public static class MongoExtensions
{
    public static IMongoCollection<TModel> GetCollection<TModel>
    (
        string connectionString = "mongodb://localhost:27017",
        string databaseName = "identity",
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
