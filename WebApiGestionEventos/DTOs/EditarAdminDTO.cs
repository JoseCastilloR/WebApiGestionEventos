using System.ComponentModel.DataAnnotations;

namespace WebApiGestionEventos.DTOs
{
    public class EditarAdminDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
