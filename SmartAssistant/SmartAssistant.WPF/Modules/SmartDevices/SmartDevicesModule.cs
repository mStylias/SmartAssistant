using Prism.Ioc;
using Prism.Modularity;
using SmartAssistant.WPF.Modules.SmartDevices.Views;

namespace SmartAssistant.WPF.Modules.SmartDevices;

public class SmartDevicesModule : IModule
{
    public void OnInitialized(IContainerProvider containerProvider)
    {
        
    }

    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<SmartDevicesView>();

        containerRegistry.RegisterForNavigation<LightsView>();
        containerRegistry.RegisterForNavigation<ThermostatView>();
        containerRegistry.RegisterForNavigation<AirConditionerView>();
        containerRegistry.RegisterForNavigation<SmartShoerackView>();
        containerRegistry.RegisterForNavigation<SmartFeederView>();
    }
}