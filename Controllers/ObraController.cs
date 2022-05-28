using Biblioteca.Data;
using Biblioteca.Models;
using Biblioteca.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Controllers;

public class ObraController : ControllerBase
{
    [HttpGet("/obras")]
    public async Task<IActionResult> ListarObras(
        [FromServices] ObraDataContext context
    )
    {

        var result = await context.Obras
            .Include(o => o.Autores)
            .Select(x => new 
            {
                Titulo = x.Titulo,
                Editora = x.Editora,
                Foto = x.Foto,
                Autores = x.Autores
            })
            .ToListAsync();
        return Ok(result);
    }

    [HttpPost("/obras")]
    public async Task<IActionResult> CadastrarObra(
        [FromBody] ObraViewModel model,
        [FromServices] ObraDataContext context
    )
    {
        var autores = context.Autores.ToList();
        var autoresModel = new List<Autor>();

        foreach (var autor in model.Autores)
        {
            if (!autores.Any(x => x.Nome == autor.Nome))
            {
                autoresModel.Add(new Autor { Nome = autor.Nome });
            }
            else
            {
                autoresModel.Add(autores.FirstOrDefault());
            }
        }
        var obra = new Obra
        {
            Titulo = model.Titulo,
            Editora = model.Editora,
            Foto = model.Foto,
            Autores = autoresModel
            
        };

        context.Obras.Add(obra);
        await context.SaveChangesAsync();

        return Ok("Obra criada com sucesso");       
    }    
}