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

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UsuarioDTO>> Get(int id)
        {
            var usuario = await context.Usuarios.FirstOrDefaultAsync(usuarioBD => usuarioBD.Id == id);

            if (usuario == null)
            {
                return NotFound("El usuario no fue existe");
            }

            return mapper.Map<UsuarioDTO>(usuario);
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<List<UsuarioDTO>>> Get(string nombre)
        {
            var usuarios = await context.Usuarios.Where(usuarioBD => usuarioBD.Nombre.Contains(nombre)).ToListAsync();
            
            return mapper.Map<List<UsuarioDTO>>(usuarios);
        }

        [HttpPost]
        public async Task<ActionResult> Post(UsuarioCreacionDTO usuarioCreacionDTO)
        {
            var existeUsuarioConMismoNombre = await context.Usuarios.AnyAsync(x => x.Nombre == usuarioCreacionDTO.Nombre);

            if (existeUsuarioConMismoNombre)
            {
                return BadRequest($"Ya existe un usuario con el nombre {usuarioCreacionDTO.Nombre}");
            }

            var usuario = mapper.Map<Usuario>(usuarioCreacionDTO);

            context.Add(usuario);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Usuario usuario, int id)
        {
            if (usuario.Id != id)
            {
                return BadRequest("El ID del usuario no coincide con el ID de la URL.");
            }

            var existe = await context.Usuarios.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }


            context.Update(usuario);
            await context.SaveChangesAsync();
            return Ok();
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
