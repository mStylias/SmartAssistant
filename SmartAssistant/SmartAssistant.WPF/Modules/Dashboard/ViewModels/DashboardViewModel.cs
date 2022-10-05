using Prism.Mvvm;
using Prism.Regions;
using SmartAssistant.Services.UserAccount;

namespace SmartAssistant.WPF.Modules.Dashboard.ViewModels;

public class DashboardViewModel : BindableBase, INavigationAware
{
    private string _username;
    public string Username
    {
        get { return _username; }
        set { SetProperty(ref _username, value); }
    }

    private readonly IUserAuthRepository _userAuthRepository;

    public DashboardViewModel(IUserAuthRepository userAuthRepository)
    {
        _userAuthRepository = userAuthRepository;
    }

    public bool IsNavigationTarget(NavigationContext navigationContext)
    {
        return false;
    }

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {
        
    }

    public async void OnNavigatedTo(NavigationContext navigationContext)
    {
        var user = await _userAuthRepository.GetLoggedInUser();
        Username = user.Username;
    }
}