using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteca.Data.Mappings;

public class AutorMap : IEntityTypeConfiguration<Autor>
{
    public void Configure(EntityTypeBuilder<Autor> builder)
    {
        builder.ToTable("Autores");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Nome)
        .HasColumnName("Nome")
        .HasColumnType("VARCHAR")
        .HasMaxLength(45)
        .IsRequired();
        
        
    }
}
