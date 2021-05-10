using System.Threading.Tasks;

namespace Qama.Framework.Core.Abstractions.Commands
{
    public interface ICommandHandler<T>
    {
        Task Handle(T command);
    }
}
