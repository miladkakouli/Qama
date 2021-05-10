using System.Collections.Generic;
using System.Threading.Tasks;
using Qama.Framework.Core.Abstractions.Validator;

namespace Qama.Framework.Core.Abstractions.Events.Decorator
{
    public class ValidationalEventHandlerDecorator<T> : IEventHandler<T> where T : EventBase
    {
        private readonly IEventHandler<T> _eventHandler;
        private readonly IEnumerable<IValidator<T>> _validators;

        public ValidationalEventHandlerDecorator(IEventHandler<T> eventHandler, IEnumerable<IValidator<T>> validators)
        {
            _eventHandler = eventHandler;
            _validators = validators;
        }
        public async Task Handle(T command)
        {
            foreach (var validator in _validators)
            {
                validator.Validate(command);
            }
            await _eventHandler.Handle(command);
        }
    }
}
