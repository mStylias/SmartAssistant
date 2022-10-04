using Prism.Commands;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using SmartAssistant.WPF.Core;
using System;
using System.Diagnostics;

namespace SmartAssistant.WPF.Modules.Calendar.ViewModels;

public class ActivityAddedSuccessfullyViewModel : BindableBase, INavigationAware
{
    private CalendarActivityDTO _activity;

    private readonly IRegionManager _regionManager;

    public DelegateCommand GoToCalendarMainCommand { get; private set; }
    public DelegateCommand GoToAddAnotherActivityCommand { get; private set; }

    public ActivityAddedSuccessfullyViewModel(IRegionManager regionManager)
    {
        GoToCalendarMainCommand = new DelegateCommand(GoToCalendarMain);
        GoToAddAnotherActivityCommand = new DelegateCommand(GoToAddAnotherActivity);
        _regionManager = regionManager;
    }

    private void GoToCalendarMain()
    {
        _regionManager.RequestNavigate(RegionNames.MainContentRegion, "MainCalendarView");
    }

    private void GoToAddAnotherActivity()
    {
        NavigationState.CreatesNewViewsOnAddActivityPages = false;
        NavigationParameters param = new NavigationParameters();
        param.Add("activity", _activity);
        _regionManager.RequestNavigate(RegionNames.MainContentRegion, "SelectActivityView", param);
    }

    public void OnNavigatedTo(NavigationContext navigationContext)
    {
        if (navigationContext.Parameters.ContainsKey("activity"))
        {
            _activity = navigationContext.Parameters.GetValue<CalendarActivityDTO>("activity");
            Debug.WriteLine($"Name: {_activity.Name}\nDate: {_activity.Date}");
        }

        NavigationState.CreatesNewViewsOnAddActivityPages = true;
    }

    public bool IsNavigationTarget(NavigationContext navigationContext)
    {
        return false;
    }

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {
        
    }
}