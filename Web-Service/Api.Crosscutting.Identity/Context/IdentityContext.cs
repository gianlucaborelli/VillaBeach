
using Api.CrossCutting.Identity.Authentication.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Api.CrossCutting.Identity.Data.Context
{
    public class IdentityContext(DbContextOptions<IdentityContext> options) : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>(options)
    {
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<RefreshToken>()
                    .HasIndex(rt => rt.UserId)
                    .IsUnique();

            SeedRoles(builder);
        }

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole<Guid>>().HasData
            (
               new IdentityRole<Guid>() { Name = "SuperAdmin", ConcurrencyStamp = "1", NormalizedName = "SUPERADMIN", Id = Guid.NewGuid() },
               new IdentityRole<Guid>() { Name = "Admin", ConcurrencyStamp = "2", NormalizedName = "ADMIN", Id = Guid.NewGuid() },
               new IdentityRole<Guid>() { Name = "User", ConcurrencyStamp = "3", NormalizedName = "USER", Id = Guid.NewGuid() }
            );
        }
    }
}