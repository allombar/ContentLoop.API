using System.ComponentModel.DataAnnotations;

namespace ContentLoop.API.Dto.Article.Patch
{
    public class UpdateArticleDto
    {
        [Required(ErrorMessage = "Le titre est obligatoire")]
        [StringLength(80, MinimumLength = 3, ErrorMessage = "Le titre doit faire entre 3 et 80 caractères")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "Le contenu est obligatoire")]
        [StringLength(150000, MinimumLength = 200, ErrorMessage = "Le contenu de l'article est trop court ou trop long.")]
        public required string Content { get; set; }
        [Required(ErrorMessage = "La description est obligatoire")]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "La description doit faire entre 10 et 200 caractères")]
        public required string Description { get; set; }
    }
}
