
using FiscalApi.Data;
using FiscalApi.DTOs;
using FiscalApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FiscalApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ClientesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var clientes = await _context.Clientes.ToListAsync();

        return Ok(clientes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var cliente = await _context.Clientes.FindAsync(id);

        if (cliente == null)
        {
            return NotFound();
        }

        return Ok(cliente);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, AtualizarClienteDto dto)
    {
        var cliente = await _context.Clientes.FindAsync(id);

        if (cliente == null)
        {
            return NotFound();
        }

        cliente.Nome = dto.Nome;
        cliente.Cnpj = dto.Cnpj;
        cliente.Email = dto.Email;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> Criar(CriarClienteDto dto)
    {
        var cliente = new Cliente
        {
            Nome = dto.Nome,
            Cnpj = dto.Cnpj,
            Email = dto.Email
        };

        _context.Clientes.Add(cliente);

        await _context.SaveChangesAsync();

        return CreatedAtAction(
            nameof(Listar),
            new { id = cliente.Id },
            cliente
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var cliente = await _context.Clientes.FindAsync(id);

        if (cliente == null)
        {
            return NotFound();
        }

        _context.Clientes.Remove(cliente);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
