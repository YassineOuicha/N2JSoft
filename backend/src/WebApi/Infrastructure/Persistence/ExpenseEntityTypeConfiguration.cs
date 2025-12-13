using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Domain.Entities;

namespace WebApi.Infrastructure.Persistence;

internal sealed class ExpenseEntityTypeConfiguration: IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.ToTable("Expenses");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Amount).HasColumnType("decimal(18,2)").IsRequired();
        builder.Property(e => e.Description).HasMaxLength(50).IsRequired();
        builder.Property(e => e.IsDeleted).IsRequired();

        builder.OwnsOne(x => x.BillingAddress, owned =>
        {
            owned.Property(x => x.Brand).HasMaxLength(100).IsRequired();
            owned.Property(x => x.Street).HasMaxLength(200).IsRequired();
            owned.Property(x => x.PostalCode).HasMaxLength(20).IsRequired();
            owned.Property(x => x.City).HasMaxLength(100).IsRequired();
        });

        builder.HasIndex(x => x.ExpenseReportId);
    }
}