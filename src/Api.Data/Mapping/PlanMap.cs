using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class PlanMap : IEntityTypeConfiguration<PlanEntity>
    {
        public void Configure(EntityTypeBuilder<PlanEntity> builder)
        {
            builder.ToTable("Plans");

            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.Name);

            builder.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(50);

            builder.Property(p => p.AmountOfDay)
                    .IsRequired();
            
            builder.Property(p => p.Description)
                    .HasMaxLength(150);
        }
    }
}