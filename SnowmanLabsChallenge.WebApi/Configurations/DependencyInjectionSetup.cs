using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SnowmanLabsChallenge.Infra.CrossCutting.IoC;
using System;

namespace SnowmanLabsChallenge.WebApi.Configurations
{
    public static class DependencyInjectionSetup
    {
        public static void AddDependencyInjectionSetup(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            NativeInjectorBootStrapper.RegisterServices(services, configuration);
        }
    }
}
