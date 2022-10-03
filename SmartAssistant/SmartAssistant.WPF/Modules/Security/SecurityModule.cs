using Prism.Ioc;
using Prism.Modularity;
using SmartAssistant.WPF.Modules.Security.Views;

namespace SmartAssistant.WPF.Modules.Security;

public class SecurityModule : IModule
{
    public void OnInitialized(IContainerProvider containerProvider)
    {
        
    }

    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<SecurityView>();
    }
}