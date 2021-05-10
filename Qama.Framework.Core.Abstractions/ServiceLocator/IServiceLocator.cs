using System;
using System.Collections.Generic;

namespace Qama.Framework.Core.Abstractions.ServiceLocator
{
    public interface IServiceLocator
    {
        T GetInstance<T>();
        object GetInstance(Type type);
        IEnumerable<T> GetInstances<T>();
        void Release(object obj);
    }
}
