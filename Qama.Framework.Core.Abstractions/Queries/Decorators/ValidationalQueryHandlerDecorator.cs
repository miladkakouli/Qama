using System.Collections.Generic;
using System.Threading.Tasks;
using Qama.Framework.Core.Abstractions.Validator;

namespace Qama.Framework.Core.Abstractions.Queries.Decorators
{
    public class ValidationalQueryHandlerDecorator<T> : IQueryHandler<T>
        where T : QueryBase
    {
        private readonly IQueryHandler<T> _queryHandler;
        private readonly IEnumerable<IValidator<T>> _validators;
        public ValidationalQueryHandlerDecorator(IQueryHandler<T> queryHandler, IEnumerable<IValidator<T>> validators)
        {
            _queryHandler = queryHandler;
            _validators = validators;
        }

        public async Task<IQueryResult> Handle(T query)
        {
            foreach (var validator in _validators)
            {
                validator.Validate(query);
            }
            return await _queryHandler.Handle(query);
        }
    }
}
