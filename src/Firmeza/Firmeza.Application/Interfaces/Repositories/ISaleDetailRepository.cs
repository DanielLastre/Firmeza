using Firmeza.Domain.Entities;

namespace Firmeza.Application.Interfaces.Repositories;

public interface ISaleDetailRepository
{
    Task<IEnumerable<SaleDetail>> GetAllAsync();
    Task<IEnumerable<SaleDetail>> GetAllPagedAsync(int pageNumber, int pageSize);
    Task<SaleDetail?> GetByIdAsync(Guid id);
    Task AddAsync(SaleDetail saleDetail);
    Task UpdateAsync(SaleDetail saleDetail);
    Task DeleteLogicalAsync(Guid id);
}
