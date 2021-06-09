
using DietPlanner.Server.DAL.Concrete.EntityFrameworkCore.Mapping.ExtensionMethods;
using DietPlanner.Server.Entities.Concrete;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DietPlanner.Server.DAL.Concrete.EntityFrameworkCore.Mapping
{
    public class ReportMapping : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.EntityBaseMap();

            builder.Property(x=>x.StartTime).IsRequired();
            builder.Property(x=>x.EndTime).IsRequired();

            builder.HasOne(x=>x.Diet)
                .WithMany(x=>x.Reports)
                .HasForeignKey(x=>x.DietId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Dietician)
                .WithMany(x => x.Reports)
                .HasForeignKey(x => x.DieticianId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Patient)
                .WithMany(x => x.Reports)
                .HasForeignKey(x => x.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Disease)
                .WithMany(x => x.Reports)
                .HasForeignKey(x => x.DiseaseId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
