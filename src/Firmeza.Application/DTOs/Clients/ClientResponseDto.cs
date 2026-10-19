namespace Firmeza.Application.DTOs.Clients;

public class ClientResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty;
    public Guid? EnterpriseId { get; set; }
    public bool IsActive { get; set; }
}
