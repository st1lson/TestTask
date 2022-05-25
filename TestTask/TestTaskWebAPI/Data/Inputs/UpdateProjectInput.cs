using System;

namespace TestTaskWebAPI.Data.Inputs
{
    /// <summary>
    /// Represents the input for updating a project
    /// </summary>
    public record UpdateProjectInput(string Id, string Name, DateTime DateStart, DateTime DateEnd);
}
