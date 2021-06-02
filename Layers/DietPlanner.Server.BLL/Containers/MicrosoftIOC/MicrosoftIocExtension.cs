using System.Reflection;

using DietPlanner.DTO.Validation;
using DietPlanner.Server.BLL.ExtensionMethods;
using DietPlanner.Server.BLL.Interfaces;
using DietPlanner.Server.BLL.Managers;
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

            #region Services
            services.AddTransient(typeof(IGenericQueryService<>), typeof(GenericQueryManager<>));
            services.AddTransient(typeof(IGenericCommandService<>), typeof(GenericCommandManager<>));
            services.AddScoped<IPersonService, PersonManager>();
            #endregion

            #region Repositoryies
            services.AddTransient(typeof(IGenericCommandRepository<>), typeof(EfGenericCommandRepository<>));
            services.AddTransient(typeof(IGenericQueryRepository<>), typeof(EfGenericQueryRepository<>)); 
            services.AddTransient(typeof(IGenericSingleQueryRepository<>), typeof(EfGenericSingleQueryRepository<>));
            services.AddScoped<IPersonRepository, EfPersonRepository>();
            #endregion

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<ICustomMapper, CustomMapper>();

        }

        public static void AddValidationDependencies(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.AddFluentValidation(opt =>
                opt.RegisterValidatorsFromAssemblyContaining<ValidationLayer>()
            );
        }
    }
}
