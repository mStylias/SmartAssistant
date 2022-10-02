using SmartAssistant.Data.Models.Calendar;

namespace SmartAssistant.Data.Models.SmartDevices;

public class SmartShoerack
{
    List<Shoe> AllShoes { get; set; }
    /// <summary>
    /// Links calendar activities with appropriate shoes 
    /// </summary>
    Dictionary<CalendarActivity, Shoe> ShoesByCalendarActivities { get; set; }

    public SmartShoerack()
    {
        AllShoes = new List<Shoe>();
        ShoesByCalendarActivities = new Dictionary<CalendarActivity, Shoe>();
    }
}