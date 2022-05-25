using System;

namespace TestTaskWebAPI.Data.Payloads
{
    public record GetTimeByDatePayload(DateTime Date, TimeSpan WorkTime);
}
