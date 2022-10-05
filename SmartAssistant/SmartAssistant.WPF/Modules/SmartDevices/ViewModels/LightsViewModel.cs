using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SmartAssistant.WPF.Core;
using System;
using System.Windows;

namespace SmartAssistant.WPF.Modules.SmartDevices.ViewModels;

public class LightsViewModel : BindableBase, INavigationAware
{
    private Visibility _lampInactiveVisibility = Visibility.Visible;
    public Visibility LampInactiveVisibility
    {
        get { return _lampInactiveVisibility; }
        set { SetProperty(ref _lampInactiveVisibility, value); }
    }

    private Visibility _lampActiveVisibility = Visibility.Collapsed;
    public Visibility LampActiveVisibility
    {
        get { return _lampActiveVisibility; }
        set { SetProperty(ref _lampActiveVisibility, value); }
    }

    private bool _isToggleChecked = false;
    private readonly IRegionManager _regionManager;

    public bool IsToggleChecked
    {
        get { return _isToggleChecked; }
        set 
        { 
            SetProperty(ref _isToggleChecked, value);
            ToggleLightVisibility();
        }
    }
    
    public DelegateCommand GoToSmartDevicesMenuCommand { get; private set; }

    public LightsViewModel(IRegionManager regionManager)
    {
        GoToSmartDevicesMenuCommand = new DelegateCommand(GoToSmartDevicesMenu);
        _regionManager = regionManager;
    }

    private void GoToSmartDevicesMenu()
    {
        _regionManager.RequestNavigate(RegionNames.MainContentRegion, "SmartDevicesView");
    }

    private void ToggleLightVisibility()
    {
        if (LampActiveVisibility == Visibility.Collapsed)
        {
            LampActiveVisibility = Visibility.Visible;
            LampInactiveVisibility = Visibility.Collapsed;
        }
        else
        {
            LampActiveVisibility = Visibility.Collapsed;
            LampInactiveVisibility = Visibility.Visible;
        }
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