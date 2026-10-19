using Firmeza.Application.DTOs.Clients;
using Firmeza.Application.DTOs.Common;

namespace Firmeza.Application.Interfaces.Services;

public interface IClientService
{
    Task<IEnumerable<ClientResponseDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<ClientResponseDto>> GetAllPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<PagedResultDto<ClientResponseDto>> GetPagedAsync(int pageNumber, int pageSize, bool? onlyActive = null, CancellationToken cancellationToken = default);
    Task<ClientResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ClientResponseDto> CreateAsync(CreateClientDto createClientDto, CancellationToken cancellationToken = default);
    Task<ClientResponseDto> UpdateAsync(Guid id, UpdateClientDto updateClientDto, CancellationToken cancellationToken = default);
    Task DeleteLogicalAsync(Guid id, CancellationToken cancellationToken = default);
}
