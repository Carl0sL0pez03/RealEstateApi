using Domain.Entities;

using Microsoft.EntityFrameworkCore;

/// <summary>
/// Represents the database context for the real estate application.
/// Manages the entities and their relationships in the database using Entity Framework Core.
/// </summary>
public class RealEstateDbContext(DbContextOptions<RealEstateDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Gets or sets the <see cref="DbSet{Property}"/> for the properties.
    /// </summary>
    public DbSet<Property> Properties { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{Owner}"/> for the owners.
    /// </summary>
    public DbSet<Owner> Owners { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{PropertyImage}"/> for the property images.
    /// </summary>
    public DbSet<PropertyImage> PropertyImages { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{PropertyTrace}"/> for the property traces (sales history).
    /// </summary>
    public DbSet<PropertyTrace> PropertyTraces { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{User}"/> for the users.
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Configures the entity relationships and keys for the database model.
    /// </summary>
    /// <param name="modelBuilder">The builder used to construct the model.</param>
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