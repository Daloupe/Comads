using System.Threading.Tasks;
using Orleans;

namespace Comads
{
    /// <summary>
    /// Grain implementation class Grain1.
    /// </summary>
    public class Grain1 : Grain, IGrain1
    {
        public Task<string> Reply(string message)
        {
            return Task.FromResult($"{message}: answer");
        }
    }
}
