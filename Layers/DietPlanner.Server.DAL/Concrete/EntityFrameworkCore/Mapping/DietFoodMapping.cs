
using DietPlanner.Server.DAL.Concrete.EntityFrameworkCore.Mapping.ExtensionMethods;
using DietPlanner.Server.Entities.Concrete;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DietPlanner.Server.DAL.Concrete.EntityFrameworkCore.Mapping
{
    public class DietFoodMapping : IEntityTypeConfiguration<DietFood>
    {
        public void Configure(EntityTypeBuilder<DietFood> builder)
        {
            builder.EntityBaseMap();

            builder.HasOne(x => x.Food)
                .WithMany(x => x.DietFoods)
                .HasForeignKey(x => x.FoodId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Diet)
                .WithMany(x => x.DietFoods)
                .HasForeignKey(x => x.DietId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
