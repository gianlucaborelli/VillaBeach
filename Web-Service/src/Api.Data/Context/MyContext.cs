using Api.Core.Data;
using Api.Core.Domain;
using Api.Core.Events.Messaging;
using Api.Core.Mediator;
using Api.Data.Mapping;
using Api.Domain.Entities;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Context
{
    public class MyContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;
        
        public DbSet<User> Users => Set<User>();        
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

        public MyContext(DbContextOptions<MyContext> options, IMediatorHandler mediatorHandler) : base(options)
        {
            _mediatorHandler = mediatorHandler;

            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();
            
            modelBuilder.Entity<Enrollment>(new EnrollmentMap().Configure);
            modelBuilder.Entity<Plan>(new PlanMap().Configure);
            modelBuilder.Entity<PlanPrice>(new PlanPriceMap().Configure);
            modelBuilder.Entity<Product>(new ProductMap().Configure);
            modelBuilder.Entity<ProductPrice>(new ProductPriceMap().Configure);
            modelBuilder.Entity<Purchase>(new PurchaseMap().Configure);            
            modelBuilder.Entity<PurchasedProduct>(new PurchasedProductsMap().Configure);
            modelBuilder.Entity<Sale>(new SaleMap().Configure);
            modelBuilder.Entity<SoldProduct>(new SoldProductMap().Configure);
            modelBuilder.Entity<User>(new UserMap().Configure);
        }

        public async Task<bool> Commit() { 
            await _mediatorHandler.PublishDomainEvents(this).ConfigureAwait(false);

            var success = await SaveChangesAsync() > 0;

            return success;
        }
    }

    public static class MediatorExtension
    {
        public static async Task PublishDomainEvents<T>(this IMediatorHandler mediator, T ctx) where T : DbContext
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) => {
                    await mediator.PublishEvent(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
    
}