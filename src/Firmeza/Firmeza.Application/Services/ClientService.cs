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

    public async Task<IEnumerable<ClientResponseDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var clients = await _clientRepository.GetAllAsync(cancellationToken);
        return clients.Select(MapToResponseDto);
    }

    public async Task<IEnumerable<ClientResponseDto>> GetAllPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var clients = await _clientRepository.GetAllPagedAsync(pageNumber, pageSize, cancellationToken);
        return clients.Select(MapToResponseDto);
    }

    public async Task<PagedResultDto<ClientResponseDto>> GetPagedAsync(int pageNumber, int pageSize, bool? onlyActive = null, CancellationToken cancellationToken = default)
    {
        if (pageNumber < 1) pageNumber = 1;
        if (pageSize < 1) pageSize = 10;

        var (items, totalRecords) = await _clientRepository.GetPagedAsync(pageNumber, pageSize, onlyActive, cancellationToken);
        var mapped = items.Select(MapToResponseDto);

        return new PagedResultDto<ClientResponseDto>(mapped, totalRecords, pageNumber, pageSize);
    }

    public async Task<ClientResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var client = await _clientRepository.GetByIdAsync(id, cancellationToken);
        return client == null ? null : MapToResponseDto(client);
    }

    public async Task<ClientResponseDto> CreateAsync(CreateClientDto dto, CancellationToken cancellationToken = default)
    {
        var enterpriseId = dto.EnterpriseId ?? Guid.Empty;
        if (enterpriseId != Guid.Empty)
        {
            var enterprise = await _enterpriseRepository.GetByIdAsync(enterpriseId, cancellationToken);
            if (enterprise == null)
                throw new NotFoundException($"La empresa con Id '{enterpriseId}' no existe.");
        }

        if (await _clientRepository.ExistsByEmailAsync(dto.Email, null, cancellationToken))
            throw new ConflictException($"Ya existe un cliente registrado con el correo electrónico '{dto.Email}'.");

        if (await _clientRepository.ExistsByDocumentAsync(dto.Document, null, cancellationToken))
            throw new ConflictException($"Ya existe un cliente registrado con el documento de identidad '{dto.Document}'.");

        var client = new Client(
            dto.Name,
            dto.Age,
            dto.Phone,
            dto.Email,
            dto.Document,
            enterpriseId
        );

        await _clientRepository.AddAsync(client, cancellationToken);
        return MapToResponseDto(client);
    }

    public async Task<ClientResponseDto> UpdateAsync(Guid id, UpdateClientDto dto, CancellationToken cancellationToken = default)
    {
        var existingClient = await _clientRepository.GetByIdAsync(id, cancellationToken);
        if (existingClient == null)
            throw new NotFoundException($"Cliente con Id '{id}' no encontrado.");

        var enterpriseId = dto.EnterpriseId ?? existingClient.EnterpriseId;
        if (enterpriseId != Guid.Empty)
        {
            var enterprise = await _enterpriseRepository.GetByIdAsync(enterpriseId, cancellationToken);
            if (enterprise == null)
                throw new NotFoundException($"La empresa con Id '{enterpriseId}' no existe.");
        }

        if (await _clientRepository.ExistsByEmailAsync(dto.Email, id, cancellationToken))
            throw new ConflictException($"Ya existe otro cliente registrado con el correo electrónico '{dto.Email}'.");

        if (await _clientRepository.ExistsByDocumentAsync(dto.Document, id, cancellationToken))
            throw new ConflictException($"Ya existe otro cliente registrado con el documento de identidad '{dto.Document}'.");

        existingClient.Update(
            dto.Name,
            dto.Age,
            dto.Phone,
            dto.Email,
            dto.Document,
            enterpriseId
        );

        await _clientRepository.UpdateAsync(existingClient, cancellationToken);
        return MapToResponseDto(existingClient);
    }

    public async Task DeleteLogicalAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var existingClient = await _clientRepository.GetByIdAsync(id, cancellationToken);
        if (existingClient == null)
            throw new NotFoundException($"Cliente con Id '{id}' no encontrado.");

        await _clientRepository.DeleteLogicalAsync(id, cancellationToken);
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
