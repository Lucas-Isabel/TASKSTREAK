using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;

namespace Data.MemoryRepositories
{
    public class UserMemoryRepository : IUserRepository
    {
        private readonly List<UserModel> _users = new List<UserModel>();

        public Task<IEnumerable<UserModel>> GetUsersAsync()
        {
            return Task.FromResult<IEnumerable<UserModel>>(_users);
        }

        public Task<UserModel> GetUserByIdAsync(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            return Task.FromResult(user);
        }

        public Task<UserModel> GetUserByEmailAsync(string email)
        {
            var user = _users.FirstOrDefault(u => u.Email == email);
            return Task.FromResult(user);
        }

        public Task<UserModel> GetUserByUserNameAsync(string userName)
        {
            var user = _users.FirstOrDefault(u => u.Username == userName);
            return Task.FromResult(user);
        }

        public Task<UserModel> AddUserAsync(UserModel user)
        {
            _users.Add(user);
            return Task.FromResult(user);
        }

        public Task<UserModel> UpdateUserAsync(UserModel user)
        {
            var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser != null)
            {
                _users.Remove(existingUser);
                _users.Add(user);
            }
            return Task.FromResult(user);
        }

        public Task<UserModel> DeleteUserAsync(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _users.Remove(user);
            }
            return Task.FromResult(user);
        }
    }
}
