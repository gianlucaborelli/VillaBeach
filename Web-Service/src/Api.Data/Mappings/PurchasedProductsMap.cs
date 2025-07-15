using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class PurchasedProductsMap : IEntityTypeConfiguration<PurchasedProduct>
    {
        public void Configure(EntityTypeBuilder<PurchasedProduct> builder)
        {
            builder.ToTable("PurchasedProducts");

            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.Id);

            builder.HasOne(p => p.Product)
                    .WithMany()
                    .HasForeignKey(p => p.ProductId);

            builder.Property(p => p.Amount)
                    .IsRequired();

            builder.Property(p => p.Price)
                    .IsRequired()
                    .HasPrecision(12, 2);
        }
    }
}