using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using SmartAssistant.WPF.Core;
using SmartAssistant.WPF.Modules.Login.Views;

namespace SmartAssistant.WPF.Modules.Login;

public class LoginModule : IModule
{
    private readonly IRegionManager _regionManager;

    public LoginModule(IRegionManager regionManager)
    {
        _regionManager = regionManager;
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
        _regionManager.RequestNavigate(RegionNames.MainWindowContentRegion, nameof(LoginView));
    }

    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<LoginView>();
    }
}