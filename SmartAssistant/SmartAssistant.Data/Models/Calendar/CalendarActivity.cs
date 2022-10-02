using SmartAssistant.Data.CustomExceptions;
using System.Globalization;

namespace SmartAssistant.Data.Models.Calendar;

public partial class CalendarActivity : IComparable<CalendarActivity>
{
    public string Name { get; set; }
    public string StartingDayName { get => StartDateTime.ToString("dddd", CultureInfo.CreateSpecificCulture("en-US")); }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }

    private bool AreTimesValid(DateTime startDateTime, DateTime endDateTime)
    {
        return DateTime.Compare(startDateTime, endDateTime) < 0;
    }

    public CalendarActivity(string name, DateTime startDateTime, DateTime endDateTime)
    {
        if (AreTimesValid(startDateTime, endDateTime) == false)
            throw new InvalidDataException("Start time must always be lesser than end time in activities!");

        Name = name;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
    }

    /// <summary>
    /// Compare Start Time of this instance with another activity
    /// </summary>
    /// <param name="otherStartDateTime"></param>
    /// <returns></returns>
    public int CompareTo(CalendarActivity? otherActivity)
    {
        if (otherActivity == null) throw new ArgumentNullException("Other calendar activity was null while comparing");

        return DateTime.Compare(StartDateTime, otherActivity.StartDateTime);
    }
}