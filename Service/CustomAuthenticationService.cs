
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using POS.Database;
using POS.Interface;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace POS.Service
{
    public class CustomAuthenticationService : ICustomAuthenticationService
    {
        private readonly POS_db_Context _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomAuthenticationService(POS_db_Context context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<User> LoginAsync(string username, string password)
        {
            // Use projection to include only the required properties (e.g., UserID, UserName, Password)
            var user = await _context.Users
                .Where(u => u.UserName == username && u.Password == password)
                .Select(u => new User
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    Password = u.Password,
                    Role = u.Role
                })
                .FirstOrDefaultAsync();

            return user;
        }


        public async Task<bool> ChangePasswordAsync(string username, string oldPassword, string newPassword)
        {
            if (!ValidatePassword(newPassword))
            {
                return false;
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == username && u.Password == oldPassword);

            if (user == null)
            {
                return false;
            }

            user.Password = newPassword;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ForgetPasswordAsync(string email, string newPassword)
        {
            if (!ValidatePassword(newPassword))
            {
                return false;
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return false;
            }

            user.Password = newPassword;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task LogoutAsync()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context != null)
            {
                await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
        }

        private bool ValidatePassword(string password)
        {
            // Check if password meets the criteria
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");
            var hasSpecialChar = new Regex(@"[!@#$%^&*(),.?""':{}|<>]");

            return hasNumber.IsMatch(password) &&
                   hasUpperChar.IsMatch(password) &&
                   hasLowerChar.IsMatch(password) &&
                   hasMinimum8Chars.IsMatch(password) &&
                   hasSpecialChar.IsMatch(password);
        }
    }
}
