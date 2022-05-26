using System;
using TestTaskData.Models;

namespace TestTaskData.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        TimeSpan GetTimeTracking(string id, DateTime date);

        TimeSpan GetTimeTracking(string id, DateTime startDate, DateTime endDate);
    }
}
