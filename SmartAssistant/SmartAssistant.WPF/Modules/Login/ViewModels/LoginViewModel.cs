using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using SmartAssistant.Services.UserAccount;
using SmartAssistant.WPF.Core;
using System;

namespace SmartAssistant.WPF.Modules.Login.ViewModels;

public class LoginViewModel : BindableBase
{
    private readonly IRegionManager _regionManager;
    private readonly IUserAuthRepository _authRepository;

    public DelegateCommand LoginCommand { get; private set; }

    public LoginViewModel(IRegionManager regionManager, IUserAuthRepository authRepository)
	{
        LoginCommand = new DelegateCommand(Login, LoginCanExecute);
        _regionManager = regionManager;
        _authRepository = authRepository;
    }

    private string _username;
    public string Username
    {
        get { return _username; }
        set
        { 
            SetProperty(ref _username, value);
            LoginCommand.RaiseCanExecuteChanged();
        }
    }

    private bool LoginCanExecute()
    {
        return !string.IsNullOrEmpty(Username);
    }

    private async void Login()
	{
        if (string.IsNullOrEmpty(Username))
            return;

        if (await _authRepository.Login(_username, ""))
        {
            var param = new NavigationParameters();
            param.Add("username", _username);

            _regionManager.RequestNavigate(RegionNames.MainWindowContentRegion, "MainRegionView");
        }     
	}
}