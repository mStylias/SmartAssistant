using SmartAssistant.Data.Models.SmartDevices;

namespace SmartAssistant.Data.Models.Rooms;

public class Room
{
    public List<SmartDevice> SmartDevices { get; set; }
    public RoomType Type { get; set; }

    public Room(List<SmartDevice> smartDevices, RoomType type)
    {
        SmartDevices = smartDevices;
        Type = type;
    }
}