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
        private readonly IEmployeeRepository _employees;
        private readonly DateProcessor _dateProcessor;

        public WorkTimesController(IEmployeeRepository employees, DateProcessor dateProcessor)
        {
            _employees = employees;
            _dateProcessor = dateProcessor;
        }

        /// <summary>
        /// Calculates a user's work time on a selected day
        /// </summary>
        /// <param name="input">Contains a user id and day</param>
        /// <returns>Amount of work hours per day</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Projects
        ///     {
        ///         "id": "userId",
        ///         "date": "2022-05-11"
        ///     }
        /// </remarks>
        /// <response code="200">Returns a number of work hours per day</response>
        /// <response code="400">If the input is null</response>
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

        /// <summary>
        /// Calculates a user's work time on a selected week
        /// </summary>
        /// <param name="input">Contains a user id and week</param>
        /// <returns>Amount of work hours per week</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Projects
        ///     {
        ///         "id": "userId",
        ///         "weekNumber": 21
        ///     }
        /// </remarks>
        /// <response code="200">Returns a number of work hours per day</response>
        /// <response code="400">If the input is null</response>
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
