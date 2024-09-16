namespace Domain.Repositories
{
    public interface IPropertyRepository
    {
        Task<List<Property>> GetAllAsync(string filterName, decimal? minPrice, decimal? maxPrice, int page, int pageSize);
        Task AddAsync(Property proterty);
        Task UpdateAsync(Property proterty);
        Task ChangePriceAsync(int id, decimal newPrice);
        Task AddImageAsync(int propertyId, PropertyImage image);
    }
}