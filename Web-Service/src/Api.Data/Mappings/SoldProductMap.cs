using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class SoldProductMap : IEntityTypeConfiguration<SoldProduct>
    {
        public void Configure(EntityTypeBuilder<SoldProduct> builder)
        {
            builder.ToTable("SoldProducts");

            builder.HasKey(s => s.Id);

            builder.HasIndex(s => s.Id);

            builder.HasOne(s => s.Product)
                    .WithMany()
                    .HasForeignKey(s => s.ProductId);
            
            builder.HasOne(s => s.ProductPrice)
                    .WithMany()
                    .HasForeignKey(p => p.ProductPriceId);

            builder.Property(s => s.Amount)
                    .IsRequired();
        }
    }
}