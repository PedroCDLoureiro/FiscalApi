namespace FiscalApi.Models;
using System.Text.Json.Serialization;

public class Cliente
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string Cnpj { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
    
    [JsonIgnore]
    public ICollection<NotaFiscal> NotasFiscais { get; set; }
    = new List<NotaFiscal>(); 
}