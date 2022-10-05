using SmartAssistant.Data.CustomExceptions;

namespace SmartAssistant.Data.Models.Calendar;

public class CalendarData
{
    /// <summary>
    /// Contains sorted sets of activities organized by dates
    /// </summary>
    public Dictionary<string, SortedSet<CalendarActivity>> ActivitiesByDate { get; set; }

    public CalendarData()
    {
        ActivitiesByDate = new Dictionary<string, SortedSet<CalendarActivity>>();
    }

    private bool AreActivitiesOverlapping(CalendarActivity activity1, CalendarActivity activity2)
    {
        if (DateTime.Compare(activity2.StartDateTime, activity1.EndDateTime) < 0 &&
            DateTime.Compare(activity1.StartDateTime, activity2.EndDateTime) < 0)
        {
            return true;
        }

        return false;
    }

    public void AddCalendarActivity(CalendarActivity activity)
    {
        var date = activity.StartDateTime.ToShortDateString();

        if (ActivitiesByDate.ContainsKey(date) == false)
        {
            ActivitiesByDate[date] = new SortedSet<CalendarActivity>();
        }

        foreach (var storedActivity in ActivitiesByDate[date])
        {
            if (AreActivitiesOverlapping(activity, storedActivity))
                throw new TimeOverlapException("Activities cannot overlap!");
        }

        ActivitiesByDate[date].Add(activity);
    }

    /// <summary>
    /// Gets all the activities in the day that the given activity starts at
    /// </summary>
    /// <param name="dateTime"> The date of the day </param>
    /// <returns></returns>
    public SortedSet<CalendarActivity> GetAllDayActivities(DateTime dateTime)
    {
        var date = dateTime.ToShortDateString();
        if (ActivitiesByDate.ContainsKey(date))
        {
            return ActivitiesByDate[date];
        }

        return null;
    }

    /// <summary>
    /// Gets the activity that starts at the given DateTime if it exists
    /// </summary>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    public CalendarActivity GetActivityByDateTime(DateTime dateTime)
    {
        var activity = ActivitiesByDate[dateTime.ToShortDateString()]
            .FirstOrDefault(x => DateTime.Compare(x.StartDateTime, dateTime) == 0);

        return activity;
    }

    /// <summary>
    /// Removes the calendar activity with the given Datetime from the dictionary
    /// </summary>
    /// <param name="activityStartDateTime"></param>
    public void DeleteCalendarActivity(DateTime activityStartDateTime)
    {
        var activity = GetActivityByDateTime(activityStartDateTime);

        if (activity != null)
        {
            var date = activityStartDateTime.ToShortDateString();
            var activities = ActivitiesByDate[date];
            activities.Remove(activity);

            if (activities.Any() == false)
            {
                ActivitiesByDate.Remove(date);
            }
        }
    }
}
