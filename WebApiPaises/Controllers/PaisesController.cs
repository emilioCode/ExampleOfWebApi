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
    [Route("api/Paises")]
    public class PaisesController : Controller
    {
        private readonly DbPaisesContext context;

        public PaisesController(DbPaisesContext context)
        {
            this.context = context;
        }
        
        [HttpGet]
        public async  Task<IEnumerable<Pais>> Get()
        {
            return await context.Pais.ToListAsync();     
        }
      
        [HttpGet("{id}",Name ="paisCreado")]
        public async Task<IActionResult> GetById(int id)
        {
            var pais = await context.Pais.Include(p=>p.Provincia).FirstOrDefaultAsync(p => p.Id == id);
           
            if (pais is null) return NotFound();

            return Ok(pais);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Pais pais)
        {
            if (ModelState.IsValid)
            {
                await context.Pais.AddAsync(pais);
                await context.SaveChangesAsync();
                return new CreatedAtRouteResult("paisCreado", new { id = pais.Id }, pais);
            }
            return BadRequest(ModelState);
        }

        [HttpPut/*("{id}")*/]
        public async Task<IActionResult> Put([FromBody] Pais pais/*, int id*/)
        {
            var pa = await context.Pais.FirstOrDefaultAsync(p => p.Id == pais.Id);

            if (pa == null) return BadRequest();

            pa.Nombre = pais.Nombre;
            context.Entry(pa).State = EntityState.Modified;       

            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var pais = await context.Pais.FirstOrDefaultAsync(p => p.Id == id);

            if (pais == null) return NotFound("No existe tal elemento.");

            context.Remove(pais);
            await context.SaveChangesAsync();

            return Ok($"{pais.Nombre} se ha eliminado.");
        }

    }
}