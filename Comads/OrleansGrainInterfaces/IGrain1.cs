using System.Threading.Tasks;
using Orleans;

namespace Comads
{
    /// <summary>
    /// Grain interface IGrain1
    /// </summary>
    public interface IGrain1 : IGrainWithIntegerKey
    {
        Task<string> Reply(string message);
    }
}
