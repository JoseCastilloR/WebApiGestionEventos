using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiGestionEventos.Entidades;

namespace WebApiGestionEventos.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public UsuariosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> Get()
        {
            return await context.Usuarios.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Usuario usuario)
        {
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
