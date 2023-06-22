using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Mapping
{
    public class PlanPriceMap : IEntityTypeConfiguration<PlanPriceEntity>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<PlanPriceEntity> builder)
        {
            builder.ToTable("PlanPrices");

            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.Current);

            builder.HasOne(p => p.Plan)
                    .WithMany(u => u.Prices)
                    .HasForeignKey(p => p.PlanId);

            builder.Property(p => p.Current)
                    .IsRequired();

            builder.Property(p => p.Value)
                    .IsRequired();
        }
    }
}