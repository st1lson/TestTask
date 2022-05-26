using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestTaskData.Models;
using TestTaskData.Repositories;
using TestTaskWebAPI.Data.Inputs;
using TestTaskWebAPI.Data.Payloads;

namespace TestTaskWebAPI.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ProjectsController : Controller
    {
        private readonly IRepository<Project> _projects;
        private readonly IMapper _mapper;

        public ProjectsController(IRepository<Project> projects, IMapper mapper)
        {
            _projects = projects;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all projects
        /// </summary>
        /// <returns>An array of projects</returns>
        /// <response code="200">Returns an array of projects</response>
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Project> projects = _projects.Get();

            return Ok(projects);
        }

        /// <summary>
        /// Gets an object by object id
        /// </summary>
        /// <param name="id">An object id</param>
        /// <returns>An object with an equal id property value</returns>
        /// <response code="200">Returns a project with an equal id property value</response>
        /// <response code="400">If the input is null</response>

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            Project project = _projects.GetById(id);
            if (project is null)
            {
                throw new ArgumentException(id, nameof(id));
            }

            return Ok(project);
        }

        /// <summary>
        /// Creates a new project
        /// </summary>
        /// <param name="input">Contains project name, start date and end date</param>
        /// <returns>A created project object</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Projects
        ///     {
        ///         "name": "Someone",
        ///         "dateStart": "2022-05-05"
        ///         "dateEnd": "2022-05-25"
        ///     }
        /// </remarks>
        /// <response code="200">Returns a created project</response>
        /// <response code="400">If the input is null</response>
        [HttpPost]
        public async Task<IActionResult> Create(CreateProjectInput input)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            Project projectToCreate = _mapper.Map<Project>(input);

            Project cratedProject = _projects.Insert(projectToCreate);
            await _projects.SaveChangesAsync();

            return Ok(new CreateProjectPayload(cratedProject));
        }

        /// <summary>
        /// Updates a project
        /// </summary>
        /// <param name="input">Contains id, project name, start date and end date</param>
        /// <returns>A updated project object</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /Projects
        ///     {
        ///         "id": "userId",
        ///         "name": "Someone",
        ///         "dateStart": "2022-05-01"
        ///         "dateEnd": "2022-05-25"
        ///     }
        /// </remarks>
        /// <response code="200">Returns a updated project</response>
        /// <response code="400">If the input is null</response>
        [HttpPut]
        public async Task<IActionResult> Update(UpdateProjectInput input)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            Project projectToUpdate = _projects.GetById(input.Id);
            projectToUpdate = _mapper.Map(input, projectToUpdate);
            Project updatedProject = _projects.Update(projectToUpdate);
            await _projects.SaveChangesAsync();

            return Ok(new UpdateProjectPayload(updatedProject));
        }

        /// <summary>
        /// Deletes a project
        /// </summary>
        /// <param name="input">Contains project id</param>
        /// <returns>A deleted project object</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /Projects
        ///     {
        ///         "id": "userId"
        ///     }
        /// </remarks>
        /// <response code="200">Returns a deleted project</response>
        /// <response code="400">If the input is null</response>
        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteProjectInput input)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            Project deletedProject = _projects.Delete(input.Id);
            await _projects.SaveChangesAsync();

            return Ok(new DeleteProjectPayload(deletedProject));
        }
    }
}
