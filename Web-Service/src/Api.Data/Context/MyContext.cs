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
        public DbSet<ProductPriceEntity> ProductPrices => Set<ProductPriceEntity>();
        public DbSet<PlanPriceEntity> PlanPrices => Set<PlanPriceEntity>();
        public DbSet<PurchasedProductEntity> PurchasedProducts => Set<PurchasedProductEntity>();
        public DbSet<SoldProductEntity> SoldProducts => Set<SoldProductEntity>();
        public DbSet<SaleEntity> Sales => Set<SaleEntity>();
        public DbSet<PurchaseEntity> Purchases => Set<PurchaseEntity>();
        public DbSet<ProductEntity> Products => Set<ProductEntity>();
        public DbSet<PlanEntity> Plans => Set<PlanEntity>();
        public DbSet<EnrollmentEntity> Enrollments => Set<EnrollmentEntity>();
        public DbSet<TuitionEntity> Tuitions => Set<TuitionEntity>();

        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<EnrollmentEntity>(new EnrollmentMap().Configure);
            modelBuilder.Entity<PlanEntity>(new PlanMap().Configure);
            modelBuilder.Entity<PlanPriceEntity>(new PlanPriceMap().Configure);
            modelBuilder.Entity<ProductEntity>(new ProductMap().Configure);
            modelBuilder.Entity<ProductPriceEntity>(new ProductPriceMap().Configure);
            modelBuilder.Entity<PurchaseEntity>(new PurchaseMap().Configure);            
            modelBuilder.Entity<PurchasedProductEntity>(new PurchasedProductsMap().Configure);
            modelBuilder.Entity<SaleEntity>(new SaleMap().Configure);
            modelBuilder.Entity<SoldProductEntity>(new SoldProductMap().Configure);
            modelBuilder.Entity<UserEntity>(new UserMap().Configure);
        }
    }
}