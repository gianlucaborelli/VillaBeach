using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class PurchaseMap : IEntityTypeConfiguration<PurchaseEntity>
    {
        public void Configure(EntityTypeBuilder<PurchaseEntity> builder)
        {
            builder.ToTable("Purchases");

            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.Id);

            builder.HasOne(p => p.User)
                    .WithMany(u => u.PurchasesList)
                    .HasForeignKey(p => p.UserId);
        }
    }
}