using System.ComponentModel.DataAnnotations;

namespace FiscalApi.DTOs;

public class CriarClienteDto
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [MinLength(2, ErrorMessage = "O nome deve ter pelo menos 2 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O CNPJ é obrigatório.")]
    [StringLength(18, MinimumLength = 14,
        ErrorMessage = "O CNPJ deve ter entre 14 e 18 caracteres.")]
    public string Cnpj { get; set; } = string.Empty;

    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "Informe um e-mail válido.")]
    public string Email { get; set; } = string.Empty;
}