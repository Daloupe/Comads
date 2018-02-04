using System;
using System.Threading;
using System.Threading.Tasks;
using Orleans;
using Orleans.Runtime;

namespace Comads
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Title = "Client";

            while (true)
            {
                try
                {
                    var client = ClientBuilder
                    .CreateDefault()
                    .LoadConfiguration("ClientConfiguration.xml")
                    .ConfigureServices(services =>
                    {
                      // Add services if you ddarrreeeeee
                    })
                    .Build();

                    await client.Connect().ConfigureAwait(false);
                    Console.WriteLine("Connected to silo!");

                    var friend = client.GetGrain<IGrain1>(0);
                    Console.WriteLine(await friend.Reply("Hello!"));


                    Console.ReadLine();
                    break;
                }
                catch (SiloUnavailableException)
                {
                    Console.WriteLine("Silo not available! Retrying in 3 seconds.");
                    Thread.Sleep(3000);
                }
            }
        }
    }
}
