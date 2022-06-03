using Biblioteca.Data.Mappings;
using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Data;

public class ObraDataContext : DbContext
{
    public ObraDataContext(DbContextOptions<ObraDataContext> options) : base(options)
    {
    }

    public DbSet<Obra> Obras { get; set; }
    public DbSet<Autor> Autores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ObraMap());
    }
}