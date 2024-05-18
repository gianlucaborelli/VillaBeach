using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class ProductPriceMap : IEntityTypeConfiguration<ProductPrice>
    {
        public void Configure(EntityTypeBuilder<ProductPrice> builder)
        {
            builder.ToTable("ProductPrices");

            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.Id);

            builder.HasOne(pp => pp.Product)
                .WithMany(p => p.Prices)
                .HasForeignKey(pp => pp.ProductId);

            builder.Property(c => c.Value)
                .IsRequired()
                .HasPrecision(12,2);

            builder.Property(p => p.Current)
                    .IsRequired();

        }
    }
}