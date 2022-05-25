using System;

namespace TestTaskWebAPI.Data.Inputs
{
    public record UpdateProjectInput(string Id, string Name, DateTime DateStart, DateTime DateEnd);
}
