using SmartAssistant.Data.Models.Calendar;

namespace SmartAssistant.Services.Calendar;

public interface ICalendarRepository
{
    Task<Dictionary<string, SortedSet<CalendarActivity>>> GetAllCalendarActivitiesAsync();
    Task<SortedSet<CalendarActivity>> GetCalendarActivitiesByDateAsync(DateTime date);
    Task<CalendarActivity> GetCalendarActivityAsync(DateTime dateTime);
    Task AddCalendarActivityAsync(CalendarActivity activity);
    Task<CalendarActivity> UpdateCalendarActivityAsync(CalendarActivity activity);
    Task DeleteCalendarActivityAsync(DateTime activityStartDateTime);
}