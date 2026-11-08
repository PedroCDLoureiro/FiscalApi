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

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var notas = await _context.NotasFiscais
            .Include(n => n.Cliente)
            .Include(n => n.Itens)
            .ToListAsync();

        return Ok(notas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var nota = await _context.NotasFiscais
            .Include(n => n.Cliente)
            .Include(n => n.Itens)
            .FirstOrDefaultAsync(n => n.Id == id);

        if (nota == null)
        {
            return NotFound("Nota fiscal não encontrada.");
        }

        return Ok(nota);
    }

    [HttpGet("cliente/{clienteId}")]
    public async Task<IActionResult> ListarPorCliente(int clienteId)
    {
        var notas = await _context.NotasFiscais
            .Where(n => n.ClienteId == clienteId)
            .Include(n => n.Cliente)
            .Include(n => n.Itens)
            .ToListAsync();

        return Ok(notas);
    }

    [HttpGet("periodo")]
    public async Task<IActionResult> ListarPorPeriodo(DateTime inicio, DateTime fim)
    {
        var notas = await _context.NotasFiscais
            .Where(n => n.DataEmissao >= inicio && n.DataEmissao <= fim)
            .Include(n => n.Cliente)
            .Include(n => n.Itens)
            .OrderBy(n => n.DataEmissao)
            .ToListAsync();

        return Ok(notas);
    }

    [HttpGet("resumo")]
    public async Task<IActionResult> ObterResumo()
    {
        var quantidadeNotas = await _context.NotasFiscais.CountAsync();

        var valorTotal = await _context.NotasFiscais.SumAsync(n => n.ValorTotal);

        var valorMedio = await _context.NotasFiscais.Select(n => (double?)n.ValorTotal).AverageAsync();

        return Ok(new
        {
            QuantidadeNotas = quantidadeNotas,
            ValorTotal = valorTotal,
            ValorMedio = valorMedio ?? 0
        });
    }
}