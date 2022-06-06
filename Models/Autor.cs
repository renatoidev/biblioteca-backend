using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
    [JsonIgnore]
    public List<Obra> Obras { get; set; } 
}