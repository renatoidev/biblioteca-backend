using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Models;

[Table("Obra")]
public class Obra
{
    public Obra()
    {
        Autores = new List<Autor>();
    }
    public int Id { get; set; }
    public string? Titulo { get; set; }
    public string? Editora { get; set; }
    public string? Foto { get; set; }
    public List<Autor> Autores { get; set; }
}
