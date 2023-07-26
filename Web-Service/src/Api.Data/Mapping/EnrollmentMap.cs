using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class EnrollmentMap : IEntityTypeConfiguration<EnrollmentEntity>
    {
        public void Configure(EntityTypeBuilder<EnrollmentEntity> builder)
        {
            builder.ToTable("Enrollments");

            builder.HasKey(e => e.Id);

            builder.HasIndex(e => e.Id);

            builder.HasOne(e => e.User)
                    .WithMany(u => u.EnrollmentList)
                    .HasForeignKey(e => e.UserId);

            builder.HasOne(e => e.Tuition)
                    .WithMany()
                    .HasForeignKey(e => e.TuitionId);

            builder.Property(a => a.Deadline)
                    .IsRequired();
        }
    }
}