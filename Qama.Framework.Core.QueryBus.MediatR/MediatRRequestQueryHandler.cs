using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Qama.Framework.Core.Abstractions.Queries;

namespace Qama.Framework.Core.QueryBus.MediatR
{
    public class MediatRRequestQueryHandler<T> :
        IRequestHandler<MediatRQueryBase<T, IQueryResult>, IQueryResult> where T : QueryBase
    {
        private readonly IQueryHandler<T> _queryHandler;
        public MediatRRequestQueryHandler(IQueryHandler<T> queryHandler)
        {
            _queryHandler = queryHandler;
        }
        public async Task<IQueryResult> Handle(MediatR.MediatRQueryBase<T, IQueryResult> request, CancellationToken cancellationToken)
        {
            return await _queryHandler.Handle(request.Query);
        }
    }
}
