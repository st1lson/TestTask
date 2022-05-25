using System;

namespace TestTaskWebAPI.Data.Payloads
{
    /// <summary>
    /// Represents the payload for calculating a work time per day
    /// </summary>
    public record GetTimeByDatePayload(DateTime Date, TimeSpan WorkTime);
}
