using Firmeza.Domain.Entities;

namespace Firmeza.Application.Interfaces.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<IEnumerable<Product>> GetAllPagedAsync(int pageNumber, int pageSize);
    Task<Product?> GetByIdAsync(Guid id);
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteLogicalAsync(Guid id);
}
