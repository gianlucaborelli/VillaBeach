using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.HasIndex(u => u.Id)
                    .IsUnique();

            builder.Property(u => u.Name)
                    .IsRequired()
                    .HasMaxLength(150);
            
            builder.HasAlternateKey(u => u.Email);

            builder.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(150);

            builder.OwnsOne(u => u.Settings).ToTable("UserSettings");

            builder.OwnsMany(u => u.AddressList, a =>
                {
                    a.WithOwner().HasForeignKey(a => a.Id);
                    a.HasKey(a => a.Id);
                    a.ToTable("UserAddresses");
                }
            );

            builder.OwnsMany(u => u.ContactList, a =>
                {
                    a.WithOwner().HasForeignKey(a => a.Id);
                    a.HasKey(a => a.Id);
                    a.ToTable("UserContacts");
                }
            );
        }
    }
}