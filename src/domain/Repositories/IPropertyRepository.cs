namespace Domain.Repositories
{
    /// <summary>
    /// Interface that defines the operations for managing properties, 
    /// including retrieving, adding, updating, changing prices, and adding images to properties.
    /// </summary>
    public interface IPropertyRepository
    {
        /// <summary>
        /// Retrieves a list of properties with optional filtering by name, price range, and pagination.
        /// </summary>
        /// <param name="filterName">Optional filter for property name.</param>
        /// <param name="minPrice">Optional filter for minimum property price.</param>
        /// <param name="maxPrice">Optional filter for maximum property price.</param>
        /// <param name="page">The page number for pagination.</param>
        /// <param name="pageSize">The number of properties per page.</param>
        /// <returns>A list of properties that match the specified filters and pagination settings.</returns>
        Task<List<Property>> GetAllAsync(string filterName, decimal? minPrice, decimal? maxPrice, int page, int pageSize);

        /// <summary>
        /// Adds a new property to the repository.
        /// </summary>
        /// <param name="property">The property to be added.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddAsync(Property property);

        /// <summary>
        /// Updates an existing property in the repository.
        /// </summary>
        /// <param name="property">The property with updated details.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateAsync(Property property);

        /// <summary>
        /// Changes the price of an existing property.
        /// </summary>
        /// <param name="id">The ID of the property whose price will be changed.</param>
        /// <param name="newPrice">The new price to set for the property.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task ChangePriceAsync(int id, decimal newPrice);

        /// <summary>
        /// Adds an image to a specified property.
        /// </summary>
        /// <param name="propertyId">The ID of the property to which the image will be added.</param>
        /// <param name="image">The image to be added to the property.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddImageAsync(int propertyId, PropertyImage image);
    }
}
