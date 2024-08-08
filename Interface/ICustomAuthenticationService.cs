using POS.Database;
using System.Threading.Tasks;

namespace POS.Interface
{
    public interface ICustomAuthenticationService
    {
        Task<User> LoginAsync(string username, string password);
        Task<bool> ChangePasswordAsync(string username, string oldPassword, string newPassword);
        Task<bool> ForgetPasswordAsync(string email, string newPassword);
        Task LogoutAsync();
    }
}
