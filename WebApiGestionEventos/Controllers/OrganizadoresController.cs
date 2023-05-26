using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiGestionEventos.DTOs;
using WebApiGestionEventos.Entidades;

namespace WebApiGestionEventos.Controllers
{
    [ApiController]
    [Route("api/organizadores")]
    public class OrganizadoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public OrganizadoresController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<List<OrganizadorDTO>> Get()
        {
            var organizadores = await context.Organizadores.Include(organizadorBD => organizadorBD.Eventos).ToListAsync();
            return mapper.Map<List<OrganizadorDTO>>(organizadores);
        }

        [HttpGet("{id:int}", Name = "ObtenerOrganizador")]
        public async Task<ActionResult<OrganizadorDTO>> Get(int id)
        {
            var organizador = await context.Organizadores.Include(organizadorBD => organizadorBD.Eventos).FirstOrDefaultAsync(organizadorBD => organizadorBD.Id == id);

            if (organizador == null)
            {
                return NotFound("El organizador no existe");
            }

            return mapper.Map<OrganizadorDTO>(organizador);
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<List<OrganizadorDTO>>> Get(string nombre)
        {
            var organizadores = await context.Organizadores.Where(organizadorBD => organizadorBD.Nombre.Contains(nombre)).ToListAsync();

            return mapper.Map<List<OrganizadorDTO>>(organizadores);
        }

        [HttpPost]
        public async Task<ActionResult> Post(OrganizadorCreacionDTO organizadorCreacionDTO)
        {
            var existeOrganizadorConMismoNombre = await context.Organizadores.AnyAsync(x => x.Nombre == organizadorCreacionDTO.Nombre);

            if (existeOrganizadorConMismoNombre)
            {
                return BadRequest($"Ya existe un organizador con el nombre {organizadorCreacionDTO.Nombre}");
            }

            var organizador = mapper.Map<Organizador>(organizadorCreacionDTO);

            context.Add(organizador);
            await context.SaveChangesAsync();

            var organizadorDTO = mapper.Map<OrganizadorDTO>(organizador);

            return CreatedAtRoute("ObtenerOrganizador", new { id = organizador.Id }, organizadorDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Organizador organizador, int id)
        {
            if (organizador.Id != id)
            {
                return BadRequest("El ID del organizador no coincide con el ID de la URL");
            }

            var existe = await context.Organizadores.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }


            context.Update(organizador);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Organizadores.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound("El ID del organizador no existe.");
            }

            context.Remove(new Organizador() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
