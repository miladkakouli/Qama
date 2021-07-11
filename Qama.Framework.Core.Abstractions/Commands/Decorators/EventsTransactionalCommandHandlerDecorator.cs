using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qama.Framework.Core.Abstractions.Events;

namespace Qama.Framework.Core.Abstractions.Commands.Decorators
{
    public class EventsTransactionalCommandHandlerDecorator<T> : ICommandHandler<T> where T : CommandBase
    {
        private readonly ICommandHandler<T> _commandHandler;
        private readonly IEventUnitOfWork _unitOfWork;

        public EventsTransactionalCommandHandlerDecorator(ICommandHandler<T> commandHandler, IEventUnitOfWork unitOfWork)
        {
            _commandHandler = commandHandler;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(T command)
        {
            try
            {
                _unitOfWork.Begin();
                await _commandHandler.Handle(command);

                _unitOfWork.Commit();
            }
            catch
            {
                _unitOfWork.Rollback();
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }
    }
}
