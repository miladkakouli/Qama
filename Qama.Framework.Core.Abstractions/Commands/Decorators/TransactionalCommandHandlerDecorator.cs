using System.Threading.Tasks;
using Qama.Framework.Core.Abstractions.Persistence;

namespace Qama.Framework.Core.Abstractions.Commands.Decorators
{
    public class TransactionalCommandHandlerDecorator<T> : ICommandHandler<T> where T : CommandBase
    {
        private readonly ICommandHandler<T> _commandHandler;
        private readonly IUnitOfWork _unitOfWork;

        public TransactionalCommandHandlerDecorator(ICommandHandler<T> commandHandler, IUnitOfWork unitOfWork)
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
