using ContentLoop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentLoop.DAL.Interfaces
{
    public interface IAuthRepository
    {
        public Task<UserEntity?> GetByEmailAsync(string email);
        public Task<UserEntity?> GetByIdAsync(Guid id);
        public Task<UserEntity> AddAsync(UserEntity user);
    }
}
