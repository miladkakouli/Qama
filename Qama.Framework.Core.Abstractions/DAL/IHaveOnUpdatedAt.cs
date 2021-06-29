using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qama.Framework.Core.Abstractions.DAL
{
    public interface IHaveOnUpdatedAt : IHaveOnUpdate
    {
        DateTime UpdatedAt { get; set; }
        string UpdatedAtColumnName { get; }

        void IHaveOnUpdate.OnPreUpdate(Action<string, object> action)
        {
            UpdatedAt = DateTime.Now;
            action.Invoke(UpdatedAtColumnName, UpdatedAt);
        }

        void IHaveOnUpdate.OnPostUpdate()
        { }
    }
}
