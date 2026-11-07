namespace FiscalApi.Models;

public class NotaFiscal
{
    public int Id { get; set; }
    public string Numero { get; set; } = string.Empty;
    public DateTime DataEmissao { get; set; }
    public decimal ValorTotal { get; set; }
    // Foreign key for Cliente
    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; } = null!;
    public ICollection<ItemNota> Itens { get; set; } = new List<ItemNota>();
}