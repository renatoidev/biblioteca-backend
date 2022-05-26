using Biblioteca.ViewModels;

namespace Biblioteca.Models;

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

    public List<AutorViewModel> RetornaAutores(List<Obra> obras, List<Autor> autores, List<ObraAutor> obraAutores)
    {
        var result = new List<AutorViewModel>();

        foreach (var obraAutor in obraAutores)
        {
            foreach (var obra in obras)
            {
                foreach (var autor in autores)
                {
                    if (obraAutor.ObraId == obra.Id && obraAutor.AutorId == autor.Id)
                        result.Add(new AutorViewModel { Nome = autor.Nome });
                }
            }
        }

        return result;
    }
}
