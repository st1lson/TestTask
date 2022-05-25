using System;

namespace TestTaskWebAPI.Data.Payloads
{
    /// <summary>
    /// Represents the payload for calculating a work time per week
    /// </summary>
    public record GetTimeByWeekPayload(int WeekNumber, TimeSpan WorkTime);
}
