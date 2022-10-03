using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SmartAssistant.WPF.Core;

namespace SmartAssistant.WPF.Modules.MainRegion.ViewModels;

public class MainRegionViewModel : BindableBase
{
    private readonly IRegionManager _regionManager;

    public DelegateCommand NavigateToDashboardCommand { get; private set; }
    public DelegateCommand NavigateToCalendarCommand { get; private set; }
    public DelegateCommand NavigateToSmartDevicesCommand { get; private set; }
    public DelegateCommand NavigateToSecurityCommand { get; private set; }

    public MainRegionViewModel(IRegionManager regionManager)
    {
        NavigateToDashboardCommand = new DelegateCommand(NavigateToDashboard);
        NavigateToCalendarCommand = new DelegateCommand(NavigateToCalendar);
        NavigateToSmartDevicesCommand = new DelegateCommand(NavigateToSmartDevices);
        NavigateToSecurityCommand = new DelegateCommand(NavigateToSecurity);
        _regionManager = regionManager;
    }

    private void NavigateToDashboard()
    {
        _regionManager.RequestNavigate(RegionNames.MainContentRegion, "DashboardView");
    }

    private void NavigateToCalendar()
    {
        _regionManager.RequestNavigate(RegionNames.MainContentRegion, "CalendarView");
    }

    private void NavigateToSmartDevices()
    {
        _regionManager.RequestNavigate(RegionNames.MainContentRegion, "SmartDevicesView");
    }

    private void NavigateToSecurity()
    {
        _regionManager.RequestNavigate(RegionNames.MainContentRegion, "SecurityView");
    }
}