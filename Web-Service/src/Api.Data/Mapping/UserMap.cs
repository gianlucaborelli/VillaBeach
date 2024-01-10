using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.HasIndex(u => u.Id)
                   .IsUnique();

            builder.OwnsOne(e => e.Authentication).ToTable("UserAuthentications");  
                    
            builder.OwnsOne(e => e.Settings).ToTable("UserSettings");  

            builder.OwnsOne(e => e.Email).ToTable("UserEmails");   
        }
    }
}