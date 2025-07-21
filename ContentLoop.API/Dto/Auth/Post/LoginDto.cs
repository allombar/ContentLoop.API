using System.ComponentModel.DataAnnotations;

namespace ContentLoop.API.Dto.Auth.Post
{
    public class LoginDto
    {
        [Required(ErrorMessage = "L'email est requis")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Le mot de passe est requis")]
        [MinLength(6, ErrorMessage = "Le mot de passe doit faire minimum 6 caractères")]
        public string Password { get; set; } = null!;
    }
}
