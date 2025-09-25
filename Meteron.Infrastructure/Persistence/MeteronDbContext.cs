using Meteron.Domain.Customers;
using Microsoft.EntityFrameworkCore;

namespace Meteron.Infrastructure.Persistence;

public sealed class MeteronDbContext(DbContextOptions<MeteronDbContext> options) : DbContext(options)
{
    public DbSet<Customer> Customers => Set<Customer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(cfg =>
        {
            cfg.ToTable("Customer");
            cfg.HasKey(c => c.Id);
            cfg.Property(c => c.Id)
                .HasConversion(id => id.Value, v => new CustomerId(v));
            cfg.Property(c => c.Email)
                .HasConversion(e => e.Value, v => new Email(v))
                .IsRequired();
        });

        modelBuilder.Entity<HouseholdCustomer>(cfg =>
        {
            cfg.ToTable("HouseholdCustomer");
            cfg.Property(h => h.FirstName)
                .IsRequired();
            cfg.Property(h => h.LastName)
                .IsRequired();
            cfg.Property(h => h.DateOfBirth);
        });

        modelBuilder.Entity<BusinessCustomer>(cfg =>
        {
            cfg.ToTable("BusinessCustomer");
            cfg.Property(b => b.Name)
                .IsRequired();
            cfg.Property(b => b.RegistrationNumber)
                .IsRequired();
        });
    }
}