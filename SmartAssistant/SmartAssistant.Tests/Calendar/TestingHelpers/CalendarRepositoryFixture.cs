using SmartAssistant.Data.Models.Calendar;
using SmartAssistant.Services;
using SmartAssistant.Services.Calendar;

namespace SmartAssistant.Tests.Calendar.TestingHelpers;

public class CalendarRepositoryFixture
{
    public ICalendarRepository CalendarRepository { get; set; }
    public CalendarRepositoryFixture()
    {
        var repository = new CalendarRepository();
        Serializer.SaveDirectory = "SerializationTest";

        var activity = new CalendarActivity("Work", new DateTime(2022, 8, 10, 8, 30, 0), new DateTime(2022, 8, 10, 10, 30, 0));
        repository.AddCalendarActivityAsync(activity).Wait();

        CalendarRepository = repository;
    }
}