using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Mapping
{
    public class PurchasedProductsMap : IEntityTypeConfiguration<PurchasedProductEntity>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<PurchasedProductEntity> builder)
        {
            builder.ToTable("PurchasedProducts");

            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.Id);

            builder.HasOne(p => p.Product)
                    .WithMany()
                    .HasForeignKey(p => p.ProductId);

            builder.HasOne(p => p.ProductPrice)
                    .WithMany()
                    .HasForeignKey(p => p.ProductPriceId);

            builder.Property(p => p.Amount)
                    .IsRequired();
        }
    }
}