using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qama.Framework.Core.Abstractions.Events;
using RabbitMQ.Client;

namespace Qama.Framework.Core.EventBus.RabbitMQ
{
    public class RabbitMQEventUnitOfWork : IEventUnitOfWork
    {
        private readonly IModel _channel;

        public RabbitMQEventUnitOfWork(IModel channel)
        {
            _channel = channel;
        }

        public void Dispose()
        {
            _channel.Dispose();
        }

        public void Begin()
        {
            _channel.TxSelect();
        }

        public void Commit()
        {
            _channel.TxCommit();
        }

        public void Rollback()
        {
            _channel.TxRollback();
        }

        public string GetConnectionString()
        {
            return _channel.ToString();
        }
    }
}
