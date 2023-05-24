namespace WebApiGestionEventos.Entidades
{
    public class EventoUsuario
    {
        public int EventoId { get; set; }

        public int UsuarioId { get; set; }

        public bool Asistio { get; set; }

        public bool Favorito { get; set; }

        public Evento Evento { get; set; }

        public Usuario Usuario { get; set; }
    }
}
