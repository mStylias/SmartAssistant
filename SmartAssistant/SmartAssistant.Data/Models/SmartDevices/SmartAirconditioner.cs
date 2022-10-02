namespace SmartAssistant.Data.Models.SmartDevices;

public class SmartAirconditioner : SmartDevice
{
    public Mode Mode { get; set; }
    public string Temperature { get; set; }

    public SmartAirconditioner(Mode mode, string temperature, string model, bool isConnected, bool isOn) 
        : base(model, isConnected, isOn)
    {
        Mode = mode;
        Temperature = temperature;
    }
}