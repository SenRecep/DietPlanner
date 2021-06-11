using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DietPlanner.Server.DAL.Concrete.EntityFrameworkCore.Mapping.ExtensionMethods;
using DietPlanner.Server.Entities.Concrete;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DietPlanner.Server.DAL.Concrete.EntityFrameworkCore.Mapping
{
    public class FileModelMapping : IEntityTypeConfiguration<FileModel>
    {
        public void Configure(EntityTypeBuilder<FileModel> builder)
        {
            builder.EntityBaseMap();

            builder.Property(x=>x.FileName).IsRequired();
            builder.Property(x=>x.OriginalFileName).IsRequired();
            builder.Property(x=>x.ContentType).IsRequired();
            builder.Property(x=>x.Type).IsRequired();
        }
    }
}
