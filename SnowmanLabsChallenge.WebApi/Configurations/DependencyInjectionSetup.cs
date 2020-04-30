using SnowmanLabsChallenge.Infra.CrossCutting.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace SnowmanLabsChallenge.WebApi.Configurations
{
    public static class DependencyInjectionSetup
    {
        public static void AddDependencyInjectionSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}
