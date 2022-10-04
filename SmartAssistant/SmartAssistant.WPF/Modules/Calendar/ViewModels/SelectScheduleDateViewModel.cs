using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SmartAssistant.WPF.Core;
using System;

namespace SmartAssistant.WPF.Modules.Calendar.ViewModels;

public class SelectScheduleDateViewModel : BindableBase, INavigationAware
{
    private CalendarActivityDTO _activity;

    private DateTime _selectedDate;
    public DateTime SelectedDate
    {
        get { return _selectedDate; }
        set { SetProperty(ref _selectedDate, value); }
    }

    private readonly IRegionManager _regionManager;
    IRegionNavigationJournal _journal;

    public DelegateCommand NavigateBackCommand { get; private set; }
    public DelegateCommand ContinueToSelectActivityCommand { get; private set; }

    public SelectScheduleDateViewModel(IRegionManager regionManager)
    {
        _regionManager = regionManager;

        NavigateBackCommand = new DelegateCommand(NavigateBack);
        ContinueToSelectActivityCommand = new DelegateCommand(ContinueToSelectActivity);
    }

    private void ContinueToSelectActivity()
    {
        _activity.Date = _selectedDate;

        NavigationParameters param = new NavigationParameters();
        param.Add("activity", _activity);

        _regionManager.RequestNavigate(RegionNames.MainContentRegion, "SelectActivityView", param);
    }

    private void NavigateBack()
    {
        _regionManager.RequestNavigate(RegionNames.MainContentRegion, "MainCalendarView");
    }

    // If it returns false a new view and viewmodel are created
    // otherwise the existing ones are used
    public bool IsNavigationTarget(NavigationContext navigationContext)
    {
        return NavigationState.CreatesNewViewsOnAddActivityPages == false;
    }

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {
        
    }

    public void OnNavigatedTo(NavigationContext navigationContext)
    {
        _journal = navigationContext.NavigationService.Journal;

        if (navigationContext.Parameters.ContainsKey("activity"))
        {
            _activity = navigationContext.Parameters.GetValue<CalendarActivityDTO>("activity");
            SelectedDate = _activity.Date;
        }
    }
}