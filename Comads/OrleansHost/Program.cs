using System;
using System.Net;
using Orleans;
using Orleans.Runtime.Configuration;
using Orleans.Runtime.Host;

namespace Comads
{
    class Program
    {
        static SiloHost siloHost;

        static void Main(string[] args)
        {
            Console.Title = "Silo";

            try
            {
                using (var siloHost = new SiloHost("Silo"))
                {
                    siloHost.LoadOrleansConfig();
                    siloHost.InitializeOrleansSilo();
                    var startedOk = siloHost.StartOrleansSilo(catchExceptions: false);
                    Console.WriteLine("Silo started successfully!");

                    Console.WriteLine("Press ENTER to exit...");
                    Console.ReadLine();
                    siloHost.ShutdownOrleansSilo();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }

}
