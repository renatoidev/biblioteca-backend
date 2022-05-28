using Biblioteca.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Biblioteca.Data.Mappings;

public class ObraMap : IEntityTypeConfiguration<Obra>
{
    public void Configure(EntityTypeBuilder<Obra> builder)
    {
        builder.ToTable("Obra");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Titulo)
        .HasColumnName("titulo")
        .HasColumnType("VARCHAR")
        .HasMaxLength(45)
        .IsRequired();

        builder.Property(x => x.Editora)
        .HasColumnName("editora")
        .HasColumnType("VARCHAR")
        .HasMaxLength(45)
        .IsRequired();


        builder.Property(x => x.Foto)
        .HasColumnName("foto")
        .HasColumnType("VARCHAR")
        .HasMaxLength(45)
        .IsRequired();

        //Relacionamentos

        builder.HasMany(x => x.Autores)
        .WithMany(x => x.Obras)
        .UsingEntity<Dictionary<string, object>>(
            "ObraAutor",
            obra => obra
            .HasOne<Autor>()
            .WithMany()
            .HasForeignKey("ObraId")
            .HasConstraintName("FK_ObraAutor_ObraId")
            .OnDelete(DeleteBehavior.Cascade),

            autor => autor
            .HasOne<Obra>()
            .WithMany()
            .HasForeignKey("AutorId")
            .HasConstraintName("FK_ObraAutor_AutorId")
            .OnDelete(DeleteBehavior.Cascade)

        );
    }
}