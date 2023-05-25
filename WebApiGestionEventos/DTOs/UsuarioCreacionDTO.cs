using System.ComponentModel.DataAnnotations;
using WebApiGestionEventos.Validaciones;

namespace WebApiGestionEventos.DTOs
{
    public class UsuarioCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 120, ErrorMessage = "El campo {0} no debe de tener mas de {1} caracteres")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }
    }
}
