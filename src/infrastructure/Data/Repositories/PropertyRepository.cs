using Domain.Repositories;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// Repository that implements the property management operations,
    /// such as creating, updating, listing, changing prices, and adding images to properties.
    /// </summary>
    public class PropertyRepository(RealEstateDbContext dbContext) : IPropertyRepository
    {

        private readonly RealEstateDbContext _dbContext = dbContext;

        /// <summary>
        /// Adds a new property to the database.
        /// </summary>
        /// <param name="property">The property entity to be added.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task AddAsync(Property property)
        {
            await _dbContext.Properties.AddAsync(property);
            await _dbContext.SaveChangesAsync();
        }


        /// <summary>
        /// Adds an image to a specified property.
        /// </summary>
        /// <param name="propertyId">The ID of the property to which the image will be added.</param>
        /// <param name="image">The image to be added to the property.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown if the property is not found.</exception>
        public async Task AddImageAsync(int propertyId, PropertyImage image)
        {
            var property = await _dbContext.Properties.FindAsync(propertyId)
        ?? throw new Exception("Property not found.");

            if (property.PropertyImages == null)
            {
                property.PropertyImages = new List<PropertyImage>();
            }

            property.PropertyImages.Add(image);

            await _dbContext.SaveChangesAsync();
        }


        /// <summary>
        /// Changes the price of an existing property.
        /// </summary>
        /// <param name="propertyId">The ID of the property whose price will be updated.</param>
        /// <param name="newPrice">The new price to be set for the property.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="Exception">Thrown if the property is not found.</exception>
        public async Task ChangePriceAsync(int propertyId, decimal newPrice)
        {
            var property = await _dbContext.Properties.FindAsync(propertyId) ?? throw new Exception("Property not found.");

            property.Price = newPrice;
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing property in the database.
        /// </summary>
        /// <param name="property">The property entity with updated details.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task UpdateAsync(Property property)
        {
            _dbContext.Properties.Update(property);
            await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves a list of properties with optional filtering by name and price, with pagination support.
        /// </summary>
        /// <param name="filterName">Optional filter by property name.</param>
        /// <param name="minPrice">Optional filter by minimum property price.</param>
        /// <param name="maxPrice">Optional filter by maximum property price.</param>
        /// <param name="page">The page number for pagination.</param>
        /// <param name="pageSize">The number of properties per page.</param>
        /// <returns>A list of properties that match the specified filters and pagination.</returns>
        public async Task<List<Property>> GetAllAsync(string filterName, decimal? minPrice, decimal? maxPrice, int page, int pageSize)
        {
            var query = _dbContext.Properties.AsQueryable();

            if (!string.IsNullOrEmpty(filterName))
                query = query.Where(p => p.Name.Contains(filterName));

            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice.Value);

            query = query.OrderBy(p => p.IdProperty);

            return await query.Skip((page - 1) * pageSize)
                      .Take(pageSize)
                      .ToListAsync();
        }

    }
}