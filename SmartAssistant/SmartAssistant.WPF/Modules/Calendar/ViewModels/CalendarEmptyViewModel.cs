using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using SmartAssistant.WPF.Core;
using SmartAssistant.WPF.Core.Events;
using System;
using System.Diagnostics;

namespace SmartAssistant.WPF.Modules.Calendar.ViewModels;

public class CalendarEmptyViewModel : BindableBase, INavigationAware
{
    private CalendarActivityDTO _activity;

    private readonly IRegionManager _regionManager;
    private readonly IEventAggregator _eventAggregator;

    public DelegateCommand GoToSelectScheduleDateCommand { get; private set; }

    public CalendarEmptyViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
    {
        GoToSelectScheduleDateCommand = new DelegateCommand(GoToSelectScheduleDate);
        _regionManager = regionManager;
        _eventAggregator = eventAggregator;
        _eventAggregator.GetEvent<SelectedCalendarDateChangedEvent>().Subscribe(OnSelectedDateChanged);
    }

    private void OnSelectedDateChanged(DateTime newDate)
    {
        _activity.Date = newDate;
    }

    private void GoToSelectScheduleDate()
    {
        NavigationParameters param = new NavigationParameters();
        param.Add("activity", _activity);

        _regionManager.RequestNavigate(RegionNames.MainContentRegion, "SelectScheduleDateView", param);
    }

    public void OnNavigatedTo(NavigationContext navigationContext)
    {
        if (navigationContext.Parameters.ContainsKey("activity"))
        {
            _activity = navigationContext.Parameters.GetValue<CalendarActivityDTO>("activity");
        }
    }

    public bool IsNavigationTarget(NavigationContext navigationContext)
    {
        return false;
    }

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {

    }
}