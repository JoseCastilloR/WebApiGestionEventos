using System.ComponentModel.DataAnnotations;
using WebApiGestionEventos.Entidades;
using WebApiGestionEventos.Validaciones;

namespace WebApiGestionEventos.DTOs
{
    public class EventoCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 120, ErrorMessage = "El campo {0} no debe de tener mas de {1} caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 120, ErrorMessage = "El campo {0} no debe de tener mas de {1} caracteres")]
        [PrimeraLetraMayuscula]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime FechaHora { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 120, ErrorMessage = "El campo {0} no debe de tener mas de {1} caracteres")]
        [PrimeraLetraMayuscula]
        public string Ubicacion { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Range(0, 300, ErrorMessage = "El campo {0} debe ser ser mayor a 0 y no exceder de 300")]
        public int CapacidadMaxima { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int LugaresDisponibles { get; set; }
    }
}