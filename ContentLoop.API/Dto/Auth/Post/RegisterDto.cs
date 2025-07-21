using System.ComponentModel.DataAnnotations;

namespace ContentLoop.API.Dto.Auth.Post
{
    /// <summary>
    /// DTO pour l’inscription d’un utilisateur.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Champs requis : <c>FirstName</c>, <c>LastName</c>, <c>Email</c>, <c>Password</c>.
    /// Les noms sont validés via une regex côté backend.
    /// </para>
    /// <para>
    /// À synchroniser avec le frontend JS :
    /// </para>
    /// <code>
    /// const nameRegex = /^[a-zA-ZàâäéèêëïîôöùûüçÀÂÄÉÈÊËÏÎÔÖÙÛÜ\s\-']+$/;
    /// </code>
    /// </remarks>
    public class RegisterDto
    {
        [Required]
        [MaxLength(50)]
        [RegularExpression(@"^[a-zA-ZàâäéèêëïîôöùûüçÇÀÂÄÉÈÊËÏÎÔÖÙÛÜ\s\-']+$",
            ErrorMessage = "Le prénom contient des caractères non autorisés.")]
        public required string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        [RegularExpression(@"^[a-zA-ZàâäéèêëïîôöùûüçÇÀÂÄÉÈÊËÏÎÔÖÙÛÜ\s\-']+$",
            ErrorMessage = "Le nom contient des caractères non autorisés.")]
        public required string LastName { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
