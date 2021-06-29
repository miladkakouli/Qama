using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qama.Framework.Core.Abstractions.DAL
{
    public interface IHaveOnCreatedAt : IHaveOnCreate
    {
        DateTime CreatedAt { get; set; }
        string CreatedAtColumnName { get; }
        void IHaveOnCreate.OnPreCreate(Action<string, object> action)
        {
            this.CreatedAt = DateTime.Now;
            action.Invoke(CreatedAtColumnName, this.CreatedAt);
        }

        void IHaveOnCreate.OnPostCreate()
        { }
    }
}
