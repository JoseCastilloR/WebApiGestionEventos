using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiGestionEventos.Entidades;

namespace WebApiGestionEventos.Controllers
{
    [ApiController]
    [Route("api/eventos")]
    public class EventosController: ControllerBase
    {
        private readonly ApplicationDbContext context;

        public EventosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [HttpGet("listado")]
        [HttpGet("/listado-eventos")]
        public async Task<ActionResult<List<Evento>>> Get()
        {
           return await context.Eventos.ToListAsync();
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<Evento>> Get(string nombre)
        {
            var evento = await context.Eventos.FirstOrDefaultAsync(x => x.Nombre.Contains(nombre));

            if (evento == null)
            {
                return NotFound("El nombre del evento no coincide con ningun registro.");
            }

            return evento;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Evento evento)
        {
            var existeEventoConMismoNombre = await context.Eventos.AnyAsync(x => x.Nombre == evento.Nombre);

            if (existeEventoConMismoNombre)
            {
                return BadRequest($"Ya existe un evento con el nombre {evento.Nombre}");
            }

            context.Add(evento);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Evento evento, int id)
        {
            if(evento.Id != id)
            {
                return BadRequest("El ID del evento no coincide con el ID de la URL.");
            }

            var existe = await context.Eventos.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }


            context.Update(evento);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Eventos.AnyAsync(x => x.Id == id);

            if(!existe)
            {
                return NotFound("El ID del evento no existe.");
            }

            context.Remove(new Evento() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
