using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class ProductPriceMap : IEntityTypeConfiguration<ProductPrice>
    {
        public void Configure(EntityTypeBuilder<ProductPrice> builder)
        {
            builder.ToTable("ProductPrice");

            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.Id);  

            builder.HasOne(p => p.Product)
                .WithMany()
                .HasForeignKey(p => p.ProductId);

            
        }
    }
}