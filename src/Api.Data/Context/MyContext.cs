using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Mapping;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Context
{
    public class MyContext : DbContext
    {
        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<Address> Addresses => Set<Address>();
        public DbSet<Contact> Contacts => Set<Contact>();
        public DbSet<ProductPrice> ProductPrices => Set<ProductPrice>();
        public DbSet<PlanPrice> PlanPrices => Set<PlanPrice>();
        public DbSet<PurchasedProduct> PurchasedProducts => Set<PurchasedProduct>();
        public DbSet<SoldProduct> SoldProducts => Set<SoldProduct>();
        public DbSet<Sale> Sales => Set<Sale>();
        public DbSet<Purchase> Purchases => Set<Purchase>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Plan> Plans => Set<Plan>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();
        public DbSet<Tuition> Tuitions => Set<Tuition>();

        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Address>(new AddressMap().Configure);
            modelBuilder.Entity<Contact>(new ContactMap().Configure);
            modelBuilder.Entity<Enrollment>(new EnrollmentMap().Configure);
            modelBuilder.Entity<Plan>(new PlanMap().Configure);
            modelBuilder.Entity<PlanPrice>(new PlanPriceMap().Configure);
            modelBuilder.Entity<Product>(new ProductMap().Configure);
            modelBuilder.Entity<ProductPrice>(new ProductPriceMap().Configure);
            modelBuilder.Entity<Purchase>(new PurchaseMap().Configure);            
            modelBuilder.Entity<PurchasedProduct>(new PurchasedProductsMap().Configure);
            modelBuilder.Entity<Sale>(new SaleMap().Configure);
            modelBuilder.Entity<SoldProduct>(new SoldProductMap().Configure);
            modelBuilder.Entity<UserEntity>(new UserMap().Configure);
        }
    }
}