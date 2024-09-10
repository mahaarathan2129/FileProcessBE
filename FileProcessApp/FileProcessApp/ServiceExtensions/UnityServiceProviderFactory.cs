using Microsoft.Extensions.DependencyInjection;
using Unity;

namespace FileProcessingApp.ServiceExtensions
{

    public class UnityServiceProvider : IServiceProvider
    {
        private readonly UnityContainer _unityContainer;

        public UnityServiceProvider(UnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public object GetService(Type serviceType)
        {
            return _unityContainer.Resolve(serviceType);
        }
    }

    //public class UnityServiceProviderFactory : IServiceProviderFactory<IUnityContainer>
    //{
    //    public IUnityContainer CreateBuilder(IServiceCollection services)
    //    {
    //        var container = new UnityContainer();
    //        foreach (var service in services)
    //        {
    //            // Register services in the Unity container
    //            // You might need to implement registration logic here based on your needs
    //        }
    //        return container;
    //    }
    //    public IServiceProvider CreateServiceProvider(IUnityContainer container)
    //    {
    //        return new UnityServiceProvider(container);
    //    }
    //}
    //public class UnityServiceProvider : IServiceProvider
    //{
    //    private readonly IUnityContainer _container;

    //    public UnityServiceProvider(IUnityContainer container)
    //    {
    //        _container = container;
    //    }

    //    public object GetService(Type serviceType)
    //    {
    //        return _container.Resolve(serviceType);
    //    }
    //}

}
