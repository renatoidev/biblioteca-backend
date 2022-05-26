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
        var autores = context.Autores.ToList();
        var obras = context.Obras.ToList();
        var autoresModel = "";
        var obraAutores = context.ObraAutor.ToList();

        

        var result = await context
            .Obras
            .AsNoTracking()
            .Include(obra => obra.Autores)
            .Select(x => new ObraViewModel
            {
                Titulo = x.Titulo,
                Editora = x.Editora,
                Foto = x.Foto,
                Autores = x.RetornaAutores(obras,autores,obraAutores)

            })
            .ToListAsync();

        var result2 = await context
            .ObraAutor
            .AsNoTracking()
            .Include(obra => obra.Obra)
            .Include(autor => autor.Autor)
            .Select(x => new ObraViewModel
            {
                Titulo = x.Obra.Titulo,
                Editora = x.Obra.Editora,
                Foto = x.Obra.Foto,
                Autores = x.RetornaAutores(obras, autores, obraAutores)
            })
            .ToListAsync();
        return Ok(result2);

    }

    [HttpPost("/obras")]
    public async Task<IActionResult> CadastrarObra(
        [FromBody] ObraViewModel model,
        [FromServices] ObraDataContext context
    )
    {
        
        //var autor = await context.Autores.FirstOrDefaultAsync(x => x.Obra.Id == model.Autor.ObraId);

        var obra = new Obra
        {
            Titulo = model.Titulo,
            Editora = model.Editora,
            Foto = model.Foto
        };

        
        
            
        foreach (var autor in model.Autores)
        {
            var autorContext = await context.Autores.FirstOrDefaultAsync(x => x.Nome.ToLower() == autor.Nome.ToLower());

            if (autorContext is not null)
                obra.Autores.Add(autorContext);
            else
            {
                var novoAutor = new Autor();
                novoAutor.Nome = autor.Nome;
                obra.Autores.Add(novoAutor);
            }
        }

        var result = context.Obras.Add(obra);
        await context.SaveChangesAsync();
        return Created($"{obra.Id}", result);
            
    }
}