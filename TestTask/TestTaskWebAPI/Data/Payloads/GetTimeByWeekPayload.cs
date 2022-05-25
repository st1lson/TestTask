using System;

namespace TestTaskWebAPI.Data.Payloads
{
    public record GetTimeByWeekPayload(int WeekNumber, TimeSpan WorkTime);
}
