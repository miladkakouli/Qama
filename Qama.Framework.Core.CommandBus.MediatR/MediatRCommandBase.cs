using MediatR;
using Qama.Framework.Core.Abstractions.Commands;

namespace Qama.Framework.Core.CommandBus.MediatR
{
    public class MediatRCommandBase<T> : IRequest where T : CommandBase
    {
        public T Command { get; private set; }
        public MediatRCommandBase(T command)
        {
            Command = command;
        }
    }
}
