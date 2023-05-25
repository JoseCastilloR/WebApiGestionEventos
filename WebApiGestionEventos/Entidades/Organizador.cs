﻿using System.ComponentModel.DataAnnotations;
using WebApiGestionEventos.Validaciones;

namespace WebApiGestionEventos.Entidades
{
    public class Organizador
    {
        public int Id { get; set; }

        [Required]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set;}

        public List<Evento> Eventos { get; set; }
    }
}
