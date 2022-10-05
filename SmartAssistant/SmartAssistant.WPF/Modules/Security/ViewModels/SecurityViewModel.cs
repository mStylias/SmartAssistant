using Prism.Mvvm;
using Prism.Regions;

namespace SmartAssistant.WPF.Modules.Security.ViewModels;

public class SecurityViewModel : BindableBase, INavigationAware
{
    public bool IsNavigationTarget(NavigationContext navigationContext)
    {
        return true;
    }

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {
        
    }

    public void OnNavigatedTo(NavigationContext navigationContext)
    {
        
    }
}