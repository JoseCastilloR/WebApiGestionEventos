namespace WebApiGestionEventos.DTOs
{
    public class UsuarioDTOConEventos : UsuarioDTO
    {
        public List<EventoDTO> Eventos { get; set; }
    }
}
