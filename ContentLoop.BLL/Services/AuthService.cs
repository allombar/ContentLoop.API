using ContentLoop.BLL.Interfaces;
using ContentLoop.BLL.Mappers;
using ContentLoop.BLL.Models;
using ContentLoop.DAL.Entities;
using ContentLoop.DAL.Interfaces;
using Isopoh.Cryptography.Argon2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentLoop.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repository;

        public AuthService(IAuthRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserModel> LoginAsync(string email, string password)
        {
            UserEntity? entity = await _repository.GetByEmailAsync(email);

            if (entity == null || !Argon2.Verify(entity.Password, password))
                throw new ArgumentException("Email ou mot de passse incorrect");

            return entity.ToBll();
        }

        public async Task<UserModel> RegisterAsync(UserModel user)
        {
            UserEntity? entity = await _repository.GetByEmailAsync(user.Email);

            if (entity is not null)
                throw new ArgumentException("Impossible de créer le compte. Vérifiez vos informations");

            user.Password = Argon2.Hash(user.Password);

            UserEntity created = await _repository.AddAsync(user.ToEntity());

            return created.ToBll();
        }

        public async Task<UserModel> GetByIdAsync(Guid id)
        {
            UserEntity? entity = await _repository.GetByIdAsync(id);

            if (entity is null)
                throw new ArgumentException("Une erreur est survenue.");

            return entity.ToBll();
        }
    }
}
