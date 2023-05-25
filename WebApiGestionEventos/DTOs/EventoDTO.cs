namespace WebApiGestionEventos.DTOs
{
    public class EventoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public DateTime FechaHora { get; set; }

        public string Ubicacion { get; set; }

        public int OrganizadorId { get; set; }
    }
}
