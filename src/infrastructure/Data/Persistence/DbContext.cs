using Domain.Entities;

using Microsoft.EntityFrameworkCore;

public class RealEstateDbContext(DbContextOptions<RealEstateDbContext> options) : DbContext(options)
{
    public DbSet<Property> Properties { get; set; }
    public DbSet<Owner> Owners { get; set; }
    public DbSet<PropertyImage> PropertyImages { get; set; }
    public DbSet<PropertyTrace> PropertyTraces { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Property>().HasKey(o => o.IdProperty);
        modelBuilder.Entity<Owner>().HasKey(o => o.IdOwner);
        modelBuilder.Entity<PropertyImage>().HasKey(o => o.IdPropertyImage);
        modelBuilder.Entity<PropertyTrace>().HasKey(o => o.IdPropertyTrace);
        modelBuilder.Entity<User>().HasKey(o => o.Id);


        modelBuilder.Entity<Property>().HasOne(p => p.Owner).WithMany(o => o.Properties).HasForeignKey(p => p.IdOwner);

        modelBuilder.Entity<PropertyImage>().HasOne(pi => pi.Property).WithMany(p => p.PropertyImages).HasForeignKey(pi => pi.IdProperty);

        modelBuilder.Entity<PropertyTrace>().HasOne(pt => pt.Property).WithMany(p => p.PropertyTraces).HasForeignKey(pt => pt.IdProperty);

        base.OnModelCreating(modelBuilder);
    }
}