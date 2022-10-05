using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using SmartAssistant.Services.Calendar;
using SmartAssistant.WPF.Core;
using SmartAssistant.WPF.Core.Events;
using System;

namespace SmartAssistant.WPF.Modules.Calendar.ViewModels;

public class MainCalendarViewModel : BindableBase, INavigationAware
{
    private DateTime _selectedDate;
    public DateTime SelectedDate
    {
        get { return _selectedDate; }
        set 
        { 
            SetProperty(ref _selectedDate, value);
            SelectedDateChanged();
        }
    }

    private readonly IRegionManager _regionManager;
    private readonly IEventAggregator _eventAggregator;
    private readonly ICalendarRepository _calendarRepository;

    public DelegateCommand GoToSelectScheduleViewCommand { get; private set; }

    public MainCalendarViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, ICalendarRepository calendarRepository)
    {
        GoToSelectScheduleViewCommand = new DelegateCommand(GoToSelectScheduleDate);
        _regionManager = regionManager;
        _eventAggregator = eventAggregator;
        _calendarRepository = calendarRepository;
    }

    private async void SelectedDateChanged()
    {
        await _calendarRepository.GetAllCalendarActivitiesAsync();
        var dayActivities = await _calendarRepository.GetCalendarActivitiesByDateAsync(SelectedDate);

        if (dayActivities == null)
        {
            GoToCalendarEmptyView();
            _eventAggregator.GetEvent<SelectedCalendarDateChangedEvent>().Publish(SelectedDate);
            return;
        }

        NavigationParameters param = new NavigationParameters();
        param.Add("day_activities", dayActivities);

        _regionManager.RequestNavigate(RegionNames.CalendarContent, "FilledCalendarView", param);
    }

    private void GoToSelectScheduleDate()
    {
        NavigationParameters param = new NavigationParameters();
        param.Add("activity", new CalendarActivityDTO() { Date = _selectedDate });

        _regionManager.RequestNavigate(RegionNames.MainContentRegion, "SelectScheduleDateView", param);
    }

    private void GoToCalendarEmptyView()
    {
        NavigationParameters param = new NavigationParameters();
        param.Add("activity", new CalendarActivityDTO() { Date = _selectedDate });

        _regionManager.RequestNavigate(RegionNames.CalendarContent, "CalendarEmptyView", param);
    }

    public void OnNavigatedTo(NavigationContext navigationContext)
    {
        SelectedDate = DateTime.Now;
    }

    public bool IsNavigationTarget(NavigationContext navigationContext)
    {
        return true;
    }

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {

    }
}