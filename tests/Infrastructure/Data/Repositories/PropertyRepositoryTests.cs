using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

using Infrastructure.Repositories;

namespace RealEstateApi.tests.Infrastructure.Data.Repositories
{
    /// <summary>
    /// Test class for PropertyRepository to ensure that it correctly handles property-related operations such as 
    /// adding, updating, and retrieving properties.
    /// </summary>
    [TestFixture]
    public class PropertyRepositoryTests
    {
        /// <summary>
        /// Test to verify that the AddAsync method successfully adds a new property to the database.
        /// This test uses an in-memory database to simulate the real database behavior.
        /// </summary>
        [Test]
        public async Task AddAsync_ShouldAddPropertyToDbContext()
        {
            var options = new DbContextOptionsBuilder<RealEstateDbContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;

            var context = new RealEstateDbContext(options);

            var repository = new PropertyRepository(context);

            var newProperty = new Property
            {
                Name = "Torre 8",
                Address = "IIgsb",
                Price = 2500000,
                CodeInternal = "Torr-233",
                Year = 2021,
                IdOwner = 9
            };

            await repository.AddAsync(newProperty);

            var savedProperty = await context.Properties.FirstOrDefaultAsync(p => p.CodeInternal == "Torr-233");
            Assert.IsNotNull(savedProperty);
            Assert.AreEqual("Torre 8", savedProperty.Name);
        }

        /// <summary>
        /// Test to verify that AddImageAsync correctly adds an image to an existing property.
        /// </summary>
        [Test]
        public async Task AddImageAsync_ShouldAddImageToProperty_WhenPropertyExists()
        {
            var options = new DbContextOptionsBuilder<RealEstateDbContext>().UseInMemoryDatabase(databaseName: "TestDatabase_AddImage").Options;

            using var context = new RealEstateDbContext(options);

            var property = new Property { IdProperty = 1, Name = "Test Property", PropertyImages = new List<PropertyImage>() };
            context.Properties.Add(property);
            await context.SaveChangesAsync();

            var repository = new PropertyRepository(context);

            var image = new PropertyImage { File = "image1.jpg" };
            await repository.AddImageAsync(1, image);

            var updatedProperty = await context.Properties.Include(p => p.PropertyImages).FirstOrDefaultAsync(p => p.IdProperty == 1);
            Assert.That(updatedProperty.PropertyImages, Contains.Item(image));
        }

        /// <summary>
        /// Test to verify that the UpdateAsync method successfully updates an existing property in the database.
        /// </summary>
        [Test]
        public async Task UpdateAsync_ShouldUpdatePropertyInDbContext()
        {
            var options = new DbContextOptionsBuilder<RealEstateDbContext>().UseInMemoryDatabase(databaseName: "TestDatabase_Update").Options;

            using var context = new RealEstateDbContext(options);

            var property = new Property { IdProperty = 1, Name = "Old Property" };
            context.Properties.Add(property);
            await context.SaveChangesAsync();

            var repository = new PropertyRepository(context);

            property.Name = "Updated Property";
            await repository.UpdateAsync(property);

            var updatedProperty = await context.Properties.FirstOrDefaultAsync(p => p.IdProperty == 1);
            Assert.AreEqual("Updated Property", updatedProperty.Name);
        }

        /// <summary>
        /// Test to verify that GetAllAsync returns properties with the correct filters and pagination applied.
        /// </summary>
        [Test]
        public async Task GetAllAsync_ShouldReturnProperties_WithFiltersAndPagination()
        {
            var options = new DbContextOptionsBuilder<RealEstateDbContext>().UseInMemoryDatabase(databaseName: "TestDatabase_GetAll").Options;

            using var context = new RealEstateDbContext(options);

            var properties = new List<Property>
            {
              new Property { IdProperty = 1, Name = "Property A", Price = 100000m },
              new Property { IdProperty = 2, Name = "Property B", Price = 200000m }
            };
            context.Properties.AddRange(properties);
            await context.SaveChangesAsync();

            var repository = new PropertyRepository(context);

            var result = await repository.GetAllAsync("Property", 50000, 150000, 1, 10);

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.AreEqual("Property A", result.First().Name);
        }
    }
}