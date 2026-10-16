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

    public async Task<IEnumerable<EnterpriseResponseDto>> GetAllAsync()
    {
        var enterprises = await _enterpriseRepository.GetAllAsync();
        return enterprises.Select(MapToResponseDto);
    }

    public async Task<IEnumerable<EnterpriseResponseDto>> GetAllPagedAsync(int pageNumber, int pageSize)
    {
        var enterprises = await _enterpriseRepository.GetAllPagedAsync(pageNumber, pageSize);
        return enterprises.Select(MapToResponseDto);
    }

    public async Task<PagedResultDto<EnterpriseResponseDto>> GetPagedAsync(int pageNumber, int pageSize, bool? onlyActive = null)
    {
        if (pageNumber < 1) pageNumber = 1;
        if (pageSize < 1) pageSize = 10;

        var (items, totalRecords) = await _enterpriseRepository.GetPagedAsync(pageNumber, pageSize, onlyActive);
        var mapped = items.Select(MapToResponseDto);

        return new PagedResultDto<EnterpriseResponseDto>(mapped, totalRecords, pageNumber, pageSize);
    }

    public async Task<EnterpriseResponseDto?> GetByIdAsync(Guid id)
    {
        var enterprise = await _enterpriseRepository.GetByIdAsync(id);
        return enterprise == null ? null : MapToResponseDto(enterprise);
    }

    public async Task<EnterpriseResponseDto> CreateAsync(CreateEnterpriseDto dto)
    {
        if (await _enterpriseRepository.ExistsByTaxIdAsync(dto.TaxId))
            throw new ConflictException($"Ya existe una empresa registrada con el NIT/TaxId '{dto.TaxId}'.");

        if (await _enterpriseRepository.ExistsByEmailAsync(dto.Email))
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

        await _enterpriseRepository.AddAsync(enterprise);
        return MapToResponseDto(enterprise);
    }

    public async Task<EnterpriseResponseDto> UpdateAsync(Guid id, UpdateEnterpriseDto dto)
    {
        var existingEnterprise = await _enterpriseRepository.GetByIdAsync(id);
        if (existingEnterprise == null)
            throw new NotFoundException($"Empresa con Id '{id}' no encontrada.");

        if (await _enterpriseRepository.ExistsByTaxIdAsync(dto.TaxId, id))
            throw new ConflictException($"Ya existe otra empresa registrada con el NIT/TaxId '{dto.TaxId}'.");

        if (await _enterpriseRepository.ExistsByEmailAsync(dto.Email, id))
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

        await _enterpriseRepository.UpdateAsync(existingEnterprise);
        return MapToResponseDto(existingEnterprise);
    }

    public async Task DeleteLogicalAsync(Guid id)
    {
        var existingEnterprise = await _enterpriseRepository.GetByIdAsync(id);
        if (existingEnterprise == null)
            throw new NotFoundException($"Empresa con Id '{id}' no encontrada.");

        await _enterpriseRepository.DeleteLogicalAsync(id);
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
