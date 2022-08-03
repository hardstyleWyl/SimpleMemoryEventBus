using SimpleMemoryEventBus;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SimpleMemoryEventBusServiceCollectionExtension
    {
        public static IServiceCollection AddSimpleMemoryEventBus(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            services.AddSingleton<ISimpleEventBus>(new SimpleEventBus(assemblies));
            return services;
        }
    }
}
