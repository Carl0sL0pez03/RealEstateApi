

using Domain.Repositories;

namespace Application.Services
{
    public class PropertyService
    {

        private readonly IPropertyRepository _propertyRepository;

        public PropertyService(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
        }

        /* Create New property */
        public async Task CreatePropertyAsync(Property property)
        {
            await _propertyRepository.AddAsync(property);
        }
        /* Create New property */

        /* Add Img */
        public async Task AddImageToPropertyAsync(int propertyId, PropertyImage image)
        {
            await _propertyRepository.AddImageAsync(propertyId, image);
        }
        /* Add Img */

        /* Change price */
        public async Task ChangePropertyPriceAsync(int propertyId, decimal newPrice)
        {
            await _propertyRepository.ChangePriceAsync(propertyId, newPrice);
        }
        /* Change price */

        /* Update property */
        public async Task UpdatePropertyAsync(Property property)
        {
            await _propertyRepository.UpdateAsync(property);
        }
        /* Update property */

        /* List All */
        public async Task<List<Property>> ListPropertiesAsync(string filterName, decimal? minPrice, decimal? maxPrice)
        {
            return await _propertyRepository.GetAllAsync(filterName, minPrice, maxPrice);
        }
        /* List All */

    }
}