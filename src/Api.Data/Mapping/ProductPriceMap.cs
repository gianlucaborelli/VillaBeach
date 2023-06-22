using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class ProductPriceMap : IEntityTypeConfiguration<ProductPriceEntity>
    {
        public void Configure(EntityTypeBuilder<ProductPriceEntity> builder)
        {
            builder.ToTable("ProductPrices");

            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.Id);

            builder.HasOne(pp => pp.Product)
                .WithMany(p => p.Prices)
                .HasForeignKey(pp => pp.ProductId);

            builder.Property(c => c.Value)
                .IsRequired();

            builder.Property(p => p.Current)
                    .IsRequired();

        }
    }
}