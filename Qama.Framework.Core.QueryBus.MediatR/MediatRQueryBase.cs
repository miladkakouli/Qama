using MediatR;
using Qama.Framework.Core.Abstractions.Queries;

namespace Qama.Framework.Core.QueryBus.MediatR
{
    public class MediatRQueryBase<T, TResult> :
        IRequest<TResult> where T : QueryBase
    {
        public T Query { get; private set; }
        public MediatRQueryBase(T query)
        {
            Query = query;
        }
    }
}
