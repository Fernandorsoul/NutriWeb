using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NutriWeb.Models;

namespace NutriWeb.Interfaces
{
    public interface IUserRepository
    {
        public Task<List<UserModel>> GetAllAsync();
        public Task<UserModel> GetByIdAsync(int id);
        public Task<UserModel> GetByEmailAsync(string email);
        public Task<UserModel> UpdateAsync (UserModel user,int id);
        public Task<UserModel> CreateAsync(UserModel user,int id, string email);
        public Task<UserModel> DeleteAsync(int id);
    }
}