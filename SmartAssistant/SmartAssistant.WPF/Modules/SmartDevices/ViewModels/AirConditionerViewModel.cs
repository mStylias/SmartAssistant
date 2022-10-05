using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SmartAssistant.WPF.Core;
using System;
using System.Windows;

namespace SmartAssistant.WPF.Modules.SmartDevices.ViewModels;

public class AirConditionerViewModel : BindableBase, INavigationAware
{

    private Visibility _airConditionerInactiveVisibility = Visibility.Visible;
    public Visibility AirConditionerInactiveVisibility
    {
        get { return _airConditionerInactiveVisibility; }
        set { SetProperty(ref _airConditionerInactiveVisibility, value); }
    }

    private Visibility _airConditionerActiveVisibility = Visibility.Collapsed;
    public Visibility AirConditionerActiveVisibility
    {
        get { return _airConditionerActiveVisibility; }
        set { SetProperty(ref _airConditionerActiveVisibility, value); }
    }

    private bool _isToggleChecked = false;
    private readonly IRegionManager _regionManager;

    public bool IsToggleChecked
    {
        get { return _isToggleChecked; }
        set
        {
            SetProperty(ref _isToggleChecked, value);
            ToggleAirConditionerVisibility();
        }
    }

    private void ToggleAirConditionerVisibility()
    {
        if (AirConditionerActiveVisibility == Visibility.Collapsed)
        {
            AirConditionerActiveVisibility = Visibility.Visible;
            AirConditionerInactiveVisibility = Visibility.Collapsed;
        }
        else
        {
            AirConditionerActiveVisibility = Visibility.Collapsed;
            AirConditionerInactiveVisibility = Visibility.Visible;
        }
    }

    public DelegateCommand GoToSmartDevicesMenuCommand { get; private set; }

    public AirConditionerViewModel(IRegionManager regionManager)
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