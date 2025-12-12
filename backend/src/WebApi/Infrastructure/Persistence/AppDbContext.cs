using Microsoft.EntityFrameworkCore;
using WebApi.Domain.Entities;
using WebApi.Domain.ValueObjects;

namespace WebApi.Infrastructure.Persistence;

internal sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
    }

    internal static async Task SeedAsync(DbContext context, CancellationToken cancellationToken)
    {
        if (!context.Set<User>().Any())
        {
            context.Set<User>().Add(new User 
            {
                FirstName = "Juste", 
                LastName = "Leblanc", 
                PostalAddress = new PostalAddress(
                    Street: "233 Chem. des Grandes Terres",
                    PostalCode: "01250",
                    City:"Montagnat",
                    Country: "France"
                )
            });
            context.Set<User>().Add(new User
            {
                FirstName = "Marc",
                LastName = "Assin",
                PostalAddress = new PostalAddress(
                    Street: "233 Chem. des Grandes Terres",
                    PostalCode: "01250",
                    City:"Montagnat",
                    Country: "France"
                )
            });
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}