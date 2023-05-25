using WebApiGestionEventos.Entidades;

namespace WebApiGestionEventos.DTOs
{
    public class OrganizadorDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public List<EventoDTO> Eventos { get; set; }
    }
}
