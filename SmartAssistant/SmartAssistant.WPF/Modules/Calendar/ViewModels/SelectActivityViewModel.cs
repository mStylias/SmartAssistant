using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SmartAssistant.WPF.Core;

namespace SmartAssistant.WPF.Modules.Calendar.ViewModels;

public class SelectActivityViewModel : BindableBase, INavigationAware
{
    CalendarActivityDTO _activity;

    private string _selectedActivity = "";
    private readonly IRegionManager _regionManager;
    private IRegionNavigationJournal _journal;

    public string SelectedActivity
    {
        get { return _selectedActivity; }
        set 
        { 
            SetProperty(ref _selectedActivity, value); 
            ContinueToSelectTimeCommand.RaiseCanExecuteChanged();
        }
    }

    public DelegateCommand<string> SelectActivityCommand { get; private set; }
    public DelegateCommand NavigateBackCommand { get; private set; }
    public DelegateCommand ContinueToSelectTimeCommand { get; private set; }

    public SelectActivityViewModel(IRegionManager regionManager)
    {
        SelectActivityCommand = new DelegateCommand<string>(SelectActivity);
        NavigateBackCommand = new DelegateCommand(NavigateBack);
        ContinueToSelectTimeCommand = new DelegateCommand(ContinueToSelectTime, CanExecuteContinueToSelectTime);
        _regionManager = regionManager;
    }

    private void SelectActivity(string activityName)
    {
        SelectedActivity = activityName;
    }

    private void ContinueToSelectTime()
    {
        _activity.Name = SelectedActivity;

        NavigationParameters param = new NavigationParameters();
        param.Add("activity", _activity);

        _regionManager.RequestNavigate(RegionNames.MainContentRegion, "SelectActivityTimeView", param);
    }

    private bool CanExecuteContinueToSelectTime()
    {
        return string.IsNullOrEmpty(_selectedActivity) == false;
    }

    private void NavigateBack()
    {
        _journal.GoBack();
    }

    public bool IsNavigationTarget(NavigationContext navigationContext)
    {
        return true;
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
        }
    }
}