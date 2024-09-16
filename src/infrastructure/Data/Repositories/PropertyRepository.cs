using Domain.Repositories;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PropertyRepository(RealEstateDbContext dbContext) : IPropertyRepository
    {

        private readonly RealEstateDbContext _dbContext = dbContext;

        /* Create new property */
        public async Task AddAsync(Property property)
        {
            await _dbContext.Properties.AddAsync(property);
            await _dbContext.SaveChangesAsync();
        }
        /* Create new property */

        /* Add Img */
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
        /* Add Img */

        /* Change price */
        public async Task ChangePriceAsync(int propertyId, decimal newPrice)
        {
            var property = await _dbContext.Properties.FindAsync(propertyId) ?? throw new Exception("Property not found.");

            property.Price = newPrice;
            await _dbContext.SaveChangesAsync();
        }
        /* Change price */

        /* Update property */
        public async Task UpdateAsync(Property property)
        {
            _dbContext.Properties.Update(property);
            await _dbContext.SaveChangesAsync();
        }
        /* Update property */

        /* List all */
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
        /* List all */
    }
}