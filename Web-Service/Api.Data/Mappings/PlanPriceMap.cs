using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class PlanPriceMap : IEntityTypeConfiguration<PlanPrice>
    {
        public void Configure(EntityTypeBuilder<PlanPrice> builder)
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