using Prism.Ioc;
using Prism.Modularity;
using SmartAssistant.WPF.Modules.Calendar.Views;

namespace SmartAssistant.WPF.Modules.Calendar;

public class CalendarModule : IModule
{
    public void OnInitialized(IContainerProvider containerProvider)
    {
        
    }

    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<CalendarEmptyView>();
        containerRegistry.RegisterForNavigation<SelectScheduleDateView>();
        containerRegistry.RegisterForNavigation<SelectActivityView>();
        containerRegistry.RegisterForNavigation<SelectActivityTimeView>();
    }
}