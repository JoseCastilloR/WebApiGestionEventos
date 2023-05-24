using System.ComponentModel.DataAnnotations;

namespace WebApiGestionEventos.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        public List<EventoUsuario> EventosUsuarios { get; set; }
    }
}
