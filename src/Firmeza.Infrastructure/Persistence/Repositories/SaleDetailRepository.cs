using Firmeza.Application.Interfaces.Repositories;
using Firmeza.Domain.Entities;
using Firmeza.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Firmeza.Infrastructure.Persistence.Repositories;

public class SaleDetailRepository : ISaleDetailRepository
{
    private readonly AppDbContext _context;

    public SaleDetailRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SaleDetail>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaleDetails
            .Where(sd => sd.IsActive)
            .Include(sd => sd.Product)
            .Include(sd => sd.Sale)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<SaleDetail>> GetAllPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        return await _context.SaleDetails
            .Where(sd => sd.IsActive)
            .Include(sd => sd.Product)
            .Include(sd => sd.Sale)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<SaleDetail?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.SaleDetails
            .Where(sd => sd.IsActive)
            .Include(sd => sd.Product)
            .Include(sd => sd.Sale)
            .FirstOrDefaultAsync(sd => sd.Id == id, cancellationToken);
    }

    public async Task AddAsync(SaleDetail saleDetail, CancellationToken cancellationToken = default)
    {
        var productExists = await _context.Products
            .AnyAsync(p => p.Id == saleDetail.ProductId, cancellationToken);

        if (!productExists)
            throw new InvalidOperationException("El producto especificado no existe.");

        var saleExists = await _context.Sales
            .AnyAsync(s => s.Id == saleDetail.SaleId, cancellationToken);

        if (!saleExists)
            throw new InvalidOperationException("La venta especificada no existe.");

        await _context.SaleDetails.AddAsync(saleDetail, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(SaleDetail saleDetail, CancellationToken cancellationToken = default)
    {
        var existingSaleDetail = await _context.SaleDetails
            .FirstOrDefaultAsync(sd => sd.Id == saleDetail.Id, cancellationToken);

        if (existingSaleDetail == null)
            throw new InvalidOperationException("Detalle de venta no encontrado.");

        var productExists = await _context.Products
            .AnyAsync(p => p.Id == saleDetail.ProductId, cancellationToken);

        if (!productExists)
            throw new InvalidOperationException("El producto especificado no existe.");

        var saleExists = await _context.Sales
            .AnyAsync(s => s.Id == saleDetail.SaleId, cancellationToken);

        if (!saleExists)
            throw new InvalidOperationException("La venta especificada no existe.");

        _context.Entry(existingSaleDetail).CurrentValues.SetValues(saleDetail);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteLogicalAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var saleDetail = await _context.SaleDetails.FindAsync(id, cancellationToken);
        if (saleDetail == null)
            throw new InvalidOperationException("Detalle de venta no encontrado.");

        saleDetail.Deactivate();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
