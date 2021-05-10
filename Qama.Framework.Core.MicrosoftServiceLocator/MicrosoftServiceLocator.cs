using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Qama.Framework.Core.Abstractions.ServiceLocator;

namespace Qama.Framework.Core.MicrosoftServiceLocator
{
    public class MicrosoftServiceLocator : IServiceLocator
    {
        private readonly IServiceProvider _container;
        public MicrosoftServiceLocator(IServiceProvider container)
        {
            this._container = container;
        }
        public T GetInstance<T>()
        {
            return _container.GetService<T>();
        }

        public object GetInstance(Type type)
        {
            return _container.GetService(type);
        }

        public IEnumerable<T> GetInstances<T>()
        {
            return _container.GetServices<T>();
        }

        public void Release(object obj)
        {
            //obj.
        }

    }
}
