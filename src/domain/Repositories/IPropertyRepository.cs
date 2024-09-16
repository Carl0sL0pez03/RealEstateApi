namespace Domain.Repositories
{
    public interface IPropertyRepository
    {
        /* Task<Property> GetByIdAsync(int id); */
        Task<List<Property>> GetAllAsync(string filterName, decimal? minPrice, decimal? maxPrice);
        Task AddAsync(Property proterty);
        Task UpdateAsync(Property proterty);
        Task ChangePriceAsync(int id, decimal newPrice);
        Task AddImageAsync(int propertyId, PropertyImage image);
    }
}