using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
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

    public DelegateCommand GoToSelectScheduleViewCommand { get; private set; }

    public MainCalendarViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
    {
        GoToSelectScheduleViewCommand = new DelegateCommand(GoToSelectScheduleDate);
        _regionManager = regionManager;
        _eventAggregator = eventAggregator;
    }

    private void SelectedDateChanged()
    {
        _eventAggregator.GetEvent<SelectedCalendarDateChangedEvent>().Publish(SelectedDate);
    }

    private void GoToSelectScheduleDate()
    {
        NavigationParameters param = new NavigationParameters();
        param.Add("activity", new CalendarActivityDTO() { Date = _selectedDate });

        _regionManager.RequestNavigate(RegionNames.MainContentRegion, "SelectScheduleDateView", param);
    }

    public void OnNavigatedTo(NavigationContext navigationContext)
    {
        SelectedDate = DateTime.Now;
        NavigationParameters param = new NavigationParameters();
        param.Add("activity", new CalendarActivityDTO() { Date = _selectedDate });

        _regionManager.RequestNavigate(RegionNames.CalendarContent, "CalendarEmptyView", param);
    }

    public bool IsNavigationTarget(NavigationContext navigationContext)
    {
        return true;
    }

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {

    }
}