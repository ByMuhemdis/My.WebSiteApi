using Entities.Models;
using Entities.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My.Application.DTOs.Project;
using My.Application.IRepositories;
using My.Persistence.Context;
using My.Services.Iservices;


namespace WebSiteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {

        private readonly IServiceManager _service;

        public ProjectController(IServiceManager service)
        {

            _service = service;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllProjects([FromQuery]ProjectPaginationParemeters projectParemeters)
        {
            var projects = await _service.ProjectService.GetProjectAllAsync(projectParemeters,false);
            return Ok(projects);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateProject(ProjectDto projectDto)
        {
            var response = await _service.ProjectService.AddProjectAsync(projectDto);
            return StatusCode(201, response);
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateProject(int id, ProjectDto projectDto)
        {
            var response = await _service.ProjectService.UpdateProjectAsync(id, projectDto);
            return Ok(response);//201
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteProject(int id)
        {

            var response = await _service.ProjectService.DeleteProjectAsync(id, true);
            return Ok(response);
        }
    }
}
