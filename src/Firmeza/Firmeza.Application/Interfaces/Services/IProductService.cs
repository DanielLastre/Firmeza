using Firmeza.Application.DTOs.Products;

namespace Firmeza.Application.Interfaces.Services;

public interface IProductService
{
    Task<IEnumerable<ProductResponseDto>> GetAllAsync();
    Task<IEnumerable<ProductResponseDto>> GetAllPagedAsync(int pageNumber, int pageSize);
    Task<ProductResponseDto?> GetByIdAsync(Guid id);
    Task<ProductResponseDto> CreateAsync(CreateProductDto dto);
    Task<ProductResponseDto> UpdateAsync(Guid id, UpdateProductDto dto);
    Task DeleteLogicalAsync(Guid id);
}
