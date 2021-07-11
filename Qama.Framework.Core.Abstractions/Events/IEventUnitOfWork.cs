using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qama.Framework.Core.Abstractions.Events
{
    public interface IEventUnitOfWork : IDisposable
    {
        void Begin();
        void Commit();
        void Rollback();
        string GetConnectionString();
    }
}
