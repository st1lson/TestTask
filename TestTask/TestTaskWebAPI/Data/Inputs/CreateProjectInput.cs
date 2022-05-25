using System;

namespace TestTaskWebAPI.Data.Inputs
{
    /// <summary>
    /// Represents the input for creating a project
    /// </summary>
    public record CreateProjectInput(string Name, DateTime DateStart, DateTime DateEnd);
}
