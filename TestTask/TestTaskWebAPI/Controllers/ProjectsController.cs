using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
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
        private readonly GenericRepository<Project> _projects;
        private readonly IMapper _mapper;

        public ProjectsController(GenericRepository<Project> projects, IMapper mapper)
        {
            _projects = projects;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                projects = _projects.Get()
            });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            Project project = _projects.GetById(id);
            if (project is null)
            {
                throw new ArgumentException(id, nameof(id));
            }

            return Ok(new
            {
                project
            });
        }

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
