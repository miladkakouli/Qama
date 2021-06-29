using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Qama.Framework.Core.Abstractions.Queries;

namespace Qama.Framework.Application.Abstractions
{
    [ApiController]
    [Route("[controller]")]
    public abstract class QueryControllerBase<T, TInput> : ControllerBase
        where T : QueryBase
        where TInput : IInputDtoQuery<T>
    {
        private readonly IQueryBus _bus;
        public QueryControllerBase(IQueryBus bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<object> Post(TInput query)
        {
            return await _bus.Dispatch(query.ToQuery());
        }
    }
}
