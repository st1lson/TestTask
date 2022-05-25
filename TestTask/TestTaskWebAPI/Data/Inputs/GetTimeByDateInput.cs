using System;

namespace TestTaskWebAPI.Data.Inputs
{
    /// <summary>
    /// Represents the input for calculating a work time per day
    /// </summary>
    public record GetTimeByDateInput(string Id, DateTime Date);
}
