using System.Threading.Tasks;
using Qama.Framework.Core.Abstractions.Commands;
using Qama.Framework.Core.Abstractions.Logging;
using Qama.Framework.Core.Abstractions.ServiceLocator;

namespace Qama.Framework.Core.CommandBus.InMemory
{
    public class InMemoryCommandBus : ICommandBus
    {
        private readonly IServiceLocator _locator;
        private readonly IEverythingLogger _everythingLogger;

        public InMemoryCommandBus(IServiceLocator locator, IEverythingLogger everythingLogger)
        {
            _locator = locator;
            _everythingLogger = everythingLogger;
            _everythingLogger.LogDebug($"using {nameof(InMemoryCommandBus)}");
        }
        public async Task Dispatch<T>(T command) where T : CommandBase
        {
            var handler = _locator.GetInstance<ICommandHandler<T>>();
            _everythingLogger.LogDebug($"calling {handler} for {command}");
            await handler.Handle(command);
        }
    }
}
