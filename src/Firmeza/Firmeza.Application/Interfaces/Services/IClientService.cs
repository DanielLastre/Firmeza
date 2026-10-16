using Firmeza.Application.DTOs.Clients;
using Firmeza.Application.DTOs.Common;

namespace Firmeza.Application.Interfaces.Services;

public interface IClientService
{
    Task<IEnumerable<ClientResponseDto>> GetAllAsync();
    Task<IEnumerable<ClientResponseDto>> GetAllPagedAsync(int pageNumber, int pageSize);
    Task<PagedResultDto<ClientResponseDto>> GetPagedAsync(int pageNumber, int pageSize, bool? onlyActive = null);
    Task<ClientResponseDto?> GetByIdAsync(Guid id);
    Task<ClientResponseDto> CreateAsync(CreateClientDto createClientDto);
    Task<ClientResponseDto> UpdateAsync(Guid id, UpdateClientDto updateClientDto);
    Task DeleteLogicalAsync(Guid id);
}
