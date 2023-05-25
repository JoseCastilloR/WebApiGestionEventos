using System.ComponentModel.DataAnnotations;
using WebApiGestionEventos.Entidades;
using WebApiGestionEventos.Validaciones;

namespace WebApiGestionEventos.DTOs
{
    public class OrganizadorCreacionDTO
    {
        [Required]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }

        public List<Evento> Eventos { get; set; }
    }
}
