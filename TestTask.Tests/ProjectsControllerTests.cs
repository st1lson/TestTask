using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using TestTaskData.Models;
using TestTaskData.Repositories;
using TestTaskWebAPI.Controllers;
using TestTaskWebAPI.Data.Inputs;
using TestTaskWebAPI.Data.Payloads;
using Xunit;

namespace TestTask.Tests
{
    public class ProjectsControllerTests
    {
        [Fact]
        public void Get_Returns_The_Correct_Data()
        {
            // Arrange
            const int count = 5;
            IEnumerable<Project> fakeProjects = A.CollectionOfDummy<Project>(count).AsEnumerable();
            IRepository<Project> repository = A.Fake<IRepository<Project>>();
            IMapper mapper = A.Fake<IMapper>();
            A.CallTo(() => repository.Get(null, null)).Returns(fakeProjects);
            ProjectsController controller = new(repository, mapper);

            // Act
            IActionResult actionResult = controller.Get();

            // Assert
            OkObjectResult result = actionResult as OkObjectResult;
            IEnumerable<Project> returnProject = result?.Value as IEnumerable<Project>;
            Assert.Equal(count, returnProject?.Count());
        }

        [Fact]
        public void GetById_Returns_The_Correct_Data()
        {
            // Arrange
            const string id = "projectId";
            Project fakeProject = A.Dummy<Project>();
            IRepository<Project> repository = A.Fake<IRepository<Project>>();
            IMapper mapper = A.Fake<IMapper>();
            A.CallTo(() => repository.GetById(id)).Returns(fakeProject);
            ProjectsController controller = new(repository, mapper);

            // Act
            IActionResult actionResult = controller.GetById(id);

            // Assert
            OkObjectResult result = actionResult as OkObjectResult;
            Project returnProject = result?.Value as Project;
            Assert.Equal(fakeProject, returnProject);
        }

        [Fact]
        public void Create_Returns_The_Correct_Data()
        {
            // Arrange
            Project fakeProject = A.Dummy<Project>();
            IRepository<Project> repository = A.Fake<IRepository<Project>>();
            IMapper mapper = A.Fake<IMapper>();
            CreateProjectInput input = new("Project1", DateTime.Now - TimeSpan.FromDays(7), DateTime.Now);
            A.CallTo(() => repository.Insert(fakeProject)).Returns(fakeProject);
            A.CallTo(() => mapper.Map<Project>(input)).Returns(fakeProject);
            ProjectsController controller = new(repository, mapper);

            // Act
            Task<IActionResult> actionResult = controller.Create(input);

            // Assert
            OkObjectResult result = actionResult.Result as OkObjectResult;
            CreateProjectPayload returnProject = result?.Value as CreateProjectPayload;
            Assert.Equal(fakeProject, returnProject?.Project);
        }

        [Fact]
        public void Update_Returns_The_Correct_Data()
        {
            // Arrange
            Project updatedProject = A.Dummy<Project>();
            Project fakeProject = A.Dummy<Project>();
            IRepository<Project> repository = A.Fake<IRepository<Project>>();
            IMapper mapper = A.Fake<IMapper>();
            UpdateProjectInput input = new("projectId", "Project1",
                DateTime.Now - TimeSpan.FromDays(7), DateTime.Now);
            A.CallTo(() => repository.GetById(input.Id)).Returns(fakeProject);
            A.CallTo(() => mapper.Map(input, fakeProject)).Returns(fakeProject);
            A.CallTo(() => repository.Update(fakeProject)).Returns(updatedProject);
            ProjectsController controller = new(repository, mapper);

            // Act
            Task<IActionResult> actionResult = controller.Update(input);

            // Assert
            OkObjectResult result = actionResult.Result as OkObjectResult;
            UpdateProjectPayload returnProject = result?.Value as UpdateProjectPayload;
            Assert.Equal(updatedProject, returnProject?.Project);
        }

        [Fact]
        public void Delete_Returns_The_Correct_Data()
        {
            // Arrange
            Project deletedProject = A.Dummy<Project>();
            IRepository<Project> repository = A.Fake<IRepository<Project>>();
            IMapper mapper = A.Fake<IMapper>();
            DeleteProjectInput input = new("projectId");
            A.CallTo(() => repository.Delete(input.Id)).Returns(deletedProject);
            ProjectsController controller = new(repository, mapper);

            // Act
            Task<IActionResult> actionResult = controller.Delete(input);

            // Assert
            OkObjectResult result = actionResult.Result as OkObjectResult;
            DeleteProjectPayload returnProject = result?.Value as DeleteProjectPayload;
            Assert.Equal(deletedProject, returnProject?.Project);
        }
    }
}
