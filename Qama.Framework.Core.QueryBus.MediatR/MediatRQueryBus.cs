using System.Threading.Tasks;
using MediatR;
using Qama.Framework.Core.Abstractions.Queries;

namespace Qama.Framework.Core.QueryBus.MediatR
{
    public class MediatRQueryBus : IQueryBus
    {
        private readonly IMediator _mediator;

        public MediatRQueryBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IQueryResult> Dispatch<T>(T query) where T : QueryBase
        {
            return await _mediator.Send(new MediatRQueryBase<T, IQueryResult>(query));
        }
    }
}
