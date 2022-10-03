using Prism.Ioc;
using Prism.Modularity;
using SmartAssistant.Services.Calendar;
using SmartAssistant.Services.UserAccount;
using SmartAssistant.WPF.Modules.Calendar;
using SmartAssistant.WPF.Modules.Dashboard;
using SmartAssistant.WPF.Modules.Login;
using SmartAssistant.WPF.Modules.MainRegion;
using SmartAssistant.WPF.Modules.Security;
using SmartAssistant.WPF.Modules.SmartDevices;
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

        moduleCatalog.AddModule<DashboardModule>();
        moduleCatalog.AddModule<CalendarModule>();
        moduleCatalog.AddModule<SmartDevicesModule>();
        moduleCatalog.AddModule<SecurityModule>();

        base.ConfigureModuleCatalog(moduleCatalog);
    }
}
