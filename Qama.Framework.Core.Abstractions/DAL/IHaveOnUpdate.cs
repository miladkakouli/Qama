using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qama.Framework.Core.Abstractions.DAL
{
    public interface IHaveOnUpdate
    {
        void OnPreUpdate(Action<string, object> action);
        void OnPostUpdate();

    }
}
