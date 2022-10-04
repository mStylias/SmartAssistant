using System.Globalization;
using System;

namespace SmartAssistant.WPF.Modules.Calendar;

public class CalendarActivityDTO
{
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}