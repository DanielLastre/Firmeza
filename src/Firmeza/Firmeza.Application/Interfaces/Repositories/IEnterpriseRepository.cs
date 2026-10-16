using Firmeza.Domain.Entities;

namespace Firmeza.Application.Interfaces.Repositories;

public interface IEnterpriseRepository
{
    Task<IEnumerable<Enterprise>> GetAllAsync();
    Task<IEnumerable<Enterprise>> GetAllPagedAsync(int pageNumber, int pageSize);
    Task<(IEnumerable<Enterprise> Items, int TotalRecords)> GetPagedAsync(int pageNumber, int pageSize, bool? onlyActive = null);
    Task<Enterprise?> GetByIdAsync(Guid id);
    Task AddAsync(Enterprise enterprise);
    Task UpdateAsync(Enterprise enterprise);
    Task DeleteLogicalAsync(Guid id);
    Task<bool> ExistsByTaxIdAsync(string taxId, Guid? excludeId = null);
    Task<bool> ExistsByEmailAsync(string email, Guid? excludeId = null);
}
