using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiGestionEventos.DTOs;
using WebApiGestionEventos.Entidades;

namespace WebApiGestionEventos.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public UsuariosController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        //[HttpGet]
        //public async Task<ActionResult<List<Usuario>>> Get()
        //{
        //    return await context.Usuarios.ToListAsync();
        //}

        [HttpGet]
        public async Task<List<UsuarioDTO>> Get()
        {
            var usuarios = await context.Usuarios.ToListAsync();
            return mapper.Map<List<UsuarioDTO>>(usuarios);
        }

        [HttpGet("{id:int}", Name = "ObtenerUsuario")]
        public async Task<ActionResult<UsuarioDTOConEventos>> Get(int id)
        {
            var usuario = await context.Usuarios
                .Include(usuarioBD =>  usuarioBD.EventosUsuarios)
                .ThenInclude(eventoUsuarioBD => eventoUsuarioBD.Evento)
                .FirstOrDefaultAsync(usuarioBD => usuarioBD.Id == id);

            if (usuario == null)
            {
                return NotFound("El usuario no fue existe");
            }

            return mapper.Map<UsuarioDTOConEventos>(usuario);
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<List<UsuarioDTO>>> Get(string nombre)
        {
            var usuarios = await context.Usuarios.Where(usuarioBD => usuarioBD.Nombre.Contains(nombre)).ToListAsync();
            
            return mapper.Map<List<UsuarioDTO>>(usuarios);
        }

        //[HttpPost]
        //public async Task<ActionResult> Post(UsuarioCreacionDTO usuarioCreacionDTO)
        //{
        //    var existeUsuarioConMismoNombre = await context.Usuarios.AnyAsync(x => x.Nombre == usuarioCreacionDTO.Nombre);

        //    if (existeUsuarioConMismoNombre)
        //    {
        //        return BadRequest($"Ya existe un usuario con el nombre {usuarioCreacionDTO.Nombre}");
        //    }

        //    var usuario = mapper.Map<Usuario>(usuarioCreacionDTO);

        //    context.Add(usuario);
        //    await context.SaveChangesAsync();
        //    return Ok();
        //}

        [HttpPost]
        public async Task<ActionResult> Post(UsuarioCreacionDTO usuarioCreacionDTO)
        {
            var eventosIds = await context.Eventos
                .Where(eventoBD => usuarioCreacionDTO.EventosIds.Contains(eventoBD.Id)).Select(x => x.Id).ToListAsync();

            if (usuarioCreacionDTO.EventosIds.Count != eventosIds.Count)
            {
                return BadRequest("No existe alguno de los eventos enviados.");
            }

            var usuario = mapper.Map<Usuario>(usuarioCreacionDTO);
            context.Add(usuario);
            await context.SaveChangesAsync();

            var usuarioDTO = mapper.Map<UsuarioDTO>(usuario);

            return CreatedAtRoute("ObtenerUsuario", new {id=usuario.Id}, usuarioDTO);
        }

        //[HttpPut("{id:int}")]
        //public async Task<ActionResult> Put(UsuarioCreacionDTO usuarioCreacionDTO, int id)
        //{
        //    var existe = await context.Usuarios.AnyAsync(x => x.Id == id);

        //    if (!existe)
        //    {
        //        return NotFound();
        //    }

        //    var eventosIds = await context.Eventos
        //        .Where(eventoBD => usuarioCreacionDTO.EventosIds.Contains(eventoBD.Id)).Select(x => x.Id).ToListAsync();

        //    if (usuarioCreacionDTO.EventosIds.Count != eventosIds.Count)
        //    {
        //        return BadRequest("No existe alguno de los eventos enviados.");
        //    }

        //    var usuario = mapper.Map<Usuario>(usuarioCreacionDTO);
        //    usuario.Id = id;

        //    context.Update(usuario);
        //    await context.SaveChangesAsync();
        //    return NoContent();
        //}

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, UsuarioCreacionDTO usuarioCreacionDTO)
        {
            var usuarioBD = await context.Usuarios
                .Include(x => x.EventosUsuarios)
                .FirstOrDefaultAsync(x => x.Id == id);

            if(usuarioBD == null)
            {
                return NotFound();
            }

            usuarioBD = mapper.Map(usuarioCreacionDTO, usuarioBD);

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Usuarios.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound("El ID del usuario no existe.");
            }

            context.Remove(new Usuario() { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
