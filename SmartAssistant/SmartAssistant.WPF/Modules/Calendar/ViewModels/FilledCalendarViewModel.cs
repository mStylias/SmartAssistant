using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SmartAssistant.Data.Models.Calendar;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace SmartAssistant.WPF.Modules.Calendar.ViewModels;

public class FilledCalendarViewModel : BindableBase, INavigationAware
{
    private Visibility _successfullyOrderedCoffeeVisibility = Visibility.Collapsed;
    public Visibility SuccessfullyOrderedCoffeeVisibility
    {
        get { return _successfullyOrderedCoffeeVisibility; }
        set { SetProperty(ref _successfullyOrderedCoffeeVisibility, value); }
    }

    private Visibility _carQuestionsVisibility = Visibility.Collapsed;
	public Visibility CarQuestionsVisibility
    {
		get { return _carQuestionsVisibility; }
		set { SetProperty(ref _carQuestionsVisibility, value); }
	}

	private ObservableCollection<CalendarActivity> _dayActivities;
	public ObservableCollection<CalendarActivity> DayActivities
	{
		get { return _dayActivities; }
		set { SetProperty(ref _dayActivities, value); }
	}

	public DelegateCommand BuyCoffeeCommand { get; private set; }


    public FilledCalendarViewModel()
	{
		BuyCoffeeCommand = new DelegateCommand(ShowSuccessMessage);

    }

	private void ShowSuccessMessage()
	{
		SuccessfullyOrderedCoffeeVisibility = Visibility.Visible;

    }

	public bool IsNavigationTarget(NavigationContext navigationContext)
	{
		return false;
	}

	public void OnNavigatedFrom(NavigationContext navigationContext)
	{
		
	}

	public async void OnNavigatedTo(NavigationContext navigationContext)
	{
		if (navigationContext.Parameters.ContainsKey("day_activities"))
		{
			var dayActivities = navigationContext.Parameters.GetValue<SortedSet<CalendarActivity>>("day_activities");
            DayActivities = new ObservableCollection<CalendarActivity>(dayActivities);
			CheckForActivitiesWithCar(dayActivities);
		}
	}

	private void CheckForActivitiesWithCar(SortedSet<CalendarActivity> activities)
	{
		if (activities.Any(a => a.TransportationMethod == "Car"))
		{
			CarQuestionsVisibility = Visibility.Visible;
		}
	}
}