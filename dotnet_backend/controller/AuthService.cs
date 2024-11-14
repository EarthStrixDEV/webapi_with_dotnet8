using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using model;
using EnumStatus;

namespace controller
{
    public class AuthService {

        private readonly AppDbContext _dbContext;

        public AuthService(AppDbContext dbContext) {
            _dbContext = dbContext;
        }
        
        public async Task<AuthResult> UserSignUp(Users newUser) {
            var findUser = await _dbContext.Users.FirstOrDefaultAsync(user => user.Name == newUser.Name && user.Email == newUser.Email);
            const string EmailFormat = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            if (findUser is not null) {
                return new AuthResult {
                    IsSuccess = false,
                    Message = "User already signed up ,Please enter your new username",
                    Status = (int)Status.BadRequest
                };
            }

            if (Regex.IsMatch(newUser.Email ,EmailFormat)) {
                return new AuthResult {
                    IsSuccess = false,
                    Message = "Email format is invalid",
                    Status = (int)Status.BadRequest
                };
            }

            if (!IsPasswordStrong(newUser.Password)) {
                return new AuthResult {
                    IsSuccess = false,
                    Message = "Password is not strong enough",
                    Status = (int)Status.BadRequest
                };
            }

            // create a new user in database
            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync();

            return new AuthResult {
                IsSuccess = false,
                Message = "New user added successfully",
                Status = (int)Status.Ok
            };
        }

        public async Task<AuthResult> UserSignIn(UserAuth user) {
            var findUser = await _dbContext.Users.FirstOrDefaultAsync(data => data.Email == user.Email && data.Password == user.Password);
            
            if (findUser is null) {
                return new AuthResult {
                    IsSuccess = false,
                    Message = "Invalid username or password",
                    Status = (int)Status.NotFound
                };
            }

            return new AuthResult {
                IsSuccess = true,
                Message = "Sign in successfully",
                Status = (int)Status.Ok
            };
        }

        public async Task<AuthResult> AdminSignIn(Users user) {
            var findUser = await _dbContext.Users.FirstOrDefaultAsync(data => data.Email == user.Email && data.Password == user.Password);

            if (findUser is null) {
                return new AuthResult {
                    IsSuccess = false,
                    Message = "Invalid username or password",
                    Status = (int)Status.NotFound
                };
            }

            return new AuthResult {
                IsSuccess = true,
                Message = "Sign in successfully",
                Status = (int)Status.Ok
            };
        }

        private bool IsPasswordStrong(string Password) {
            string uppercase = @"[a-z]";
            string lowercase = @"[A-Z]";
            string digit = @"[0-9]";
            string pattern = @"[!@#$%^&*]";

            if (Password is null || Password.Length == 0) {
                return false;
            }

            if (Regex.IsMatch(Password, uppercase) &&
                Regex.IsMatch(Password, lowercase) &&
                Regex.IsMatch(Password, digit) &&
                Regex.IsMatch(Password, pattern)) {
                return true;
            }

            return false;
        }
    }
}