using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiGestionEventos.DTOs;
using WebApiGestionEventos.Entidades;
using WebApiGestionEventos.Filtros;

namespace WebApiGestionEventos.Controllers
{
    [ApiController]
    [Route("api/organizadores/{organizadorId:int}/eventos")]
    public class EventosController: ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly ILogger<EventosController> logger;
        private readonly IMapper mapper;

        public EventosController(ApplicationDbContext context, ILogger<EventosController> logger, IMapper mapper)
        {
            this.context = context;
            this.logger = logger;
            this.mapper = mapper;
        }

        //[HttpGet]
        [HttpGet("listado")]
        [HttpGet("/listado-eventos")]
        [ServiceFilter(typeof(MiFiltroDeAccion))]
        //[Authorize]
        public async Task<List<EventoDTO>> Get()
        {
            logger.LogInformation("Estamos obteniendo los eventos");

            //return await context.Eventos.ToListAsync();

            var eventos = await context.Eventos.ToListAsync();

            return mapper.Map<List<EventoDTO>>(eventos);
        }

        //[HttpGet("{id:int}")]
        //public async Task<ActionResult<EventoDTO>> Get(int id)
        //{
        //    var evento = await context.Eventos.FirstOrDefaultAsync(eventoBD => eventoBD.Id == id);
        //    return mapper.Map<EventoDTO>(evento);
        //}

        //[HttpGet("{nombre}")]
        //[ResponseCache(Duration = 10)]
        //public async Task<ActionResult<Evento>> Get(string nombre)
        //{
        //    var evento = await context.Eventos.FirstOrDefaultAsync(x => x.Nombre.Contains(nombre));

        //    if (evento == null)
        //    {
        //        return NotFound("El nombre del evento no coincide con ningun registro.");
        //    }

        //    return evento;
        //}

        [HttpGet]
        public async Task<ActionResult<List<EventoDTO>>> Get(int organizadorId)
        {
            var existeOrganizador = await context.Organizadores.AnyAsync(organizadorBD => organizadorBD.Id == organizadorId);

            if (!existeOrganizador)
            {
                return NotFound();
            }

            var eventos = await context.Eventos.Where(eventoBD => eventoBD.OrganizadorId == organizadorId).ToListAsync();

            return mapper.Map<List<EventoDTO>>(eventos);
        }

        //[HttpPost]
        //public async Task<ActionResult> Post(EventoCreacionDTO eventoCreacionDTO)
        //{
        //    var evento = mapper.Map<Evento>(eventoCreacionDTO);

        //    context.Add(evento);
        //    await context.SaveChangesAsync();
        //    return Ok();
        //}

        [HttpPost]
        public async Task<ActionResult> Post(int organizadorId, EventoCreacionDTO eventoCreacionDTO)
        {
            var existeOrganizador = await context.Organizadores.AnyAsync(organizadorBD => organizadorBD.Id == organizadorId);

            if (!existeOrganizador)
            {
                return NotFound();
            }

            var evento = mapper.Map<Evento>(eventoCreacionDTO);
            evento.OrganizadorId = organizadorId;
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
