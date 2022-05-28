using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Models;

[Table("Autor")]
public class Autor
{
    public Autor()
    {
        Obras = new List<Obra>();
    }
    public int Id { get; set; }
    public string? Nome { get; set; }
    public List<Obra> Obras { get; set; } 
}