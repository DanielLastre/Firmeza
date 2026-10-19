using Firmeza.Domain.Entities;

namespace Firmeza.Application.Interfaces.Repositories;

public interface ISaleDetailRepository
{
    Task<IEnumerable<SaleDetail>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<SaleDetail>> GetAllPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<SaleDetail?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(SaleDetail saleDetail, CancellationToken cancellationToken = default);
    Task UpdateAsync(SaleDetail saleDetail, CancellationToken cancellationToken = default);
    Task DeleteLogicalAsync(Guid id, CancellationToken cancellationToken = default);
}
