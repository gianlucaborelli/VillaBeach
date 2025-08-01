using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class ProductMap : IEntityTypeConfiguration<Product>
     {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.Name);

            builder.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(50);

            builder.Property(p => p.Stock)
                    .IsRequired()
                    .HasMaxLength(50);

            builder.Property(p => p.Description)
                    .HasMaxLength(50);

            builder.Property(p => p.BarCode)
                    .HasMaxLength(15);

            builder.Property(p => p.Price)
                    .IsRequired()
                    .HasPrecision(12, 2);
        }
    }
}