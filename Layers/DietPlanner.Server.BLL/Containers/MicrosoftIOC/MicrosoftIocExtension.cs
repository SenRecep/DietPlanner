using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DietPlanner.Server.BLL.ExtensionMethods;
using DietPlanner.Server.DAL.Concrete.EntityFrameworkCore.Contexts;
using DietPlanner.Server.DAL.Concrete.EntityFrameworkCore.Repositories;
using DietPlanner.Server.DAL.Interfaces;

using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DietPlanner.Server.BLL.Containers.MicrosoftIOC
{
    public static class MicrosoftIocExtension
    {

        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            string connectionString = configuration.GetCustomConnectionString(environment.GetConnectionType());
            string migrationName = "DietPlanner.Server";

            services.AddTransient<DbContext, DietPlannerDbContext>();

            services.AddDbContext<DietPlannerDbContext>(opt =>
                opt.UseSqlServer(connectionString, sqlOpt =>
                    sqlOpt.MigrationsAssembly(migrationName)
                    )
            );

            services.AddHttpContextAccessor();

            //services.AddTransient(typeof(IGenericService<>), typeof(GenericManager<>));
            services.AddTransient(typeof(IGenericRepository<>), typeof(EfGenericRepository<>));

            //services.AddAutoMapper(typeof(ProductMappingProfile));
            //services.AddScoped<ICustomMapper, CustomMapper>();

        }

        public static void AddValidationDependencies(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.AddFluentValidation(opt =>
            {
                //opt.RegisterValidatorsFromAssemblyContaining<ProductCreateDtoValidator>();
            });
        }
    }
}
