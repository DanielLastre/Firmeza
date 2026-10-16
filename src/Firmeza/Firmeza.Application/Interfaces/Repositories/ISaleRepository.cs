using Firmeza.Domain.Entities;

namespace Firmeza.Application.Interfaces.Repositories;

public interface ISaleRepository
{
    Task<IEnumerable<Sale>> GetAllAsync();
    Task<IEnumerable<Sale>> GetAllPagedAsync(int pageNumber, int pageSize);
    Task<Sale?> GetByIdAsync(Guid id);
    Task AddAsync(Sale sale);
    Task UpdateAsync(Sale sale);
    Task DeleteLogicalAsync(Guid id);
}
