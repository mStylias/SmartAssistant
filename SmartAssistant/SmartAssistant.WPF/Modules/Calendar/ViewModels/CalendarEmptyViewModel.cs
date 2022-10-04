using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SmartAssistant.WPF.Core;

namespace SmartAssistant.WPF.Modules.Calendar.ViewModels;

public class CalendarEmptyViewModel : BindableBase
{
	private readonly IRegionManager _regionManager;

	public DelegateCommand NavigateToSelectScheduleDateCommand { get; private set; }

	public CalendarEmptyViewModel(IRegionManager regionManager)
	{
        NavigateToSelectScheduleDateCommand = new DelegateCommand(NavigateToSelectScheduleDate);
		_regionManager = regionManager;
	}

	private void NavigateToSelectScheduleDate()
	{
		NavigationParameters param = new NavigationParameters();
		param.Add("activity", new CalendarActivityDTO());

		_regionManager.RequestNavigate(RegionNames.MainContentRegion, "SelectScheduleDateView", param);
    }
}