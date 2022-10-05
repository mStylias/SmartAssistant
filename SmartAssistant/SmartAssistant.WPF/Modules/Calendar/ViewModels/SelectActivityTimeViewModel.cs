using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SmartAssistant.Data.CustomExceptions;
using SmartAssistant.Data.Models.Calendar;
using SmartAssistant.Services.Calendar;
using SmartAssistant.WPF.Core;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace SmartAssistant.WPF.Modules.Calendar.ViewModels;

public class SelectActivityTimeViewModel : BindableBase, INavigationAware
{
    CalendarActivityDTO _activity;

    #region Observable Properties
    private DateTime? _selectedStartTime;
    public DateTime? SelectedStartTime
    {
        get { return _selectedStartTime; }
        set 
        { 
            SetProperty(ref _selectedStartTime, value);
            AddActivityCommand.RaiseCanExecuteChanged();
        }
    }

    private DateTime? _selectedEndTime;
    public DateTime? SelectedEndTime
    {
        get { return _selectedEndTime; }
        set 
        { 
            SetProperty(ref _selectedEndTime, value);
            AddActivityCommand.RaiseCanExecuteChanged();
        }
    }

    private bool _isActivityOutside = true;
    public bool IsActivityOutside
    {
        get { return _isActivityOutside; }
        set { SetProperty(ref _isActivityOutside, value); }
    }

    private string _tranportationMethod;
    public string TransportationMethod
    {
        get { return _tranportationMethod; }
        set { SetProperty(ref _tranportationMethod, value); }
    }

    private bool _showInvalidTimeError = false;
    public bool ShowInvalidTimeError
    {
        get { return _showInvalidTimeError; }
        set { SetProperty(ref _showInvalidTimeError, value); }
    }

    private bool _showTimeOverlapError = false;
    public bool ShowTimeOverlapError
    {
        get { return _showTimeOverlapError; }
        set { SetProperty(ref _showTimeOverlapError, value); }
    }

    private bool _isActivityCustom = false;
    public bool IsActivityCustom
    {
        get { return _isActivityCustom; }
        set 
        { 
            SetProperty(ref _isActivityCustom, value);
            if (value) { CustomActivityNameVisibility = Visibility.Visible; }
            else { CustomActivityNameVisibility = Visibility.Collapsed; }
        }
    }

    private string _customActivityName = "";
    public string CustomActivityName
    {
        get { return _customActivityName; }
        set { SetProperty(ref _customActivityName, value); }
    }

    private Visibility _customActivityNameVisibility = Visibility.Collapsed;
    public Visibility CustomActivityNameVisibility
    {
        get { return _customActivityNameVisibility; }
        set { SetProperty(ref _customActivityNameVisibility, value); }
    }
    #endregion

    private readonly ICalendarRepository _calendarRepository;
    private readonly IRegionManager _regionManager;
    private IRegionNavigationJournal _journal;

    public DelegateCommand NavigateBackCommand { get; private set; }
    public DelegateCommand AddActivityCommand { get; private set; }

    public SelectActivityTimeViewModel(IRegionManager regionManager, ICalendarRepository calendarRepository)
    {
        _regionManager = regionManager;
        _calendarRepository = calendarRepository;
        NavigateBackCommand = new DelegateCommand(NavigateBack);
        AddActivityCommand = new DelegateCommand(AddActivity, CanAddActivity);
    }

    private async void AddActivity()
    {
        if (CanAddActivity() == false)
            return;

        if (IsActivityCustom)
        {
            _activity.Name = _customActivityName;
        }

        _activity.StartTime = _selectedStartTime.Value;
        _activity.EndTime = _selectedEndTime.Value;

        try
        {
            CalendarActivity calendarActivity = new CalendarActivity
            (
                _activity.Name,
                new DateTime(_activity.Date.Year, _activity.Date.Month, _activity.Date.Day,
                                _activity.StartTime.Hour, _activity.StartTime.Minute, _activity.StartTime.Second),
                new DateTime(_activity.Date.Year, _activity.Date.Month, _activity.Date.Day,
                                _activity.EndTime.Hour, _activity.EndTime.Minute, _activity.EndTime.Second),
                _isActivityOutside,
                _tranportationMethod
            );
      
            await _calendarRepository.AddCalendarActivityAsync(calendarActivity);

            NavigationParameters param = new NavigationParameters();
            param.Add("activity", _activity);
            _regionManager.RequestNavigate(RegionNames.MainContentRegion, "ActivityAddedSuccessfullyView", param);
        }
        catch (InvalidDataException)
        {
            ShowInvalidTimeError = true;
            ShowTimeOverlapError = false;
        }
        catch (TimeOverlapException)
        {
            ShowInvalidTimeError = false;
            ShowTimeOverlapError = true;
        }
    }

    private bool CanAddActivity()
    {
        if (IsActivityCustom && string.IsNullOrEmpty(CustomActivityName))
        {
            return false;
        }

        if (_selectedStartTime.HasValue && _selectedEndTime.HasValue == false)
        {
            return false;
        }

        return true;
    }

    private void NavigateBack()
    {
        _journal.GoBack();
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
        _journal = navigationContext.NavigationService.Journal;

        if (navigationContext.Parameters.ContainsKey("activity"))
        {
            _activity = navigationContext.Parameters.GetValue<CalendarActivityDTO>("activity");
            
            if (_activity.Name == "Custom")
            {
                IsActivityCustom = true;
            }
        }
    }
}