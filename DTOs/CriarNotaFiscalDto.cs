namespace FiscalApi.DTOs;

public class CriarNotaFiscalDto
{
    public string Numero { get; set; } = string.Empty;

    public DateTime DataEmissao { get; set; }

    public int ClienteId { get; set; }

    public List<CriarItemNotaDto> Itens { get; set; } = new();
}

public class CriarItemNotaDto
{
    public string Descricao { get; set; } = string.Empty;

    public int Quantidade { get; set; }

    public decimal ValorUnitario { get; set; }
}