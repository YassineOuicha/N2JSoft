using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Domain.Entities;

namespace WebApi.Infrastructure.Persistence;

internal sealed class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
        builder.Property(x => x.LastName).HasMaxLength(100).IsRequired();
        builder.OwnsOne(x => x.PostalAddress, owned =>
        {
            owned.Property(x => x.Street).HasMaxLength(200).IsRequired();
            owned.Property(x => x.PostalCode).HasMaxLength(20).IsRequired();
            owned.Property(x => x.City).HasMaxLength(100).IsRequired();
            owned.Property(x => x.Country).HasMaxLength(100).IsRequired();
        });

        builder.Property(x => x.MonthlyExpenseLimit).IsRequired();
        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.IsDeleted).IsRequired();
    }
}