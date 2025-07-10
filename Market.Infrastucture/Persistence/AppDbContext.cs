using Market.Domain.Entity.HR;
using Market.Domain.Entity.Main;
using Market.Domain.Entity.Management;
using Market.Domain.Entity.Operations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Market.Infrastucture.Persistence;

internal class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    // Define DbSets for your entities
    internal DbSet<Branch> Branches { get; set; } 
    internal DbSet<Inventory> Inventories { get; set; }
    internal DbSet<Product> Products { get; set; }
    internal DbSet<Category> Categories { get; set; }
    internal DbSet<Supplier> Suppliers { get; set; }
    internal DbSet<Contact> Contacts { get; set; }
    internal DbSet<CountryKey> CountryKeys { get; set; }
    internal DbSet<PurchaseInvoice> PurchaserInvoices { get; set; }
    internal DbSet<PurchaseDetail> PurchaseDetails { get; set; }
    internal DbSet<ProductStock> ProductStocks { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Models configurations
        // Branch Table Configuration
        modelBuilder.Entity<Branch>().Property(x => x.Id).HasDefaultValueSql(sql: "NEWID()");

        // Inventory Table Configuration
        modelBuilder.Entity<Inventory>().Property(x => x.Id).HasDefaultValueSql(sql: "NEWID()");
        // Product Table Configuration
        modelBuilder.Entity<Product>().Property(x => x.Id).HasDefaultValueSql(sql: "NEWID()");
        // Supplier Table Configuration
        modelBuilder.Entity<Supplier>().Property(x => x.Id).HasDefaultValueSql(sql: "NEWID()");

        // PurchaseInvoice Table Configuration
        modelBuilder.Entity<PurchaseInvoice>().Property(x => x.PurchaseDate).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd()
            .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

        // Purchase invoices details table configuration
        modelBuilder.Entity<PurchaseDetail>().ToTable(x => x.HasCheckConstraint("CK_PurchaseDetail_Quantity", "[Quantity] >= 0"));
        modelBuilder.Entity<PurchaseDetail>().Property(x => x.Quantity).HasDefaultValue(1);
        modelBuilder.Entity<PurchaseDetail>().Property(x => x.UnitPrice).HasDefaultValue(0.0m);
        modelBuilder.Entity<PurchaseDetail>().Property(x => x.TotalPrice).HasComputedColumnSql("[Quantity] * [UnitPrice]", stored: true);
        modelBuilder.Entity<PurchaseDetail>().Property(x => x.Status).HasDefaultValue(false);

        //ProductStock Table Configuration
        modelBuilder.Entity<ProductStock>().Property(x=>x.UnitDefaultPrice).HasComputedColumnSql("[DefaultCostPrice] * 1.15 ", stored: true);
        modelBuilder.Entity<ProductStock>().ToTable(x=> x.HasCheckConstraint("CK_ProductStock_QuantityAvailable", "[QuantityAvailable] >= 0"));

    }

}

