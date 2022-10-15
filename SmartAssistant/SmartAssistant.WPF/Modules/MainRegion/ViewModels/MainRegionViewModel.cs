using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SmartAssistant.Services.UserAccount;
using SmartAssistant.WPF.Core;
using SmartAssistant.WPF.Modules.MainRegion.Views;
using SmartAssistant.WPF.Modules.Security.Views;
using System;
using System.Windows;
using System.Windows.Threading;

namespace SmartAssistant.WPF.Modules.MainRegion.ViewModels;

public class MainRegionViewModel : BindableBase, INavigationAware
{
    private Visibility _profileNotificationVisibility = Visibility.Collapsed;
    public Visibility ProfileNotificationVisibility
    {
        get { return _profileNotificationVisibility; }
        set { SetProperty(ref _profileNotificationVisibility, value); }
    }

    private readonly IRegionManager _regionManager;
    private readonly IUserAuthRepository _userAuthRepository;

    public DelegateCommand NavigateToDashboardCommand { get; private set; }
    public DelegateCommand NavigateToCalendarCommand { get; private set; }
    public DelegateCommand NavigateToSmartDevicesCommand { get; private set; }
    public DelegateCommand NavigateToSecurityCommand { get; private set; }

    public DelegateCommand HideNotificationEllipseCommand { get; private set; }
    public DelegateCommand OpenNotificationCommand { get; private set; }
    public DelegateCommand LogoutUserCommand { get; private set; }

    public DelegateCommand ShowHelpWindowCommand { get; private set; }

    public MainRegionViewModel(IRegionManager regionManager, IUserAuthRepository userAuthRepository)
    {
        NavigateToDashboardCommand = new DelegateCommand(NavigateToDashboard);
        NavigateToCalendarCommand = new DelegateCommand(NavigateToCalendar);
        NavigateToSmartDevicesCommand = new DelegateCommand(NavigateToSmartDevices);
        NavigateToSecurityCommand = new DelegateCommand(NavigateToSecurity);

        HideNotificationEllipseCommand = new DelegateCommand(HideProfileNotification);
        OpenNotificationCommand = new DelegateCommand(OpenNotification);
        LogoutUserCommand = new DelegateCommand(LogoutUser);

        ShowHelpWindowCommand = new DelegateCommand(ShowHelpWindow);

        DispatcherTimer notificationPopupTimer = new DispatcherTimer();
        notificationPopupTimer.Interval = new TimeSpan(0, 1, 0);
        notificationPopupTimer.Tick += notificationPopupTimer_Tick;
        notificationPopupTimer.Start();

        _regionManager = regionManager;
        _userAuthRepository = userAuthRepository;
    }

    private void ShowHelpWindow()
    {
        HelpWindow helpWindow = new HelpWindow();
        helpWindow.ShowDialog();
    }

    private void OpenNotification()
    {
        PetDamageWindow petDamageWindow = new PetDamageWindow();
        petDamageWindow.ShowDialog();
    }

    private void notificationPopupTimer_Tick(object sender, EventArgs e)
    {
        ProfileNotificationVisibility = Visibility.Visible;
    }

    private void NavigateToDashboard()
    {
        _regionManager.RequestNavigate(RegionNames.MainContentRegion, "DashboardView");
    }

    private void NavigateToCalendar()
    {
        _regionManager.RequestNavigate(RegionNames.MainContentRegion, "MainCalendarView");
    }

    private void NavigateToSmartDevices()
    {
        _regionManager.RequestNavigate(RegionNames.MainContentRegion, "SmartDevicesView");
    }

    private void NavigateToSecurity()
    {
        _regionManager.RequestNavigate(RegionNames.MainContentRegion, "SecurityView");
    }

    public void HideProfileNotification()
    {
        ProfileNotificationVisibility = Visibility.Collapsed;
    }

    private async void LogoutUser()
    {
        if (await _userAuthRepository.Logout())
        {
            _regionManager.RequestNavigate(RegionNames.MainWindowContentRegion, "LoginView");
        }
    }

    public void OnNavigatedTo(NavigationContext navigationContext)
    {

    }

    public bool IsNavigationTarget(NavigationContext navigationContext)
    {
        return true;
    }

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {
        
    }
}