using Microsoft.EntityFrameworkCore;
using NoPrint.Customers.Domain.Model;
using NoPrint.Invoices.Domain.Models;
using NoPrint.Invoices.Domain.ValueObjects;
using NoPrint.Shops.Domain.Models;
using NoPrint.Users.Domain.Models;

namespace NoPrint.Ef;

public class NoPrintContext : DbContext
{
    public DbSet<Shop> Shops { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<UserBase> Users { get; set; }

    public NoPrintContext(DbContextOptions<NoPrintContext> options):base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Shop>().OwnsOne(x => x.User);
        modelBuilder.Entity<Customer>().OwnsOne(x => x.User);
        modelBuilder.Entity<UserBase>(o =>
        {
            o.OwnsOne(x => x.ExpireDate);
            o.OwnsOne(x => x.User).ToTable("Users_up");
            o.OwnsOne(x => x.Visitor).ToTable("Visitors");
        });
        modelBuilder.Entity<Invoice>(o =>
        {
            o.OwnsOne(x => x.Customer);
            o.OwnsOne(x => x.Shop);
            o.OwnsMany<InvoiceItem>("Items");
        });
    }
}