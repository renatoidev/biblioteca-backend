using Biblioteca.Models;

namespace Biblioteca.ViewModels;

public class ObraViewModel
{
    public ObraViewModel()
    {
        Autores = new List<AutorViewModel>();
    }
    public string Titulo { get; set; }
    public string Editora { get; set; }
    public string Foto { get; set; }
    public List<AutorViewModel> Autores { get; set; } 

}