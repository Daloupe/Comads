using Orleans.Runtime.Configuration;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Hosting;
using Orleans.CodeGeneration;
using Orleans.CodeGenerator;

namespace Comads
{
    public class Program
    {
        public static int Main(string[] args)
        {
            Console.WriteLine("Starting Up...");
            return RunMainAsync().Result;
        }

        private static async Task<int> RunMainAsync()
        {
            try
            {
                var host = await StartSilo();
                Console.WriteLine("Press Enter to terminate...");
                Console.ReadLine();

                await host.StopAsync();

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 1;
            }
        }

        private static async Task<ISiloHost> StartSilo()
        {
            var config = ClusterConfiguration.LocalhostPrimarySilo();
            config.AddMemoryStorageProvider();
            config.LoadFromFile("OrleansConfiguration.xml");

            var builder = new SiloHostBuilder()
                .UseConfiguration(config)
                .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(Grain1).Assembly).WithReferences())
                .ConfigureLogging(logging => logging.SetMinimumLevel(LogLevel.Debug));

            var host = builder.Build();

            await host.StartAsync();
            Console.WriteLine("Awaiting Connection...");
            return host;
        }
    }
}