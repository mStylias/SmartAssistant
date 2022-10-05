using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SmartAssistant.WPF.Core;
using System;

namespace SmartAssistant.WPF.Modules.SmartDevices.ViewModels;

public class SmartDevicesViewModel : BindableBase, INavigationAware
{
    private readonly IRegionManager _regionManager;

    public SmartDevicesViewModel(IRegionManager regionManager)
    {
        _regionManager = regionManager;
    }

    public void NavigateToLights()
    {
        _regionManager.RequestNavigate(RegionNames.MainContentRegion, "LightsView");
    }

    public void NavigateToThermostat()
    {
        _regionManager.RequestNavigate(RegionNames.MainContentRegion, "ThermostatView");
    }

    public void NavigateToAirConditioner()
    {
        _regionManager.RequestNavigate(RegionNames.MainContentRegion, "AirConditionerView");
    }

    public void NavigateToSmartShoerack()
    {
        _regionManager.RequestNavigate(RegionNames.MainContentRegion, "SmartShoerackView");
    }

    public void NavigateToSmartFeeder()
    {
        _regionManager.RequestNavigate(RegionNames.MainContentRegion, "SmartFeederView");
    }

    public bool IsNavigationTarget(NavigationContext navigationContext)
    {
        return false;
    }

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {
        
    }

    public void OnNavigatedTo(NavigationContext navigationContext)
    {
        
    }
}