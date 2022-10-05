using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SmartAssistant.WPF.Core;
using System.Windows;

namespace SmartAssistant.WPF.Modules.SmartDevices.ViewModels;

public class ThermostatViewModel : BindableBase, INavigationAware
{
    private bool _isToggleChecked = false;
    private readonly IRegionManager _regionManager;

    public bool IsToggleChecked
    {
        get { return _isToggleChecked; }
        set
        {
            SetProperty(ref _isToggleChecked, value);
        }
    }

    public DelegateCommand GoToSmartDevicesMenuCommand { get; private set; }

    public ThermostatViewModel(IRegionManager regionManager)
    {
        GoToSmartDevicesMenuCommand = new DelegateCommand(GoToSmartDevicesMenu);
        _regionManager = regionManager;
    }

    private void GoToSmartDevicesMenu()
    {
        _regionManager.RequestNavigate(RegionNames.MainContentRegion, "SmartDevicesView");
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