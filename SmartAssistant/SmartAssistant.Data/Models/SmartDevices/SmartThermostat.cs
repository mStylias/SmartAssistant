namespace SmartAssistant.Data.Models.SmartDevices;

public class SmartThermostat : SmartDevice
{
    /// <summary>
    /// Temperature in Celcius
    /// </summary>
    public double Temperature { get; set; }

    public SmartThermostat(string model, double temperature, bool isConnected = true, bool isOn = false) : base(model, isConnected, isOn)
    {
        Temperature = temperature;
    }
}