using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using SmartAssistant.Services.UserAccount;
using SmartAssistant.WPF.Core;
using SmartAssistant.WPF.Modules.Login.Views;

namespace SmartAssistant.WPF.Modules.Login;

public class LoginModule : IModule
{
    private readonly IRegionManager _regionManager;
    private readonly IUserAuthRepository _authRepository;

    public LoginModule(IRegionManager regionManager, IUserAuthRepository authRepository)
    {
        _regionManager = regionManager;
        _authRepository = authRepository;
    }

    public async void OnInitialized(IContainerProvider containerProvider)
    {
        if (await _authRepository.GetLoggedInUser() == null)
            _regionManager.RequestNavigate(RegionNames.MainWindowContentRegion, nameof(LoginView));
    }

    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<LoginView>();
    }
}