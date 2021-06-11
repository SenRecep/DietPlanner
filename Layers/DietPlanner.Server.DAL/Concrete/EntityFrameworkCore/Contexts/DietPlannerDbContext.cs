

using DietPlanner.Server.DAL.Concrete.EntityFrameworkCore.Mapping;

using Microsoft.EntityFrameworkCore;

namespace DietPlanner.Server.DAL.Concrete.EntityFrameworkCore.Contexts
{
    public class DietPlannerDbContext : DbContext
    {
        public DietPlannerDbContext(DbContextOptions<DietPlannerDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AdminMapping).Assembly);
        }

        public DbSet<Entities.Concrete.Admin> Admins { get; set; }
        public DbSet<Entities.Concrete.Diet> Diets { get; set; }
        public DbSet<Entities.Concrete.Dietician> Dieticians { get; set; }
        public DbSet<Entities.Concrete.Disease> Diseases { get; set; }
        public DbSet<Entities.Concrete.Patient> Patients { get; set; }
        public DbSet<Entities.Concrete.Report> Reports { get; set; }
        public DbSet<Entities.Concrete.Role> Roles { get; set; }
        public DbSet<Entities.Concrete.Food> Foods { get; set; }
        public DbSet<Entities.Concrete.DietFood> DietFoods { get; set; }
        public DbSet<Entities.Concrete.FileModel> FileModels { get; set; }
    }
}
