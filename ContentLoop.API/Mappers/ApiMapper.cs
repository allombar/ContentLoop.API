using ContentLoop.API.Dto.Auth.Get;
using ContentLoop.API.Dto.Auth.Post;
using ContentLoop.BLL.Models;

namespace ContentLoop.API.Mappers
{
    public static class ApiMapper
    {
        public static UserDto ToDto(this UserModel user)
        {
            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role
            };
        }

        public static UserModel ToBll(this RegisterDto dto)
        {
            return new UserModel
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Password = dto.Password,
                Role = "user",
                CreatedAt = DateTime.Now
            };
        }
    }
}
