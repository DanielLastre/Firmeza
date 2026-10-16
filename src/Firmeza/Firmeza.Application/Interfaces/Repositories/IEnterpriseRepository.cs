using Firmeza.Domain.Entities;

namespace Firmeza.Application.Interfaces.Repositories;

public interface IEnterpriseRepository
{
    Task<IEnumerable<Enterprise>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Enterprise>> GetAllPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<(IEnumerable<Enterprise> Items, int TotalRecords)> GetPagedAsync(int pageNumber, int pageSize, bool? onlyActive = null, CancellationToken cancellationToken = default);
    Task<Enterprise?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Enterprise enterprise, CancellationToken cancellationToken = default);
    Task UpdateAsync(Enterprise enterprise, CancellationToken cancellationToken = default);
    Task DeleteLogicalAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsByTaxIdAsync(string taxId, Guid? excludeId = null, CancellationToken cancellationToken = default);
    Task<bool> ExistsByEmailAsync(string email, Guid? excludeId = null, CancellationToken cancellationToken = default);
}
