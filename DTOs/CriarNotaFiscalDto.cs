using System.ComponentModel.DataAnnotations;

namespace FiscalApi.DTOs;

public class CriarNotaFiscalDto
{
    [Required(ErrorMessage = "O número da nota é obrigatório.")]
    [StringLength(20, ErrorMessage = "O número deve ter no máximo 20 caracteres.")]
    public string Numero { get; set; } = string.Empty;

    [Range(typeof(DateTime), "2000-01-01", "2100-12-31",
        ErrorMessage = "Informe uma data de emissão válida.")]
    public DateTime DataEmissao { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Selecione um cliente válido.")]
    public int ClienteId { get; set; }

    [Required(ErrorMessage = "A nota deve possuir pelo menos um item.")]
    [MinLength(1, ErrorMessage = "A nota deve possuir pelo menos um item.")]
    public List<CriarItemNotaDto> Itens { get; set; } = new();
}

public class CriarItemNotaDto : IValidatableObject
{
    [Required(ErrorMessage = "A descrição do item é obrigatória.")]
    public string Descricao { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
    public int Quantidade { get; set; }

    [Required(ErrorMessage = "O valor unitário é obrigatório.")]
    public decimal ValorUnitario { get; set; }

    public IEnumerable<ValidationResult> Validate(
        ValidationContext validationContext)
    {
        if (ValorUnitario <= 0)
        {
            yield return new ValidationResult(
                "O valor unitário deve ser maior que zero.",
                new[] { nameof(ValorUnitario) }
            );
        }
    }
}