using Firmeza.Application.DTOs.Clients;
using Firmeza.Application.DTOs.Common;
using Firmeza.Application.Exceptions;
using Firmeza.Application.Interfaces.Repositories;
using Firmeza.Application.Interfaces.Services;
using Firmeza.Domain.Entities;

namespace Firmeza.Application.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly IEnterpriseRepository _enterpriseRepository;

    public ClientService(
        IClientRepository clientRepository,
        IEnterpriseRepository enterpriseRepository)
    {
        _clientRepository = clientRepository;
        _enterpriseRepository = enterpriseRepository;
    }

    public async Task<IEnumerable<ClientResponseDto>> GetAllAsync()
    {
        var clients = await _clientRepository.GetAllAsync();
        return clients.Select(MapToResponseDto);
    }

    public async Task<IEnumerable<ClientResponseDto>> GetAllPagedAsync(int pageNumber, int pageSize)
    {
        var clients = await _clientRepository.GetAllPagedAsync(pageNumber, pageSize);
        return clients.Select(MapToResponseDto);
    }

    public async Task<PagedResultDto<ClientResponseDto>> GetPagedAsync(int pageNumber, int pageSize, bool? onlyActive = null)
    {
        if (pageNumber < 1) pageNumber = 1;
        if (pageSize < 1) pageSize = 10;

        var (items, totalRecords) = await _clientRepository.GetPagedAsync(pageNumber, pageSize, onlyActive);
        var mapped = items.Select(MapToResponseDto);

        return new PagedResultDto<ClientResponseDto>(mapped, totalRecords, pageNumber, pageSize);
    }

    public async Task<ClientResponseDto?> GetByIdAsync(Guid id)
    {
        var client = await _clientRepository.GetByIdAsync(id);
        return client == null ? null : MapToResponseDto(client);
    }

    public async Task<ClientResponseDto> CreateAsync(CreateClientDto dto)
    {
        var enterpriseId = dto.EnterpriseId ?? Guid.Empty;
        if (enterpriseId != Guid.Empty)
        {
            var enterprise = await _enterpriseRepository.GetByIdAsync(enterpriseId);
            if (enterprise == null)
                throw new NotFoundException($"La empresa con Id '{enterpriseId}' no existe.");
        }

        if (await _clientRepository.ExistsByEmailAsync(dto.Email))
            throw new ConflictException($"Ya existe un cliente registrado con el correo electrónico '{dto.Email}'.");

        if (await _clientRepository.ExistsByDocumentAsync(dto.Document))
            throw new ConflictException($"Ya existe un cliente registrado con el documento de identidad '{dto.Document}'.");

        var client = new Client(
            dto.Name,
            dto.Age,
            dto.Phone,
            dto.Email,
            dto.Document,
            enterpriseId
        );

        await _clientRepository.AddAsync(client);
        return MapToResponseDto(client);
    }

    public async Task<ClientResponseDto> UpdateAsync(Guid id, UpdateClientDto dto)
    {
        var existingClient = await _clientRepository.GetByIdAsync(id);
        if (existingClient == null)
            throw new NotFoundException($"Cliente con Id '{id}' no encontrado.");

        var enterpriseId = dto.EnterpriseId ?? existingClient.EnterpriseId;
        if (enterpriseId != Guid.Empty)
        {
            var enterprise = await _enterpriseRepository.GetByIdAsync(enterpriseId);
            if (enterprise == null)
                throw new NotFoundException($"La empresa con Id '{enterpriseId}' no existe.");
        }

        if (await _clientRepository.ExistsByEmailAsync(dto.Email, id))
            throw new ConflictException($"Ya existe otro cliente registrado con el correo electrónico '{dto.Email}'.");

        if (await _clientRepository.ExistsByDocumentAsync(dto.Document, id))
            throw new ConflictException($"Ya existe otro cliente registrado con el documento de identidad '{dto.Document}'.");

        existingClient.Update(
            dto.Name,
            dto.Age,
            dto.Phone,
            dto.Email,
            dto.Document,
            enterpriseId
        );

        await _clientRepository.UpdateAsync(existingClient);
        return MapToResponseDto(existingClient);
    }

    public async Task DeleteLogicalAsync(Guid id)
    {
        var existingClient = await _clientRepository.GetByIdAsync(id);
        if (existingClient == null)
            throw new NotFoundException($"Cliente con Id '{id}' no encontrado.");

        await _clientRepository.DeleteLogicalAsync(id);
    }

    private static ClientResponseDto MapToResponseDto(Client client)
    {
        return new ClientResponseDto
        {
            Id = client.Id,
            Name = client.Name,
            Age = client.Age,
            Phone = client.Phone,
            Email = client.Email,
            Document = client.Document,
            EnterpriseId = client.EnterpriseId,
            IsActive = client.IsActive
        };
    }
}
