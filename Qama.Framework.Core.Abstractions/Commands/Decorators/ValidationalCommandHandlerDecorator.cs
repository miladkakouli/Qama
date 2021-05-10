using System.Collections.Generic;
using System.Threading.Tasks;
using Qama.Framework.Core.Abstractions.Validator;

namespace Qama.Framework.Core.Abstractions.Commands.Decorators
{
    public class ValidationalCommandHandlerDecorator<T> : ICommandHandler<T> where T : CommandBase
    {
        private readonly ICommandHandler<T> _commandHandler;
        private readonly IEnumerable<IValidator<T>> _validators;

        public ValidationalCommandHandlerDecorator(ICommandHandler<T> commandHandler, IEnumerable<IValidator<T>> validators)
        {
            _commandHandler = commandHandler;
            _validators = validators;
        }
        public async Task Handle(T command)
        {
            foreach (var validator in _validators)
            {
                validator.Validate(command);
            }
            await _commandHandler.Handle(command);
        }
    }
}
