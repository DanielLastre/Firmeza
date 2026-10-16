using System.ComponentModel.DataAnnotations;

namespace Firmeza.Application.DTOs.Clients;

public class CreateClientDto
{
    [Required(ErrorMessage = "El nombre del cliente es obligatorio.")]
    [StringLength(150, ErrorMessage = "El nombre no puede exceder 150 caracteres.")]
    public string Name { get; set; } = string.Empty;

    [Range(0, 150, ErrorMessage = "La edad debe estar entre 0 y 150 años.")]
    public int Age { get; set; }

    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    [StringLength(50, MinimumLength = 7, ErrorMessage = "El teléfono debe tener entre 7 y 50 caracteres.")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
    [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
    [StringLength(150, ErrorMessage = "El correo no puede exceder 150 caracteres.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "El documento de identidad es obligatorio.")]
    [StringLength(50, MinimumLength = 5, ErrorMessage = "El documento debe tener entre 5 y 50 caracteres.")]
    public string Document { get; set; } = string.Empty;

    [Required(ErrorMessage = "El ID de la empresa asociada es obligatorio.")]
    public Guid? EnterpriseId { get; set; }
}
