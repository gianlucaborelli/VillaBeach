using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class AddressMap : IEntityTypeConfiguration<AddressEntity>
    {
        public void Configure(EntityTypeBuilder<AddressEntity> builder)
        {
            builder.ToTable("Addresses");

            builder.HasKey(a => a.Id);

            builder.HasIndex(a => a.PostalCode);

            builder.HasOne(a => a.User)
                    .WithMany(u => u.AddressList)
                    .HasForeignKey(a => a.UserId);

            builder.Property(a => a.PostalCode)
                    .IsRequired()
                    .HasMaxLength(8);

            builder.Property(a => a.Street)
                    .IsRequired()
                    .HasMaxLength(100);

            builder.Property(a => a.Number)
                    .IsRequired()
                    .HasMaxLength(10);

            builder.Property(a => a.District)
                    .IsRequired()
                    .HasMaxLength(100);

            builder.Property(a => a.City)
                    .IsRequired()
                    .HasMaxLength(50);

            builder.Property(a => a.State)
                    .IsRequired()
                    .HasMaxLength(2);

            builder.Property(a => a.UserId)
                    .IsRequired();

            builder.Property(a => a.Description)
                    .HasMaxLength(100);
        }
    }
}