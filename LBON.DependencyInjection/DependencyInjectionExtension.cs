using System.Linq;
using System.Reflection;
using LBON.DependencyInjection.DependencyInjection;
using LBON.EntityFrameworkCore.Repositories;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtension
    {
        public static void ServiceRegister(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddScoped(typeof(IEfRepository<,,>), typeof(EfRepository<,,>));
            if (assemblies.Length <= 0)
            {
                assemblies = new[] { Assembly.GetExecutingAssembly() };
            }

            foreach (var assembly in assemblies)
            {
                var allTypes = assembly.GetTypes();
                foreach (var type in allTypes)
                {
                    if (typeof(ITransientDependency).IsAssignableFrom(type) && type.IsClass && !type.IsAbstract)
                    {

                        var interfaceTypes = type.GetInterfaces().Where(p => p.FullName != null && !p.FullName.Contains("ITransientDependency"));
                        foreach (var interfaceType in interfaceTypes)
                        {
                            services.AddTransient(interfaceType, type);
                        }
                    }
                }

                foreach (var type in allTypes)
                {
                    if (typeof(ISingletonDependency).IsAssignableFrom(type) && type.IsClass && !type.IsAbstract)
                    {
                        var interfaceTypes = type.GetInterfaces().Where(p => p.FullName != null && !p.FullName.Contains("ISingletonDependency"));
                        foreach (var interfaceType in interfaceTypes)
                        {
                            services.AddSingleton(interfaceType, type);
                        }
                    }
                }

                foreach (var type in allTypes)
                {
                    if (typeof(IScopedDependency).IsAssignableFrom(type) && type.IsClass && !type.IsAbstract)
                    {
                        var interfaceTypes = type.GetInterfaces().Where(p => p.FullName != null && !p.FullName.Contains("IScopedDependency"));
                        foreach (var interfaceType in interfaceTypes)
                        {
                            services.AddScoped(interfaceType, type);
                        }
                    }
                }
            }

        }
    }
}
