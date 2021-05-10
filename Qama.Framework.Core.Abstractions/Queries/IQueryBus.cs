using System.Threading.Tasks;

namespace Qama.Framework.Core.Abstractions.Queries
{
    public interface IQueryBus
    {
        Task<IQueryResult> Dispatch<T>(T query)
            where T : QueryBase;
    }
}
