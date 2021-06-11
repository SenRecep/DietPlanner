using System.Reflection;

using DietPlanner.DTO.Validation;
using DietPlanner.Server.BLL.ExtensionMethods;
using DietPlanner.Server.BLL.Interfaces;
using DietPlanner.Server.BLL.Managers;
using DietPlanner.Server.BLL.Managers.ReportExport;
using DietPlanner.Server.BLL.Settings;
using DietPlanner.Server.DAL.Concrete.EntityFrameworkCore.Contexts;
using DietPlanner.Server.DAL.Concrete.EntityFrameworkCore.Repositories;
using DietPlanner.Server.DAL.Interfaces;

using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
            services.AddTransient(typeof(IGenericSingleQueryService<>), typeof(GenericSingleQueryManager<>));
            services.AddScoped<IPersonService, PersonManager>();
            services.AddScoped<IRoleService, RoleManager>();

            services.AddScoped<HtmlTool>();
            services.AddScoped<JsonTool>();
            services.AddScoped<HtmlReportExportCreator>();
            services.AddScoped<JsonReportExportCreator>();


            services.AddTransient<IMessageService, MailService>();
            #endregion

            #region Repositoryies
            services.AddTransient(typeof(IGenericCommandRepository<>), typeof(EfGenericCommandRepository<>));
            services.AddTransient(typeof(IGenericQueryRepository<>), typeof(EfGenericQueryRepository<>)); 
            services.AddTransient(typeof(IGenericSingleQueryRepository<>), typeof(EfGenericSingleQueryRepository<>));
            services.AddScoped<IPersonRepository, EfPersonRepository>();
            services.AddScoped<IPersonSingleQueryRepository, EfPersonSingleQueryRepository>();
            services.AddScoped<IRoleRepository, EfRoleRepository>();
            services.AddScoped<IPatientRepository, EfPatientRepository>();
            services.AddScoped<IExportRepository, EfExportRepository>();
            #endregion

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<ICustomMapper, CustomMapper>();


            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.AddSingleton<IMailSettings>(sp => sp.GetRequiredService<IOptions<MailSettings>>().Value);


        }

        public static void AddValidationDependencies(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.AddFluentValidation(opt =>
                opt.RegisterValidatorsFromAssemblyContaining<ValidationLayer>()
            );
        }
    }
}
