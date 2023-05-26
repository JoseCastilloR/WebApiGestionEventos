using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApiGestionEventos.Entidades;

namespace WebApiGestionEventos
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EventoUsuario>()
                .HasKey(eu => new { eu.EventoId, eu.UsuarioId });
        }

        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<EventoUsuario> EventosUsuarios { get; set; }
        
        public DbSet<Organizador> Organizadores { get; set; }
    }
}
