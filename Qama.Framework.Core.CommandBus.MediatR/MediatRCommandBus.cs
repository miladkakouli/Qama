using System.Threading.Tasks;
using MediatR;
using Qama.Framework.Core.Abstractions.Commands;

namespace Qama.Framework.Core.CommandBus.MediatR
{
    public class MediatRCommandBus : ICommandBus
    {
        private readonly IMediator _mediator;
        public MediatRCommandBus(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Dispatch<T>(T command) where T : CommandBase
        {
            await _mediator.Send(new MediatRCommandBase<T>(command));
        }
    }
}
