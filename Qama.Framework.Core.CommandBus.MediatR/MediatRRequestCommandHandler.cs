using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Qama.Framework.Core.Abstractions.Commands;

namespace Qama.Framework.Core.CommandBus.MediatR
{
    public class MediatRRequestCommandHandler<T> :
        IRequestHandler<MediatRCommandBase<T>, Unit> where T : CommandBase
    {
        private readonly ICommandHandler<T> _commandHandler;
        public MediatRRequestCommandHandler(ICommandHandler<T> commandHandler)
        {
            _commandHandler = commandHandler;
        }
        public async Task<Unit> Handle(MediatRCommandBase<T> request, CancellationToken cancellationToken)
        {
            await _commandHandler.Handle(request.Command);
            return new Unit();
        }
    }
}
