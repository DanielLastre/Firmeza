using Firmeza.Application.DTOs.Common;
using Firmeza.Application.DTOs.Enterprises;
using Firmeza.Application.Exceptions;
using Firmeza.Application.Interfaces.Repositories;
using Firmeza.Application.Interfaces.Services;
using Firmeza.Domain.Entities;
using Firmeza.Domain.Enums;

namespace Firmeza.Application.Services;

public class EnterpriseService : IEnterpriseService
{
    private readonly IEnterpriseRepository _enterpriseRepository;

    public EnterpriseService(IEnterpriseRepository enterpriseRepository)
    {
        _enterpriseRepository = enterpriseRepository;
    }

    public async Task<IEnumerable<EnterpriseResponseDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var enterprises = await _enterpriseRepository.GetAllAsync(cancellationToken);
        return enterprises.Select(MapToResponseDto);
    }

    public async Task<IEnumerable<EnterpriseResponseDto>> GetAllPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var enterprises = await _enterpriseRepository.GetAllPagedAsync(pageNumber, pageSize, cancellationToken);
        return enterprises.Select(MapToResponseDto);
    }

    public async Task<PagedResultDto<EnterpriseResponseDto>> GetPagedAsync(int pageNumber, int pageSize, bool? onlyActive = null, CancellationToken cancellationToken = default)
    {
        if (pageNumber < 1) pageNumber = 1;
        if (pageSize < 1) pageSize = 10;

        var (items, totalRecords) = await _enterpriseRepository.GetPagedAsync(pageNumber, pageSize, onlyActive, cancellationToken);
        var mapped = items.Select(MapToResponseDto);

        return new PagedResultDto<EnterpriseResponseDto>(mapped, totalRecords, pageNumber, pageSize);
    }

    public async Task<EnterpriseResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var enterprise = await _enterpriseRepository.GetByIdAsync(id, cancellationToken);
        return enterprise == null ? null : MapToResponseDto(enterprise);
    }

    public async Task<EnterpriseResponseDto> CreateAsync(CreateEnterpriseDto dto, CancellationToken cancellationToken = default)
    {
        if (await _enterpriseRepository.ExistsByTaxIdAsync(dto.TaxId, null, cancellationToken))
            throw new ConflictException($"Ya existe una empresa registrada con el NIT/TaxId '{dto.TaxId}'.");

        if (await _enterpriseRepository.ExistsByEmailAsync(dto.Email, null, cancellationToken))
            throw new ConflictException($"Ya existe una empresa registrada con el correo electrónico '{dto.Email}'.");

        var enterprise = new Enterprise(
            dto.Name,
            dto.TradeName,
            (EnterpriseType)dto.Type,
            dto.Address,
            dto.Phone,
            dto.Email,
            dto.TaxId
        );

        await _enterpriseRepository.AddAsync(enterprise, cancellationToken);
        return MapToResponseDto(enterprise);
    }

    public async Task<EnterpriseResponseDto> UpdateAsync(Guid id, UpdateEnterpriseDto dto, CancellationToken cancellationToken = default)
    {
        var existingEnterprise = await _enterpriseRepository.GetByIdAsync(id, cancellationToken);
        if (existingEnterprise == null)
            throw new NotFoundException($"Empresa con Id '{id}' no encontrada.");

        if (await _enterpriseRepository.ExistsByTaxIdAsync(dto.TaxId, id, cancellationToken))
            throw new ConflictException($"Ya existe otra empresa registrada con el NIT/TaxId '{dto.TaxId}'.");

        if (await _enterpriseRepository.ExistsByEmailAsync(dto.Email, id, cancellationToken))
            throw new ConflictException($"Ya existe otra empresa registrada con el correo electrónico '{dto.Email}'.");

        existingEnterprise.Update(
            dto.Name,
            dto.TradeName,
            (EnterpriseType)dto.Type,
            dto.Address,
            dto.Phone,
            dto.Email,
            dto.TaxId
        );

        await _enterpriseRepository.UpdateAsync(existingEnterprise, cancellationToken);
        return MapToResponseDto(existingEnterprise);
    }

    public async Task DeleteLogicalAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var existingEnterprise = await _enterpriseRepository.GetByIdAsync(id, cancellationToken);
        if (existingEnterprise == null)
            throw new NotFoundException($"Empresa con Id '{id}' no encontrada.");

        await _enterpriseRepository.DeleteLogicalAsync(id, cancellationToken);
    }

    private static EnterpriseResponseDto MapToResponseDto(Enterprise enterprise)
    {
        return new EnterpriseResponseDto
        {
            Id = enterprise.Id,
            Name = enterprise.Name,
            TradeName = enterprise.TradeName,
            Type = (int)enterprise.Type,
            Address = enterprise.Address,
            Phone = enterprise.Phone.Value,
            Email = enterprise.Email.Value,
            TaxId = enterprise.TaxId.Value,
            IsActive = enterprise.IsActive
        };
    }
}
