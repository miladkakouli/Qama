using System.Threading.Tasks;

namespace Qama.Framework.Core.Abstractions.Queries
{
    public interface IQueryHandler<T>
        where T : QueryBase
    {
        /**
         * <summary>A Method to Handle <see cref="QueryBase">Query</see></summary>
         * <param name="query">query to Handle It. It should be QueryBase</param>
         * <returns>object of type <see cref="IQueryResult">IQueryResult</see></returns>
         * <example>sldkjslkdjsldjk<code>int x = 0;</code></example>
         */
        Task<IQueryResult> Handle(T query);
    }
}
