using System.Threading.Tasks;
using Orleans;
using Microsoft.Extensions.Logging;

namespace Comads
{
    /// <summary>
    /// Grain implementation class Grain1.
    /// </summary>
    public class Grain1 : Orleans.Grain, IGrain1
    {
        private readonly ILogger logger;

        public Grain1(ILogger<Grain1> logger)
        {
            this.logger = logger;
        }

        Task<string> IGrain1.SayHello(string greeting)
        {
            logger.LogInformation($"SayHello message received: greeting = '{greeting}'");
            return Task.FromResult($"You said: '{greeting}', I say: Hello!");
        }
    }
}
