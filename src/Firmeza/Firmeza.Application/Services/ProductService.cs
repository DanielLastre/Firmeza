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

    public async Task<IEnumerable<ProductResponseDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var products = await _productRepository.GetAllAsync(cancellationToken);
        return products.Select(p => MapToResponseDto(p));
    }

    public async Task<IEnumerable<ProductResponseDto>> GetAllPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var products = await _productRepository.GetAllPagedAsync(pageNumber, pageSize, cancellationToken);
        return products.Select(p => MapToResponseDto(p));
    }

    public async Task<ProductResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByIdAsync(id, cancellationToken);
        return product == null ? null : MapToResponseDto(product);
    }

    public async Task<ProductResponseDto> CreateAsync(CreateProductDto dto, CancellationToken cancellationToken = default)
    {
        var product = new Product(dto.Name, dto.Brand, dto.Price, dto.Description, dto.Stock);
        await _productRepository.AddAsync(product, cancellationToken);
        return MapToResponseDto(product);
    }

    public async Task<ProductResponseDto> UpdateAsync(Guid id, UpdateProductDto dto, CancellationToken cancellationToken = default)
    {
        var existingProduct = await _productRepository.GetByIdAsync(id, cancellationToken);
        if (existingProduct == null)
            throw new InvalidOperationException("Producto no encontrado.");

        await _productRepository.UpdateAsync(existingProduct, cancellationToken);
        return MapToResponseDto(existingProduct);
    }

    public async Task DeleteLogicalAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _productRepository.DeleteLogicalAsync(id, cancellationToken);
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
