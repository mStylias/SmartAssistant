using SmartAssistant.Data.Models;

namespace SmartAssistant.Services.UserAccount;

public interface IUserAuthRepository
{
    Task<User> GetLoggedInUser();
    Task<bool> Login(string username, string password);
    Task<bool> Logout();
}