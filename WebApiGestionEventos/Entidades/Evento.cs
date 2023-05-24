﻿using System.ComponentModel.DataAnnotations;
using WebApiGestionEventos.Validaciones;

namespace WebApiGestionEventos.Entidades
{
    public class Evento: IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 15, ErrorMessage = "El campo {0} no debe de tener más de {1} caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 100, ErrorMessage = "El campo {0} no debe de tener más de {1} caracteres")]
        [PrimeraLetraMayuscula]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime FechaHora { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 20, ErrorMessage = "El campo {0} no debe de tener más de {1} caracteres")]
        [PrimeraLetraMayuscula]
        public string Ubicacion { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(0, 300, ErrorMessage ="El campo {0} debe ser ser mayor a 0 y no exceder de 300")]
        public int CapacidadMaxima { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int LugaresDisponibles { get; set; }

        public List<EventoUsuario> EventosUsuarios { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Nombre))
            {
                var primeraLetra = Nombre[0].ToString();

                if(primeraLetra != primeraLetra.ToUpper())
                {
                    yield return new ValidationResult("La primera letra debe ser mayúscula", new string[] { nameof(Nombre) });
                }
            }
        }
    }
}
