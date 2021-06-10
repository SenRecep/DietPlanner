﻿
using DietPlanner.Server.DAL.Concrete.EntityFrameworkCore.Mapping.ExtensionMethods;
using DietPlanner.Server.Entities.Concrete;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DietPlanner.Server.DAL.Concrete.EntityFrameworkCore.Mapping
{
    public class DietMapping : IEntityTypeConfiguration<Diet>
    {
        public void Configure(EntityTypeBuilder<Diet> builder)
        {
            builder.EntityBaseMap();
            builder.Property(x => x.Name).HasMaxLength(30).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(1500).IsRequired();
        }
    }
}
