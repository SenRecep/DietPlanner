
using DietPlanner.Server.DAL.Concrete.EntityFrameworkCore.Mapping.ExtensionMethods;
using DietPlanner.Server.Entities.Concrete;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DietPlanner.Server.DAL.Concrete.EntityFrameworkCore.Mapping
{
    public class DiseaseMapping : IEntityTypeConfiguration<Disease>
    {
        public void Configure(EntityTypeBuilder<Disease> builder)
        {
            builder.EntityBaseMap();
            builder.Property(x => x.Name).HasMaxLength(30).IsRequired();
        }
    }
}
