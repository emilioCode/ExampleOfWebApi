using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPaises.Models;

namespace WebApiPaises.Controllers
{
    [Produces("application/json")]
    [Route("api/Paises/{IdPais}/Provincia/")]
    public class ProvinciaController : Controller
    {
        private DbPaisesContext context { get; }

        public ProvinciaController(DbPaisesContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Provincia>> GetProvincias(int IdPais)
        {
            return await context.Provincia.Where(p => p.IdPais == IdPais).ToListAsync();
        }

        [HttpGet("{id}", Name = "provinciaCreado")]
        public async Task<IActionResult> GetById(int id)
        {
            var provincia = await context.Provincia.FirstOrDefaultAsync(p => p.Id == id);

            if (provincia is null) return NotFound();

            return Ok(provincia);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Provincia provincia, int IdPais)
        {
            if (ModelState.IsValid)
            {
                provincia.IdPais = IdPais;
                await context.Provincia.AddAsync(provincia);
                await context.SaveChangesAsync();
                return new CreatedAtRouteResult("provinciaCreado", new { id = provincia.Id }, provincia);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Provincia provincia, int IdPais)
        {
            var pa = await context.Provincia.FirstOrDefaultAsync(p => p.Id == provincia.Id && p.IdPais==IdPais);

            if (pa == null) return BadRequest();

            pa.Nombre = provincia.Nombre;
            if (provincia.IdPais != null) pa.IdPais = provincia.IdPais;
            context.Entry(pa).State = EntityState.Modified;

            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var provincia = await context.Provincia.FirstOrDefaultAsync(p => p.Id == id);

            if (provincia == null) return NotFound("No existe tal elemento.");

            context.Remove(provincia);
            await context.SaveChangesAsync();

            return Ok($"{provincia.Nombre} de {provincia.IdPaisNavigation.Nombre} se ha eliminado.");
        }

    }
}