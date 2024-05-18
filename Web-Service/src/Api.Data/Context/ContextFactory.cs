using Api.Core.Mediator;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context
{
    // public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    // {   
    //     public MyContext CreateDbContext(string[] args)
    //     {
    //         var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");
    //         var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
    //         optionsBuilder.UseNpgsql(connectionString);
    //         return new MyContext (optionsBuilder.Options, mediatorHandler);
    //     }
    // }
}