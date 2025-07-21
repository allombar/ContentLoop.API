using System.ComponentModel.DataAnnotations;

namespace ContentLoop.API.Dto.Auth.Get
{
    public class UserDto
    {
        public Guid Id { get; set; }
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string LastName { get; set; }
        [Required]
        public required string Role { get; set; }
    }
}
