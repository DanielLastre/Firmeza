using Firmeza.Application.DTOs.Sales;

namespace Firmeza.Application.Interfaces.Services;

public interface ISaleService
{
    Task<IEnumerable<SaleResponseDto>> GetAllAsync();
    Task<IEnumerable<SaleResponseDto>> GetAllPagedAsync(int pageNumber, int pageSize);
    Task<SaleResponseDto?> GetByIdAsync(Guid id);
    Task<SaleResponseDto> CreateAsync(CreateSaleDto dto);
    Task<SaleResponseDto> UpdateAsync(Guid id, UpdateSaleDto dto);
    Task DeleteLogicalAsync(Guid id);
}
