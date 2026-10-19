using Firmeza.Application.DTOs.Sales;

namespace Firmeza.Application.Interfaces.Services;

public interface ISaleService
{
    Task<IEnumerable<SaleResponseDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<SaleResponseDto>> GetAllPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default);
    Task<SaleResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<SaleResponseDto> CreateAsync(CreateSaleDto dto, CancellationToken cancellationToken = default);
    Task<SaleResponseDto> UpdateAsync(Guid id, UpdateSaleDto dto, CancellationToken cancellationToken = default);
    Task DeleteLogicalAsync(Guid id, CancellationToken cancellationToken = default);
}
