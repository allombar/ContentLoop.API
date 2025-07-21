using ContentLoop.BLL.Models;
using ContentLoop.DAL.Entities;

namespace ContentLoop.BLL.Mappers
{
    public static class BllToDalMapper
    {
        public static UserEntity ToEntity(this UserModel user)
        {
            return new UserEntity()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
                Role = user.Role,
            };
        }
    }
}
