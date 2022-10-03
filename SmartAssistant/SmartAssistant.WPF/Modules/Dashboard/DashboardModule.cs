using Prism.Ioc;
using Prism.Modularity;
using SmartAssistant.WPF.Modules.Dashboard.Views;

namespace SmartAssistant.WPF.Modules.Dashboard;

public class DashboardModule : IModule
{
    public void OnInitialized(IContainerProvider containerProvider)
    {
        
    }

    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<DashboardView>();
    }
}