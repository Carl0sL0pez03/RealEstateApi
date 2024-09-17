

using Domain.Repositories;

namespace Application.Services
{
    /// <summary>
    /// Service responsible for handling property-related operations,
    /// including creation, updating, price changes, image handling, and listing properties.
    /// </summary>
    public class PropertyService(IPropertyRepository propertyRepository)
    {
        private readonly IPropertyRepository _propertyRepository = propertyRepository;

        /// <summary>
        /// Creates a new property and saves it to the repository.
        /// </summary>
        /// <param name="property">The property object to be created.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task CreatePropertyAsync(Property property)
        {
            await _propertyRepository.AddAsync(property);
        }

        /// <summary>
        /// Adds an image to an existing property.
        /// </summary>
        /// <param name="propertyId">The ID of the property to which the image will be added.</param>
        /// <param name="image">The image to be added to the property.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task AddImageToPropertyAsync(int propertyId, PropertyImage image)
        {
            await _propertyRepository.AddImageAsync(propertyId, image);
        }

        /// <summary>
        /// Changes the price of an existing property.
        /// </summary>
        /// <param name="propertyId">The ID of the property whose price is to be changed.</param>
        /// <param name="newPrice">The new price to be set for the property.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task ChangePropertyPriceAsync(int propertyId, decimal newPrice)
        {
            await _propertyRepository.ChangePriceAsync(propertyId, newPrice);
        }

        /// <summary>
        /// Updates the details of an existing property.
        /// </summary>
        /// <param name="property">The property object with updated details.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task UpdatePropertyAsync(Property property)
        {
            await _propertyRepository.UpdateAsync(property);
        }

        /// <summary>
        /// Retrieves a list of properties, with optional filtering by name and price range, and pagination support.
        /// </summary>
        /// <param name="filterName">Optional filter for property name.</param>
        /// <param name="minPrice">Optional filter for minimum property price.</param>
        /// <param name="maxPrice">Optional filter for maximum property price.</param>
        /// <param name="page">The page number for pagination.</param>
        /// <param name="pageSize">The number of properties to retrieve per page.</param>
        /// <returns>A list of properties that match the specified filters and pagination settings.</returns>
        public async Task<List<Property>> ListPropertiesAsync(string filterName, decimal? minPrice, decimal? maxPrice, int page, int pageSize)
        {
            return await _propertyRepository.GetAllAsync(filterName, minPrice, maxPrice, page, pageSize);
        }
    }
}