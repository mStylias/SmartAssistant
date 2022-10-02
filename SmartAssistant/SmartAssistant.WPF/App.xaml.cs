using Prism.Ioc;
using Prism.Modularity;
using SmartAssistant.Services.Calendar;
using SmartAssistant.Services.UserAccount;
using SmartAssistant.WPF.Modules.Login;
using SmartAssistant.WPF.Modules.MainRegion;
using SmartAssistant.WPF.Views;
using Wpf.Ui.Controls;

namespace SmartAssistant.WPF;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    protected override UiWindow CreateShell()
    {
        return Container.Resolve<MainWindow>();
    }

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.Register<IUserAuthRepository, UserAuthRepository>();
        containerRegistry.Register<ICalendarRepository, CalendarRepository>();
    }

    protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
    {
        moduleCatalog.AddModule<MainRegionModule>();
        moduleCatalog.AddModule<LoginModule>();
        base.ConfigureModuleCatalog(moduleCatalog);
    }
}
