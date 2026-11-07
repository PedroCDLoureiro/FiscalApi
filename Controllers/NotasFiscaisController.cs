using FiscalApi.Data;
using FiscalApi.DTOs;
using FiscalApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiscalApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotasFiscaisController : ControllerBase
{
    private readonly AppDbContext _context;

    public NotasFiscaisController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Criar(CriarNotaFiscalDto dto)
    {
        var cliente = await _context.Clientes
            .FindAsync(dto.ClienteId);

        if (cliente == null)
        {
            return NotFound("Cliente não encontrado.");
        }

        var nota = new NotaFiscal
        {
            Numero = dto.Numero,
            DataEmissao = dto.DataEmissao,
            ClienteId = dto.ClienteId,

            Itens = dto.Itens.Select(item => new ItemNota
            {
                Descricao = item.Descricao,
                Quantidade = item.Quantidade,
                ValorUnitario = item.ValorUnitario
            }).ToList()
        };

        nota.ValorTotal = nota.Itens.Sum(
            item => item.Quantidade * item.ValorUnitario
        );

        _context.NotasFiscais.Add(nota);

        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(Criar),
            new { id = nota.Id },
            nota
        );
    }
}