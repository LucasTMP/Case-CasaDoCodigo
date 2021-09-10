using Api.CasaDoCodigo.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.CasaDoCodigo.Configurations
{
    public static class DependencyInjectionConfig
    {

        public static IServiceCollection DependencyInjection(this IServiceCollection services)
        {

            services.AddScoped<ApiDbContext>();

            return services;
        }

    }
}
