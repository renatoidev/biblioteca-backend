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

    [HttpPut("/obras/{id}")]
    public async Task<IActionResult> EditarObra (
        [FromServices] ObraDataContext context,
        [FromBody] ObraViewModel model,
        [FromRoute] int id)
    {
        var obra = await context.Obras.Include(x => x.Autores).FirstOrDefaultAsync(x => x.Id == id);
        var autores = obra.Autores;
        var modelAutores = new List<Autor>();

        foreach(var autor in autores)
        {
            if(!model.Autores.Any(x => x.Nome == autor.Nome))
            {
                modelAutores.Add(new Autor { Nome = model.Autores.FirstOrDefault().Nome });
            }
            else
            {
                modelAutores.Add(autores.FirstOrDefault());
            }
        }

        obra.Titulo = model.Titulo;
        obra.Editora = model.Editora;
        obra.Foto = model.Foto;
        obra.Autores = modelAutores;

        context.Obras.Update(obra);
        await context.SaveChangesAsync();

        return Ok("Obra alterada com sucesso");
    }

    [HttpDelete ("/obras/{id}")]
    public async Task<IActionResult> DeletarObra (
        [FromRoute] int id,
        [FromServices] ObraDataContext context)
    {
        var obra = await context.Obras.FirstOrDefaultAsync(x => x.Id == id);
        
        if (obra == null)
            return NotFound();

        context.Obras.Remove(obra);
        await context.SaveChangesAsync();

        return Ok("Obra deletada com sucesso");
    }
}