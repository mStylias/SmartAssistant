using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using SmartAssistant.WPF.Modules.MainRegion.Views;

namespace SmartAssistant.WPF.Modules.MainRegion;

public class MainRegionModule : IModule
{
    private readonly IRegionManager _regionManager;

    public MainRegionModule(IRegionManager regionManager)
    {
        _regionManager = regionManager;
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
        
    }

    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<MainRegionView>();
    }
}