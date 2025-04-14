using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecom.Core.interfaces;
using Ecom.Infrastructure.Data;
using Ecom.Infrastructure.Repositries;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Ecom.Core.Services;
using Microsoft.Extensions.FileProviders;

namespace Ecom.Infrastructure
{
    public static class infrastructureRegistration
    {
        public static IServiceCollection infrastructureConfiguration(this IServiceCollection services , IConfiguration configuration)
        {
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();
    
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddSingleton<IImageManagementService,  ImageManagementService>();
            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"))
);

            services.AddDbContext<AppDbContext>(op => {
                op.UseSqlServer(configuration.GetConnectionString("EcomDataBases"));
                });
            return services;
        }

    }
}
