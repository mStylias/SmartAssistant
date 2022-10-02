namespace SmartAssistant.Services.UserAccount;

public interface IUserAuthRepository
{
    Task<bool> Authenticate(string username, string password);
    Task<bool> Logout();
}