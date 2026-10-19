using Firmeza.Application.Interfaces.Repositories;
using Firmeza.Domain.Entities;
using Firmeza.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Firmeza.Infrastructure.Persistence.Repositories;

public class EnterpriseRepository : IEnterpriseRepository
{
    private readonly AppDbContext _context;

    public EnterpriseRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Enterprise>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Enterprises
            .OrderBy(e => e.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Enterprise>> GetAllPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        return await _context.Enterprises
            .OrderBy(e => e.Name)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<(IEnumerable<Enterprise> Items, int TotalRecords)> GetPagedAsync(int pageNumber, int pageSize, bool? onlyActive = null, CancellationToken cancellationToken = default)
    {
        var query = _context.Enterprises.AsQueryable();

        if (onlyActive.HasValue)
        {
            query = query.Where(e => e.IsActive == onlyActive.Value);
        }

        var totalRecords = await query.CountAsync(cancellationToken);
        var items = await query
            .OrderBy(e => e.Name)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalRecords);
    }

    public async Task<Enterprise?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Enterprises
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public async Task AddAsync(Enterprise enterprise, CancellationToken cancellationToken = default)
    {
        await _context.Enterprises.AddAsync(enterprise, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Enterprise enterprise, CancellationToken cancellationToken = default)
    {
        _context.Enterprises.Update(enterprise);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteLogicalAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var enterprise = await _context.Enterprises.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        if (enterprise == null)
            throw new KeyNotFoundException($"Empresa con Id '{id}' no encontrada.");

        enterprise.Deactivate();
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsByTaxIdAsync(string taxId, Guid? excludeId = null, CancellationToken cancellationToken = default)
    {
        var query = _context.Enterprises.Where(e => e.TaxId.Value == taxId);
        if (excludeId.HasValue)
        {
            query = query.Where(e => e.Id != excludeId.Value);
        }

        return await query.AnyAsync(cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(string email, Guid? excludeId = null, CancellationToken cancellationToken = default)
    {
        var query = _context.Enterprises.Where(e => e.Email.Value == email);
        if (excludeId.HasValue)
        {
            query = query.Where(e => e.Id != excludeId.Value);
        }

        return await query.AnyAsync(cancellationToken);
    }
}
