using Firmeza.Application.DTOs.Sales;
using Firmeza.Application.Interfaces.Repositories;
using Firmeza.Application.Interfaces.Services;
using Firmeza.Domain.Entities;

namespace Firmeza.Application.Services;

public class SaleService : ISaleService
{
    private readonly ISaleRepository _saleRepository;

    public SaleService(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public async Task<IEnumerable<SaleResponseDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var sales = await _saleRepository.GetAllAsync(cancellationToken);
        return sales.Select(s => MapToResponseDto(s));
    }

    public async Task<IEnumerable<SaleResponseDto>> GetAllPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var sales = await _saleRepository.GetAllPagedAsync(pageNumber, pageSize, cancellationToken);
        return sales.Select(s => MapToResponseDto(s));
    }

    public async Task<SaleResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var sale = await _saleRepository.GetByIdAsync(id, cancellationToken);
        return sale == null ? null : MapToResponseDto(sale);
    }

    public async Task<SaleResponseDto> CreateAsync(CreateSaleDto dto, CancellationToken cancellationToken = default)
    {
        var sale = new Sale(DateTime.UtcNow, dto.ClientId, 0);
        await _saleRepository.AddAsync(sale, cancellationToken);
        return MapToResponseDto(sale);
    }

    public async Task<SaleResponseDto> UpdateAsync(Guid id, UpdateSaleDto dto, CancellationToken cancellationToken = default)
    {
        var existingSale = await _saleRepository.GetByIdAsync(id, cancellationToken);
        if (existingSale == null)
            throw new InvalidOperationException("Venta no encontrada.");

        await _saleRepository.UpdateAsync(existingSale, cancellationToken);
        return MapToResponseDto(existingSale);
    }

    public async Task DeleteLogicalAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _saleRepository.DeleteLogicalAsync(id, cancellationToken);
    }

    private static SaleResponseDto MapToResponseDto(Sale sale)
    {
        return new SaleResponseDto
        {
            Id = sale.Id,
            Date = sale.Date,
            ClientId = sale.ClientId,
            Total = sale.Total.Amount,
            IsActive = sale.IsActive
        };
    }
}
