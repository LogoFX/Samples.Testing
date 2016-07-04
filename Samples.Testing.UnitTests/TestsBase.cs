using System;
using Microsoft.Practices.Unity;

namespace Samples.Testing.UnitTests
{
    public abstract class TestsBase : IDisposable
    {
        public abstract class WithRootObject<TRootObject, TRootObjectImplementation> : TestsBase 
            where TRootObjectImplementation : TRootObject
        {
            protected WithRootObject()
            {
                _container.RegisterType<TRootObject, TRootObjectImplementation>();
            }

            protected TRootObject GetRootObject()
            {
                return _container.Resolve<TRootObject>();
            }
        }

        private readonly IUnityContainer _container;

        protected TestsBase()
        {
            _container = new UnityContainer();            
        }

        public void Dispose()
        {
            _container.Dispose();
        }

        protected void RegisterInstance<T>(T instance)
        {
            _container.RegisterInstance(instance);
        }

        protected void RegisterBuilder<T>(Func<T> instanceCreator)
        {
            _container.RegisterInstance(instanceCreator());
        }
    }    
}