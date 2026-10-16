using System.ComponentModel.DataAnnotations;

namespace Firmeza.Application.DTOs.Enterprises;

public class CreateEnterpriseDto
{
    [Required(ErrorMessage = "La razón social (Name) es obligatoria.")]
    [StringLength(150, ErrorMessage = "El nombre no puede exceder 150 caracteres.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "El nombre comercial (TradeName) es obligatorio.")]
    [StringLength(150, ErrorMessage = "El nombre comercial no puede exceder 150 caracteres.")]
    public string TradeName { get; set; } = string.Empty;

    [Range(1, 6, ErrorMessage = "El tipo de empresa debe estar entre 1 y 6 (1: Individual, 2: Corporation, 3: LLC, 4: Partnership, 5: NonProfit, 6: Cooperative).")]
    public int Type { get; set; }

    [Required(ErrorMessage = "La dirección es obligatoria.")]
    [StringLength(250, ErrorMessage = "La dirección no puede exceder 250 caracteres.")]
    public string Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    [StringLength(50, MinimumLength = 7, ErrorMessage = "El teléfono debe tener entre 7 y 50 caracteres.")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
    [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
    [StringLength(100, ErrorMessage = "El correo no puede exceder 100 caracteres.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "El NIT/TaxId es obligatorio.")]
    [StringLength(50, MinimumLength = 8, ErrorMessage = "El NIT/TaxId debe tener al menos 8 caracteres.")]
    public string TaxId { get; set; } = string.Empty;
}
