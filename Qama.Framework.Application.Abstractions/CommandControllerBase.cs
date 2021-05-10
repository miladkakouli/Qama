using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Qama.Framework.Core.Abstractions.Commands;

namespace Qama.Framework.Application.Abstractions
{
    [ApiController]
    public abstract class CommandControllerBase<T, TInput> : ControllerBase
        where T : CommandBase
        where TInput : IInputDtoCommand<T>
    {
        private readonly ICommandBus _bus;
        public CommandControllerBase(ICommandBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task Post(TInput input)
        {
            await _bus.Dispatch(input.ToCommand());
        }
    }
}
