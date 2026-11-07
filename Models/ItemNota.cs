namespace FiscalApi.Models;

public class ItemNota
{
    public int Id {get; set;}
    public string Descricao {get; set;} = string.Empty;
    public int Quantidade {get; set;}
    public decimal ValorUnitario {get; set;}
    // Foreign key for NotaFiscal
    public int NotaFiscalId {get; set;}
    public NotaFiscal NotaFiscal {get; set;} = null!;  
}