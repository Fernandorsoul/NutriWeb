

using System.ComponentModel.DataAnnotations;

namespace NutriWeb.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string Password  {get; set;}

         [Required(ErrorMessage = "O papel do usuário é obrigatório.")]
        public string Role { get; set; } // Nutricionista ou Cliente

    }
}