namespace Biblioteca.Models;

public class Autor
{
    public Autor()
    {
        Obras = new List<Obra>();
    }
    public int Id { get; set; }
    public string? Nome { get; set; }
    public IList<Obra> Obras { get; set; } 
}