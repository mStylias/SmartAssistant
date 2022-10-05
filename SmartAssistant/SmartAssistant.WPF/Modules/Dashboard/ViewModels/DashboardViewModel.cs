using Prism.Mvvm;
using Prism.Regions;

namespace SmartAssistant.WPF.Modules.Dashboard.ViewModels;

public class DashboardViewModel : BindableBase, INavigationAware
{
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