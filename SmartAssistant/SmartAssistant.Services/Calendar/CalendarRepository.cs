using SmartAssistant.Data.Models.Calendar;

namespace SmartAssistant.Services.Calendar;

public class CalendarRepository : ICalendarRepository
{
    public string CalendarActivitiesFileName = "CalendarActivities.json";
    public CalendarData _calendarData = new CalendarData();

    public async Task<Dictionary<string, SortedSet<CalendarActivity>>> GetAllCalendarActivitiesAsync()
    {
        var calendarActivities = await Serializer.DeserializeJsonFile<Dictionary<string, SortedSet<CalendarActivity>>>
                        (CalendarActivitiesFileName);

        if (calendarActivities == null)
        {
            calendarActivities = new Dictionary<string, SortedSet<CalendarActivity>>();
        }

        _calendarData.ActivitiesByDate = calendarActivities;
        return calendarActivities;
    }

    public async Task<SortedSet<CalendarActivity>> GetCalendarActivitiesByDateAsync(DateTime date)
    {
        if (_calendarData.ActivitiesByDate.Any() == false)
        {
            _calendarData.ActivitiesByDate = await GetAllCalendarActivitiesAsync();
        }

        return _calendarData.GetAllDayActivities(date);
    }

    public async Task<CalendarActivity> GetCalendarActivityAsync(DateTime dateTime)
    {
        if (_calendarData.ActivitiesByDate.Any() == false)
        {
            _calendarData.ActivitiesByDate = await GetAllCalendarActivitiesAsync();
        }

        return _calendarData.GetActivityByDateTime(dateTime);
    }

    public async Task AddCalendarActivityAsync(CalendarActivity activity)
    {
        if (_calendarData.ActivitiesByDate.Any() == false)
        {
            _calendarData.ActivitiesByDate = await GetAllCalendarActivitiesAsync();
        }

        _calendarData.AddCalendarActivity(activity);
        await Serializer.SaveJsonToFile(CalendarActivitiesFileName, _calendarData.ActivitiesByDate);
    }

    public async Task<CalendarActivity> UpdateCalendarActivityAsync(CalendarActivity activity)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteCalendarActivityAsync(DateTime activityStartDateTime)
    {
        if (_calendarData.ActivitiesByDate.Any() == false)
        {
            _calendarData.ActivitiesByDate = await GetAllCalendarActivitiesAsync();
        }

        _calendarData.DeleteCalendarActivity(activityStartDateTime);
        await Serializer.SaveJsonToFile(CalendarActivitiesFileName, _calendarData.ActivitiesByDate);
    }
}