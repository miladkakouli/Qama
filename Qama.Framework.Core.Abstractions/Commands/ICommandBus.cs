using System.Threading.Tasks;

namespace Qama.Framework.Core.Abstractions.Commands
{
    public interface ICommandBus
    {
        Task Dispatch<T>(T command) where T : CommandBase;
    }
}
