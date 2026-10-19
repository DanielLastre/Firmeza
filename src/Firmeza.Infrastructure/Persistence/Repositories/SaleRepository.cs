using Firmeza.Application.Interfaces.Repositories;
using Firmeza.Domain.Entities;
using Firmeza.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Firmeza.Infrastructure.Persistence.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly AppDbContext _context;

    public SaleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Sale>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Sales
            .Where(s => s.IsActive)
            .Include(s => s.Client)
            .Include(s => s.SaleDetails)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Sale>> GetAllPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        return await _context.Sales
            .Where(s => s.IsActive)
            .Include(s => s.Client)
            .Include(s => s.SaleDetails)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Sales
            .Where(s => s.IsActive)
            .Include(s => s.Client)
            .Include(s => s.SaleDetails)
            .ThenInclude(sd => sd.Product)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
    }

    public async Task AddAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        var clientExists = await _context.Clients
            .AnyAsync(c => c.Id == sale.ClientId, cancellationToken);

        if (!clientExists)
            throw new InvalidOperationException("El cliente especificado no existe.");

        await _context.Sales.AddAsync(sale, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        var existingSale = await _context.Sales
            .FirstOrDefaultAsync(s => s.Id == sale.Id, cancellationToken);

        if (existingSale == null)
            throw new InvalidOperationException("Venta no encontrada.");

        var clientExists = await _context.Clients
            .AnyAsync(c => c.Id == sale.ClientId, cancellationToken);

        if (!clientExists)
            throw new InvalidOperationException("El cliente especificado no existe.");

        _context.Entry(existingSale).CurrentValues.SetValues(sale);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteLogicalAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var sale = await _context.Sales.FindAsync(id, cancellationToken);
        if (sale == null)
            throw new InvalidOperationException("Venta no encontrada.");

        sale.Deactivate();
        await _context.SaveChangesAsync(cancellationToken);
    }
}
