using Biblioteca.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Models;

[Keyless]
[Table("ObraAutor")]
public class ObraAutor
{
    
    public int AutorId { get; set; }
    public int ObraId { get; set; }
    public Autor Autor { get; set; }
    public Obra Obra { get; set; }

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
