using Microsoft.EntityFrameworkCore;
using NutriWeb.Data;
using NutriWeb.Interfaces;
using NutriWeb.Models;

namespace NutriWeb.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly NutriContext _nutriContext;
        public UserRepository(NutriContext context)
        {
            _nutriContext = context;
        }

        public async Task<List<UserModel>> GetAllAsync()
        {
            return await _nutriContext.Users.ToListAsync();
        }

        public async Task<UserModel> GetByEmailAsync(string email)
        {
            var userByEmail = await _nutriContext.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (userByEmail == null)
            {
                throw new Exception("O E-mail não foi encontrado.");
            }

            return userByEmail;
        }

        public async Task<UserModel> GetByIdAsync(int id)
        {
            var userById = await _nutriContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (userById == null)
            {
                throw new Exception($"Usuário com ID {id} não encontrado.");
            }
            return userById;
        }

        public async Task<UserModel> UpdateAsync(UserModel user, int id)
        {
            var existingUser = await _nutriContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (existingUser == null)
            {
                throw new Exception($"Usuário com ID {id} não encontrado.");
            }

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;

            await _nutriContext.SaveChangesAsync();
            return existingUser;
        }

        public async Task<UserModel> CreateAsync(UserModel user, int id, string email)
        {
            var existingUser = await _nutriContext.Users
                .Where(x => x.Id == id || x.Email == email)
                .FirstOrDefaultAsync();
            
            if (existingUser != null)
            {
                if (existingUser.Id == id)
                    throw new Exception($"Usuário com ID {id} já está cadastrado.");
                if (existingUser.Email == email)
                    throw new Exception($"E-mail {email} já está cadastrado na base de dados.");
            }

            await _nutriContext.Users.AddAsync(user);
            await _nutriContext.SaveChangesAsync();

            return user;
        }

        public async Task<UserModel> DeleteAsync(int id)
        {
            var userById = await _nutriContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (userById == null)
            {
                throw new Exception($"Usuário com ID {id} não encontrado.");
            }

            _nutriContext.Users.Remove(userById);
            await _nutriContext.SaveChangesAsync();
            return userById;
        }
    }
}
