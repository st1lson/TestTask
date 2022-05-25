namespace TestTaskWebAPI.Data.Inputs
{
    /// <summary>
    /// Represents the input for calculating a work time per week
    /// </summary>
    public record GetTimeByWeekInput(string Id, int WeekNumber);
}
