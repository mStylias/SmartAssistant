using Prism.Commands;
using Prism.Regions;
using SmartAssistant.WPF.Core;
using System;

namespace SmartAssistant.WPF.Modules.MainRegion.ViewModels;

public class HelpWindowViewModel
{
	private readonly IRegionManager _regionManager;

	public DelegateCommand<string> ShowFeatureHelpCommand { get; private set; }

	public HelpWindowViewModel(IRegionManager regionManager)
	{
		ShowFeatureHelpCommand = new DelegateCommand<string>(ShowFeatureHelp);
		_regionManager = regionManager;
	}

	private void ShowFeatureHelp(string view)
	{
		switch (view)
		{
			case "Login":
                _regionManager.RequestNavigate(RegionNames.HelpWindowContentRegion, "LoginHelpView");
				return;
            case "Calendar":
				_regionManager.RequestNavigate(RegionNames.HelpWindowContentRegion, "CreateCalendarHelpView");
                return;
			case "ViewCalendar":
                _regionManager.RequestNavigate(RegionNames.HelpWindowContentRegion, "ViewCalendarHelpView");
                return;
			case "Lights":
                _regionManager.RequestNavigate(RegionNames.HelpWindowContentRegion, "LightsHelpView");
                return;
			case "Thermostat":
                _regionManager.RequestNavigate(RegionNames.HelpWindowContentRegion, "ThermostatHelpView");
                return;
			case "AirConditioner":
                _regionManager.RequestNavigate(RegionNames.HelpWindowContentRegion, "AirConditionerHelpView");
                return;
			case "SmartFeeder":
                _regionManager.RequestNavigate(RegionNames.HelpWindowContentRegion, "SmartFeederHelpView");
                return;
			case "SmartShoerack":
                _regionManager.RequestNavigate(RegionNames.HelpWindowContentRegion, "SmartShoerackHelpView");
                return;
			case "SecurityCameras":
                _regionManager.RequestNavigate(RegionNames.HelpWindowContentRegion, "SecurityCamerasHelpView");
                return;
        }
	}
}