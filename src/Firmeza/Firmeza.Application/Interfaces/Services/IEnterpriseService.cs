using Firmeza.Application.DTOs.Common;
using Firmeza.Application.DTOs.Enterprises;

namespace Firmeza.Application.Interfaces.Services;

public interface IEnterpriseService
{
    Task<IEnumerable<EnterpriseResponseDto>> GetAllAsync();
    Task<IEnumerable<EnterpriseResponseDto>> GetAllPagedAsync(int pageNumber, int pageSize);
    Task<PagedResultDto<EnterpriseResponseDto>> GetPagedAsync(int pageNumber, int pageSize, bool? onlyActive = null);
    Task<EnterpriseResponseDto?> GetByIdAsync(Guid id);
    Task<EnterpriseResponseDto> CreateAsync(CreateEnterpriseDto createEnterpriseDto);
    Task<EnterpriseResponseDto> UpdateAsync(Guid id, UpdateEnterpriseDto updateEnterpriseDto);
    Task DeleteLogicalAsync(Guid id);
}
