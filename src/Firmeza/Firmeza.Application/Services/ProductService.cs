using Firmeza.Application.DTOs.Products;
using Firmeza.Application.Interfaces.Repositories;
using Firmeza.Application.Interfaces.Services;
using Firmeza.Domain.Entities;

namespace Firmeza.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductResponseDto>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync();
        return products.Select(p => MapToResponseDto(p));
    }

    public async Task<IEnumerable<ProductResponseDto>> GetAllPagedAsync(int pageNumber, int pageSize)
    {
        var products = await _productRepository.GetAllPagedAsync(pageNumber, pageSize);
        return products.Select(p => MapToResponseDto(p));
    }

    public async Task<ProductResponseDto?> GetByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        return product == null ? null : MapToResponseDto(product);
    }

    public async Task<ProductResponseDto> CreateAsync(CreateProductDto dto)
    {
        var product = new Product(dto.Name, dto.Brand, dto.Price, dto.Description, dto.Stock);
        await _productRepository.AddAsync(product);
        return MapToResponseDto(product);
    }

    public async Task<ProductResponseDto> UpdateAsync(Guid id, UpdateProductDto dto)
    {
        var existingProduct = await _productRepository.GetByIdAsync(id);
        if (existingProduct == null)
            throw new InvalidOperationException("Producto no encontrado.");

        await _productRepository.UpdateAsync(existingProduct);
        return MapToResponseDto(existingProduct);
    }

    public async Task DeleteLogicalAsync(Guid id)
    {
        await _productRepository.DeleteLogicalAsync(id);
    }

    private static ProductResponseDto MapToResponseDto(Product product)
    {
        return new ProductResponseDto
        {
            Id = product.Id,
            Name = product.Name,
            Brand = product.Brand,
            Price = product.Price.Amount,
            Description = product.Description,
            Stock = product.Stock,
            IsActive = product.IsActive
        };
    }
}
