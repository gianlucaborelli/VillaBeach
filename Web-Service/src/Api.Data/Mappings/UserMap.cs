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

            builder.OwnsOne(e => e.Authentication).ToTable("UserAuthentications");

            builder.OwnsOne(u => u.Settings).ToTable("UserSettings");

            builder.OwnsOne(u => u.Email).ToTable("UserEmails");

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