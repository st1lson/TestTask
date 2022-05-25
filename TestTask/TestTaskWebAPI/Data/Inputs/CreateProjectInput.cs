using System;

namespace TestTaskWebAPI.Data.Inputs
{
    public record CreateProjectInput(string Name, DateTime DateStart, DateTime DateEnd);
}
