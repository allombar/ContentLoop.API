using ContentLoop.BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentLoop.BLL.Interfaces
{
    public interface IAuthService
    {
        public Task<UserModel> LoginAsync(string email, string password);
        public Task<UserModel> RegisterAsync(UserModel user);
        public Task<UserModel> GetByIdAsync(Guid id);
    }
}
