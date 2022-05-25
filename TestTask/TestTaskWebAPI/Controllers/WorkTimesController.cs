using Microsoft.AspNetCore.Mvc;
using System;
using TestTaskData.Repositories;
using TestTaskServices;
using TestTaskWebAPI.Data.Inputs;
using TestTaskWebAPI.Data.Payloads;

namespace TestTaskWebAPI.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class WorkTimesController : Controller
    {
        private readonly EmployeeRepository _employees;
        private readonly DateProcessor _dateProcessor;

        public WorkTimesController(EmployeeRepository employees, DateProcessor dateProcessor)
        {
            _employees = employees;
            _dateProcessor = dateProcessor;
        }

        [HttpPost]
        [Route("getByDate")]
        public IActionResult GetTimeByDate(GetTimeByDateInput input)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            TimeSpan result = _employees.GetTimeTracking(input.Id, input.Date);

            return Ok(new GetTimeByDatePayload(input.Date, result));
        }

        [HttpPost]
        [Route("getByWeek")]
        public IActionResult GetTimeByWeek(GetTimeByWeekInput input)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            (DateTime start, DateTime end) = _dateProcessor.GetWeekScopeByWeekNumber(input.WeekNumber);
            TimeSpan result = _employees.GetTimeTracking(input.Id, start, end);

            return Ok(new GetTimeByWeekPayload(input.WeekNumber, result));
        }
    }
}
