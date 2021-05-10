using System.Threading.Tasks;
using Qama.Framework.Core.Abstractions.Logging;
using Qama.Framework.Core.Abstractions.Queries;
using Qama.Framework.Core.Abstractions.ServiceLocator;

namespace Qama.Framework.Core.QueryBus.InMemory
{
    public class InMemoryQueryBus : IQueryBus
    {
        private readonly IServiceLocator _locator;
        private readonly IEverythingLogger _everythingLogger;

        public InMemoryQueryBus(IServiceLocator locator, IEverythingLogger everythingLogger)
        {
            _locator = locator;
            _everythingLogger = everythingLogger;
            _everythingLogger.LogDebug($"using {nameof(InMemoryQueryBus)}");
        }
        public async Task<IQueryResult> Dispatch<T>(T query)
            where T : QueryBase
        {
            var handler = _locator.GetInstance<IQueryHandler<T>>();
            _everythingLogger.LogDebug($"calling {handler} for {query}");
            return await handler.Handle(query);
        }
    }
}
