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
            Console.WriteLine("Starting Up");

            // Orleans should run in its own AppDomain, we set it up like this
            var hostDomain = AppDomain.CreateDomain("OrleansHost", null,
            new AppDomainSetup()
            {
                AppDomainInitializer = InitSilo
            });

            DoSomeClientWork();

            Console.WriteLine("Orleans Silo is running.\nPress Enter to terminate...");
            Console.ReadLine();

            // We do a clean shutdown in the other AppDomain
            hostDomain.DoCallBack(ShutdownSilo);
        }

        static void DoSomeClientWork()
        {
            // Orleans comes with a rich XML and programmatic configuration. Here we're just going to set up with basic programmatic config
            var config =ClientConfiguration.LocalhostSilo(30000);
            GrainClient.Initialize(config);

            var friend = GrainClient.GrainFactory.GetGrain<IGrain1>(0);
            var result = friend.Reply("Goodbye").Result;

            Console.WriteLine(result);
        }

        static void InitSilo(string[] args)
        {
            siloHost = new SiloHost(Dns.GetHostName());
            // The Cluster config is quirky and weird to configure in code, so we're going to use a config file
            siloHost.ConfigFileName = "OrleansConfiguration.xml";

            siloHost.InitializeOrleansSilo();
            var startedok = siloHost.StartOrleansSilo();
            if (!startedok)
                throw new SystemException(String.Format("Failed to start Orleans silo '{0}' as a {1} node", siloHost.Name, siloHost.Type));
        }

        static void ShutdownSilo()
        {
            if (siloHost != null)
            {
                siloHost.Dispose();
                GC.SuppressFinalize(siloHost);
                siloHost = null;
            }
        }
    }

}
