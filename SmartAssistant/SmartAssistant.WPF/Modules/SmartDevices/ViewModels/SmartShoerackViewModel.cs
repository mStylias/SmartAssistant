using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SmartAssistant.WPF.Core;
using System.Diagnostics;

namespace SmartAssistant.WPF.Modules.SmartDevices.ViewModels;

public class SmartShoerackViewModel : BindableBase, INavigationAware
{
    private readonly IRegionManager _regionManager;
    private const string SHOES_STORE_URL = "https://www.skroutz.gr/search?keyphrase=%CF%80%CE%B1%CF%80%CE%BF%CF%85%CF%84%CF%83%CE%B9%CE%B1";

    public DelegateCommand GoToSmartDevicesMenuCommand { get; private set; }
    public DelegateCommand GoToBuyShoesWebsiteCommand { get; private set; }

    public SmartShoerackViewModel(IRegionManager regionManager)
    {
        _regionManager = regionManager;
        GoToSmartDevicesMenuCommand = new DelegateCommand(GoToSmartDevicesMenu);
        GoToBuyShoesWebsiteCommand = new DelegateCommand(GoToBuyShoesWebsite);
    }

    private void GoToBuyShoesWebsite()
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = SHOES_STORE_URL,
            UseShellExecute = true
        });
    }

    private void GoToSmartDevicesMenu()
    {
        _regionManager.RequestNavigate(RegionNames.MainContentRegion, "SmartDevicesView");
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

    }
}