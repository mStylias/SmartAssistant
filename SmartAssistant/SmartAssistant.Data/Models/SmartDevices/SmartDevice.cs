namespace SmartAssistant.Data.Models.SmartDevices;

public abstract class SmartDevice
{
    public string Model { get; set; }
    public bool IsConnected { get; set; }
    public bool IsOn { get; set; }

    public SmartDevice(string model, bool isConnected = true, bool isOn = false)
    {
        Model = model;
        IsConnected = isConnected;
        IsOn = isOn;
    }
}