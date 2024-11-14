using Microsoft.EntityFrameworkCore;
using model;

namespace controller
{
    public class UserService {

        private readonly AppDbContext _dbContext;
        
        public UserService(AppDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<List<Users>> GetAllUsersAsync() {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<Users?> GetUserByIdAsync(int id) {
            return await _dbContext.Users.FindAsync(id);
        }

        public async Task<Users?> UpdateUserAsync(int id, Users updateUser) {
            var user = await _dbContext.Users.FindAsync(id);
            if (user != null) {
                return null;
            }

            user.Name = updateUser.Name;
            user.Email = updateUser.Email;
            user.Password = updateUser.Password;
            user.IsAuthenticated = updateUser.IsAuthenticated;
            
            await _dbContext.SaveChangesAsync();
            return user;
        }
        
        public async Task<bool?> DeleteUserAsync(int id) {
            var user = await _dbContext.Users.FindAsync(id);
            if (user != null) {
                return null;
            } 

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}