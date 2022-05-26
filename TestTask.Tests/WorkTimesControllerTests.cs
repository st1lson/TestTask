using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System;
using TestTaskData.Repositories;
using TestTaskServices;
using TestTaskWebAPI.Controllers;
using TestTaskWebAPI.Data.Inputs;
using TestTaskWebAPI.Data.Payloads;
using Xunit;

namespace TestTask.Tests
{
    public class WorkTimesControllerTests
    {
        [Fact]
        public void GetTimeByDate_Returns_The_Correct_Data()
        {
            // Arrange
            TimeSpan fakeTime = TimeSpan.FromHours(8);
            IEmployeeRepository repository = A.Fake<IEmployeeRepository>();
            DateProcessor dateProcessor = A.Fake<DateProcessor>();
            GetTimeByDateInput input = new("userId", DateTime.Now);
            A.CallTo(() => repository.GetTimeTracking(input.Id, input.Date)).Returns(fakeTime);
            WorkTimesController controller = new(repository, dateProcessor);

            // Act
            IActionResult actionResult = controller.GetTimeByDate(input);

            // Assert
            OkObjectResult result = actionResult as OkObjectResult;
            GetTimeByDatePayload workTime = result?.Value as GetTimeByDatePayload;
            Assert.Equal(fakeTime, workTime?.WorkTime);
        }

        [Fact]
        public void GetTimeByWeek_Returns_The_Correct_Data()
        {
            // Arrange
            TimeSpan fakeTime = TimeSpan.FromHours(40);
            DateTime fakeWeekStart = DateTime.Today - TimeSpan.FromDays(8);
            DateTime fakeWeekEnd = DateTime.Today - TimeSpan.FromDays(1);
            IEmployeeRepository repository = A.Fake<IEmployeeRepository>();
            DateProcessor dateProcessor = A.Fake<DateProcessor>();
            GetTimeByWeekInput input = new("userId", 21);
            A.CallTo(() => dateProcessor.GetWeekScopeByWeekNumber(input.WeekNumber)).Returns((fakeWeekStart, fakeWeekEnd));
            A.CallTo(() => repository.GetTimeTracking(input.Id, fakeWeekStart, fakeWeekEnd)).Returns(fakeTime);
            WorkTimesController controller = new(repository, dateProcessor);

            // Act
            IActionResult actionResult = controller.GetTimeByWeek(input);

            // Assert
            OkObjectResult result = actionResult as OkObjectResult;
            GetTimeByWeekPayload workTime = result?.Value as GetTimeByWeekPayload;
            Assert.Equal(fakeTime, workTime?.WorkTime);
        }
    }
}
