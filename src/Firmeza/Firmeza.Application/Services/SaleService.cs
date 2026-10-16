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

    public async Task<IEnumerable<SaleResponseDto>> GetAllAsync()
    {
        var sales = await _saleRepository.GetAllAsync();
        return sales.Select(s => MapToResponseDto(s));
    }

    public async Task<IEnumerable<SaleResponseDto>> GetAllPagedAsync(int pageNumber, int pageSize)
    {
        var sales = await _saleRepository.GetAllPagedAsync(pageNumber, pageSize);
        return sales.Select(s => MapToResponseDto(s));
    }

    public async Task<SaleResponseDto?> GetByIdAsync(Guid id)
    {
        var sale = await _saleRepository.GetByIdAsync(id);
        return sale == null ? null : MapToResponseDto(sale);
    }

    public async Task<SaleResponseDto> CreateAsync(CreateSaleDto dto)
    {
        var sale = new Sale(DateTime.UtcNow, dto.ClientId, 0);
        await _saleRepository.AddAsync(sale);
        return MapToResponseDto(sale);
    }

    public async Task<SaleResponseDto> UpdateAsync(Guid id, UpdateSaleDto dto)
    {
        var existingSale = await _saleRepository.GetByIdAsync(id);
        if (existingSale == null)
            throw new InvalidOperationException("Venta no encontrada.");

        await _saleRepository.UpdateAsync(existingSale);
        return MapToResponseDto(existingSale);
    }

    public async Task DeleteLogicalAsync(Guid id)
    {
        await _saleRepository.DeleteLogicalAsync(id);
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
