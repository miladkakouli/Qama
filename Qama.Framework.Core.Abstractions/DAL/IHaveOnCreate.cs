using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qama.Framework.Core.Abstractions.DAL
{
    public interface IHaveOnCreate
    {
        void OnPreCreate(Action<string, object> action);
        void OnPostCreate();

    }
}
