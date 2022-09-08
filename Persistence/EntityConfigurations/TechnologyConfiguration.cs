using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfigurations
{
    public class TechnologyConfiguration : IEntityTypeConfiguration<Technology>
    {
        public void Configure(EntityTypeBuilder<Technology> builder)
        {
            // Diğer tanımlamaları yazım kurallarına uyduğum için yazmıyorum.
            builder.Property(pl => pl.Name)
                .HasColumnType("nvarchar")
                .HasMaxLength(255);
        }
    }
}
