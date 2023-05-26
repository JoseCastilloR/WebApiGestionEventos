using AutoMapper;
using WebApiGestionEventos.DTOs;
using WebApiGestionEventos.Entidades;

namespace WebApiGestionEventos.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UsuarioCreacionDTO, Usuario>()
                .ForMember(usuario => usuario.EventosUsuarios, opciones => opciones.MapFrom(MapEventosUsuarios));
            CreateMap<Usuario, UsuarioDTO>();
            CreateMap<Usuario, UsuarioDTOConEventos>()
                .ForMember(usuarioDTO => usuarioDTO.Eventos, opciones => opciones.MapFrom(MapUsuarioDTOEventos));

            CreateMap<EventoCreacionDTO, Evento>();
            CreateMap<Evento, EventoDTO>();

            CreateMap<OrganizadorCreacionDTO, Organizador>();
            CreateMap<Organizador, OrganizadorDTO>();
        }

        private List<EventoUsuario> MapEventosUsuarios(UsuarioCreacionDTO usuarioCreacionDTO, Usuario usuario)
        {
            var resultado = new List<EventoUsuario>();

            if(usuarioCreacionDTO.EventosIds == null)
            {
                return resultado;
            }

            foreach (var eventoId in usuarioCreacionDTO.EventosIds)
            {
                resultado.Add(new EventoUsuario() { EventoId = eventoId });
            }

            return resultado;
        }

        private List<EventoDTO> MapUsuarioDTOEventos(Usuario usuario, UsuarioDTO usuarioDTO)
        {
            var resultado = new List<EventoDTO>();

            if(usuario.EventosUsuarios == null)
            {
                return resultado;
            }

            foreach(var eventoUsuario in usuario.EventosUsuarios)
            {
                resultado.Add(new EventoDTO()
                {
                    Id = eventoUsuario.EventoId,
                    Nombre = eventoUsuario.Evento.Nombre,
                    FechaHora = eventoUsuario.Evento.FechaHora,
                    Ubicacion = eventoUsuario.Evento.Ubicacion,
                    OrganizadorId = eventoUsuario.Evento.OrganizadorId,

                });
            }

            return resultado;
        }
    }
}
