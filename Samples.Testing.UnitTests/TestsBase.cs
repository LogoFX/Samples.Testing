using System;
using Microsoft.Practices.Unity;

namespace Samples.Testing.UnitTests
{
    public abstract class TestsBase : IDisposable
    {
        public abstract class WithRootObject<TRootObject> : TestsBase
        {
            protected TRootObject GetRootObject()
            {
                return Container.Resolve<TRootObject>();
            }
        }

        protected readonly IUnityContainer Container;

        protected TestsBase()
        {
            Container = new UnityContainer();
        }

        public void Dispose()
        {
            Container.Dispose();
        }

        protected void RegisterInstance<T>(T instance)
        {
            Container.RegisterInstance(instance);
        }

        protected void RegisterBuilder<T>(Func<T> instanceCreator)
        {
            Container.RegisterInstance(instanceCreator());
        }
    }    
}