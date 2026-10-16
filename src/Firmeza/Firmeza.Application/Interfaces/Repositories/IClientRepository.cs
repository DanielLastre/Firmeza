using Firmeza.Domain.Entities;

namespace Firmeza.Application.Interfaces.Repositories;

public interface IClientRepository
{
    Task<IEnumerable<Client>> GetAllAsync();
    Task<IEnumerable<Client>> GetAllPagedAsync(int pageNumber, int pageSize);
    Task<(IEnumerable<Client> Items, int TotalRecords)> GetPagedAsync(int pageNumber, int pageSize, bool? onlyActive = null);
    Task<Client?> GetByIdAsync(Guid id);
    Task AddAsync(Client client);
    Task UpdateAsync(Client client);
    Task DeleteLogicalAsync(Guid id);
    Task<bool> ExistsByEmailAsync(string email, Guid? excludeId = null);
    Task<bool> ExistsByDocumentAsync(string document, Guid? excludeId = null);
}
