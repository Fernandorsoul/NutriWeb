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

        public Task<UserModel> GetByIdAsync(int id)
        {
            var userById = _nutriContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (userById == null)
            {
                throw new Exception($"O Id {id}, não foi encontrado");
            }
            return userById;
        }

        public async Task<UserModel> UpdateAsync(UserModel user, int id)
        {
            var existingUser = await _nutriContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (existingUser == null)
            {
                throw new Exception($"O Id {user.Id}, não foi encontrado");
            }
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;

            await _nutriContext.SaveChangesAsync();
            return user;
        }
         public async Task<UserModel> CreateAsync(UserModel user, int id, string email)
        {
            var existingUser = await _nutriContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            var existingEmail = await _nutriContext.Users.FirstOrDefaultAsync(e=> e.Email == email);
            if (existingUser != null)
            {
                throw new Exception($"Usuario {id}, já está cadastrado!!");
                
            }
            else if (existingEmail != null)
            {
                throw new Exception($"Email {email} ja cadastrado na base de dados!");
            }

            _nutriContext.Users.Add(user);
            _nutriContext.SaveChanges();

            return (existingUser);

            
        }

        public async Task<UserModel> DeleteAsync(int id)
        {
            var userById = await _nutriContext.Users.FirstOrDefaultAsync(x => id == id);
            if(userById == null)
            {
                throw new Exception($"O usuário não existe");
            }
            _nutriContext.Users.Remove(userById);
            await _nutriContext.SaveChangesAsync();
            return userById;

        }
    }
}