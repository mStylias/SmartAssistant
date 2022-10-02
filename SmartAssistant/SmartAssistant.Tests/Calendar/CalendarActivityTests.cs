using SmartAssistant.Data.CustomExceptions;
using SmartAssistant.Data.Models.Calendar;
using Xunit.Abstractions;

namespace SmartAssistant.Tests.Calendar;

public class CalendarActivityTests
{
    private readonly ITestOutputHelper _output;

    public CalendarActivityTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void CreateActivitiesSortedSet_ShouldCreateActivities()
    {
        CalendarActivity activity1 = new CalendarActivity("Sex", new DateTime(2022, 9, 21, 7, 10, 0), new DateTime(2022, 9, 21, 8, 10, 0));
        CalendarActivity activity2 = new CalendarActivity("Bar", new DateTime(2022, 9, 22, 8, 0, 0), new DateTime(2022, 9, 22, 10, 0, 0));
        CalendarActivity activity3 = new CalendarActivity("IntSession", new DateTime(2022, 9, 21, 10, 0, 0), new DateTime(2022, 9, 21, 11, 0, 0));

        var calendarData = new CalendarData();
        calendarData.AddCalendarActivity(activity1);
        calendarData.AddCalendarActivity(activity2);
        calendarData.AddCalendarActivity(activity3);

        Assert.Equal(2, calendarData.ActivitiesByDate?.Count);
        Assert.Equal("Sex", calendarData.GetActivityByDateTime(activity1.StartDateTime)?.Name);
        Assert.Equal("IntSession", calendarData.GetActivityByDateTime(activity3.StartDateTime)?.Name);
        Assert.Equal("Bar", calendarData.GetActivityByDateTime(activity2.StartDateTime)?.Name);
    }

    [Theory]
    [InlineData(13, 40, 15, 40,
                14, 40, 16, 40)]

    [InlineData(14, 40, 16, 40,
                13, 40, 15, 40)]

    [InlineData(14, 40, 15, 40,
                13, 40, 16, 40)]

    [InlineData(13, 40, 16, 40,
                14, 40, 15, 40)]
    public void CreateActivitiesWithOverlappingDateTimes_ShouldThrow(int startTime1Hours, int startTime1Minutes, int endTime1Hours, int endTime1Minutes,
                                                                     int startTime2Hours, int startTime2Minutes, int endTime2Hours, int endTime2Minutes)
    {
        DateTime startTime1 = new DateTime(2022, 9, 21, startTime1Hours, startTime1Minutes, 0);
        DateTime endTime1 = new DateTime(2022, 9, 21, endTime1Hours, endTime1Minutes, 0);

        DateTime startTime2 = new DateTime(2022, 9, 21, startTime2Hours, startTime2Minutes, 0);
        DateTime endTime2 = new DateTime(2022, 9, 21, endTime2Hours, endTime2Minutes, 0);

        var activity1 = new CalendarActivity("Sex", startTime1, endTime1);
        var activity2 = new CalendarActivity("Bar", new DateTime(2022, 9, 22, 5, 5, 5), new DateTime(2022, 9, 22, 5, 10, 15));
        var activity3 = new CalendarActivity("Int", startTime2, endTime2);

        var calendarData = new CalendarData();

        calendarData.AddCalendarActivity(activity1);
        calendarData.AddCalendarActivity(activity2);
        Assert.Throws<TimeOverlapException>(() =>
        {
            calendarData.AddCalendarActivity(activity3);
        });
    }

    [Theory]
    [InlineData(3, 34, 0,
                3, 34, 0)]
    [InlineData(3, 50, 0,
                3, 0, 0)]
    public void CreateActivityWithWrongTimes_ShouldThrow(int startTimeHours, int startTimeMinutes, int startTimeSeconds,
                                                         int endTimeHours, int endTimeMinutes, int endTimeSeconds)
    {
        DateTime startTime = new DateTime(2022, 9, 21, startTimeHours, startTimeMinutes, startTimeSeconds);
        DateTime endTime = new DateTime(2022, 9, 21, endTimeHours, endTimeMinutes, endTimeSeconds);

        Assert.Throws<InvalidDataException>(() =>
        {
            var activity = new CalendarActivity("Sex", startTime, endTime);
        });
    }
}