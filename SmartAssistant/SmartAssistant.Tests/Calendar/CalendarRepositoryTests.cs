using SmartAssistant.Data.Models.Calendar;
using SmartAssistant.Services.Calendar;
using SmartAssistant.Tests.Calendar.TestingHelpers;
using Xunit.Abstractions;

namespace SmartAssistant.Tests.Calendar;

[Collection("Calendar Repository Instance")]
public class CalendarRepositoryTests
{
    private readonly ITestOutputHelper _output;
    private readonly ICalendarRepository _calendarRepository;
    public CalendarRepositoryTests(ITestOutputHelper output, CalendarRepositoryFixture calendarFixture)
    {
        _output = output;
        _calendarRepository = calendarFixture.CalendarRepository;
    }

    [Fact]
    public async void GetAllCalendarActivitiesAsync_ShouldGetActivitiesDictionary()
    {
        var activitiesByDates = await _calendarRepository.GetAllCalendarActivitiesAsync();

        Assert.True(activitiesByDates.Any());
        _output.WriteLine(activitiesByDates.ElementAt(0).Value.ElementAt(0).Name);
    }

    [Fact]
    public async void GetCalendarActivitiesByDateAsync_ShouldGetShortedSetOfActivities()
    {
        var activities = await _calendarRepository.GetCalendarActivitiesByDateAsync(new DateTime(2022, 8, 10));

        Assert.NotEmpty(activities);
        _output.WriteLine(activities.ElementAt(0).Name);
    }

    [Fact]
    public async void GetCalendarActivityAsync_ShouldGetActivityByDate()
    {
        var activity = await _calendarRepository.GetCalendarActivityAsync(new DateTime(2022, 8, 10, 8, 30, 0));
        Assert.Equal("Work", activity.Name);
    }

    [Fact]
    public async void AddAndDeleteCalendarActivityAsync_ShouldAddAndThenDeleteActivity()
    {
        var activity = new CalendarActivity("Sleep", new DateTime(2022, 10, 12, 8, 30, 0), new DateTime(2022, 10, 12, 10, 30, 0));
        await _calendarRepository.AddCalendarActivityAsync(activity);

        Assert.NotEmpty(Directory.GetFiles("SerializationTest"));

        await _calendarRepository.DeleteCalendarActivityAsync(activity.StartDateTime);
    }
}