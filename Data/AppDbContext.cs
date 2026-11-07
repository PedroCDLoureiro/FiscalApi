using FiscalApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FiscalApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Cliente> Clientes { get; set; }

    public DbSet<NotaFiscal> NotasFiscais { get; set; }

    public DbSet<ItemNota> ItensNota { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Cliente>()
            .HasMany(c => c.NotasFiscais)
            .WithOne(n => n.Cliente)
            .HasForeignKey(n => n.ClienteId);

        modelBuilder.Entity<NotaFiscal>()
            .HasMany(n => n.Itens)
            .WithOne(i => i.NotaFiscal)
            .HasForeignKey(i => i.NotaFiscalId);
    }
}