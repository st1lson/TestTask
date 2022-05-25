using System;
using System.Linq;
using TestTaskData.DbContexts;
using TestTaskData.Models;

namespace TestTaskData.Repositories
{
    public sealed class EmployeeRepository : GenericRepository<Employee>
    {
        public EmployeeRepository(AppDbContext context) : base(context)
        {

        }

        public TimeSpan GetTimeTracking(string id, DateTime date)
        {
            IQueryable<WorkTime> workTimes = Context.WorkTimes.Where(w => w.EmployeeId == id && w.Day == date);

            return Sum(workTimes);
        }

        public TimeSpan GetTimeTracking(string id, DateTime startDate, DateTime endDate)
        {
            IQueryable<WorkTime> workTimes = Context.WorkTimes.Where(w => w.EmployeeId == id && w.Day >= startDate && w.Day <= endDate);

            return Sum(workTimes);
        }

        private static TimeSpan Sum(IQueryable<WorkTime> workTimes)
        {
            TimeSpan result = TimeSpan.Zero;
            foreach (var item in workTimes)
            {
                result += item.Time;
            }

            return result;
        }
    }
}
