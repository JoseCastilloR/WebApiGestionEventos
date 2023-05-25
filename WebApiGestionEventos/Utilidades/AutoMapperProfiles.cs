using AutoMapper;
using WebApiGestionEventos.DTOs;
using WebApiGestionEventos.Entidades;

namespace WebApiGestionEventos.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UsuarioCreacionDTO, Usuario>();
            CreateMap<Usuario, UsuarioDTO>();
            CreateMap<EventoCreacionDTO, Evento>();
            CreateMap<Evento, EventoDTO>();
            CreateMap<OrganizadorCreacionDTO, Organizador>();
            CreateMap<Organizador, OrganizadorDTO>();
        }
    }
}
