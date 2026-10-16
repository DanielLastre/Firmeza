using Firmeza.Application.DTOs.Common;
using Firmeza.Application.DTOs.Enterprises;

namespace Firmeza.Application.Interfaces.Services;

public interface IEnterpriseService
{
    Task<IEnumerable<EnterpriseResponseDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<EnterpriseResponseDto>> GetAllPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<PagedResultDto<EnterpriseResponseDto>> GetPagedAsync(int pageNumber, int pageSize, bool? onlyActive = null, CancellationToken cancellationToken = default);
    Task<EnterpriseResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<EnterpriseResponseDto> CreateAsync(CreateEnterpriseDto createEnterpriseDto, CancellationToken cancellationToken = default);
    Task<EnterpriseResponseDto> UpdateAsync(Guid id, UpdateEnterpriseDto updateEnterpriseDto, CancellationToken cancellationToken = default);
    Task DeleteLogicalAsync(Guid id, CancellationToken cancellationToken = default);
}
