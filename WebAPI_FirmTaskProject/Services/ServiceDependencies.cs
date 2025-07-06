using Contracts.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Services.Profiles;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class ServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddAutoMapper(typeof(EmployeeProfile));

            return services;
        }
    }
}
