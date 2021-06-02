
using DietPlanner.Server.Entities.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DietPlanner.Server.DAL.Concrete.EntityFrameworkCore.Mapping.ExtensionMethods
{
    public static class TypeBuilderExtensionMethods
    {
        public static void EntityBaseMap<TEntity>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : class, IEntityBase, new()
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("newid()").ValueGeneratedOnAdd();
            builder.Property(x => x.CreatedTime).HasDefaultValueSql("getdate()").ValueGeneratedOnAdd().IsRequired();
            builder.Property(x => x.UpdateTime).IsRequired(false);
            builder.Property(x => x.CreateUserId).IsRequired();
            builder.Property(x => x.UpdateUserId).IsRequired(false);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false).IsRequired();

        }


        public static void PersonMap<TEntity>(this EntityTypeBuilder<TEntity> builder)
           where TEntity : class, IPerson, new()
        {
            builder.EntityBaseMap();
            builder.Property(x => x.FirstName).HasMaxLength(40).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(40).IsRequired();
            builder.Property(x => x.IdentityNumber).HasMaxLength(11).IsRequired();
            builder.Property(x => x.Address).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(50).IsRequired();
            builder.Property(x => x.PhoneNumber).HasMaxLength(11).IsRequired();
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.RoleId).IsRequired();
            builder.HasIndex(x=>x.IdentityNumber).IsUnique();
        }
    }
}
