using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class ContactMap : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable("Contacts");

            builder.HasKey(c => c.Id);

            builder.HasIndex(c => c.ContactForm);

            builder.HasOne(c => c.User)
                    .WithMany(u => u.ContactList)
                    .HasForeignKey(c => c.UserId);

            builder.Property(c => c.ContactForm)
                    .IsRequired()
                    .HasMaxLength(50);

            builder.Property(c => c.ContactType)
                    .IsRequired()
                    .HasMaxLength(2);

            builder.Property(c => c.Description)
                    .HasMaxLength(100);

            builder.Property(c => c.UserId)
                    .IsRequired();
        }
    }
}