namespace SmartAssistant.Data.Models.SmartDevices;

public class SmartLamp : SmartDevice
{
    public string Brightness { get; set; }
    public string Hue { get; set; }

    public SmartLamp(string model, string brightness, string hue, bool isConnected = true, bool isOn = false) 
            : base(model, isConnected, isOn)
    {
        Brightness = brightness;
        Hue = hue;
    }
}