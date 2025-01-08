using AutoMapper;
using Entities.Pagination;
using My.Application.DTOs.Project;
using My.Application.Exceptions.Project;
using My.Application.IRepositories;
using My.Services.Iservices;
using My.Services.Iservices.IProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Services.ServicesManager.Project
{
    public class ProjectManager : IProjectService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public ProjectManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<bool> AddProjectAsync(ProjectDto projectDto)
        {

            if (projectDto is null)
            {
                string msg = "An error occured in the list to be added.";
                _logger.LogWarning(msg);
                throw new KeyNotFoundException(msg);
            }
            var project = new Entities.Models.Project
            {
                Title = projectDto.Title,
                Description = projectDto.Description,
                ImageUrl = projectDto.ImageUrl,
                ProjectUrl = projectDto.ProjectUrl,
                Technologies = projectDto.Technologies,

            };

            var result = await _manager.ProjectWriteRepository.AddAsync(project);
            await _manager.ProjectWriteRepository.SaveAsync();
            return result;

        }

        public async Task<bool> DeleteProjectAsync(int id, bool tracking)
        {
            var deleteProject = _manager.ProjectReadRepository.GetByIdAsync(id, false);
            if (deleteProject is null)
                throw new ProjectNotfoundException(id);
            if (id == 0 || id == null)
                throw new ProjectNotfoundException(id);
            await _manager.ProjectWriteRepository.RemoveAsync(deleteProject.Id);
            await _manager.ProjectWriteRepository.SaveAsync();
            return true;
        }

        public async Task<List<Entities.Models.Project>> GetProjectAllAsync(ProjectPaginationParemeters projectParemeters,bool tracking)
        {
            // ProjectPaginationParemeters projectParemeters burası sayfalama için eklendi 
            var project = await _manager.ProjectReadRepository.GetPaginationForProjectRequestQueryAsync(projectParemeters,false);
            if (project is null)
            {
                string msg = "An error occured while creating the about list";
                _logger.LogWarning(msg);
                throw new KeyNotFoundException(msg); 
            }

            return project.ToList();
        }

        public async Task<Entities.Models.Project> GetProjectByIdAsync(int id)
        {
            var project = await _manager.ProjectReadRepository.GetByIdAsync(id, false);
            if (project is null)
            {
                throw new ProjectNotfoundException(id);
            }

            return project;

        }

        public async Task<bool> UpdateProjectAsync(int id, ProjectDto projectDto)
        {
            var updateProject = await _manager.ProjectReadRepository.GetByIdAsync(id, true);
            if (updateProject is null)
                throw new ProjectNotfoundException(id);

            if (id == 0 || id == null)
                throw new ProjectNotfoundException(id);
            //Mapping 
            updateProject.Title = projectDto.Title;
            updateProject.Description = projectDto.Description;
            updateProject.Technologies = projectDto.Technologies;
            updateProject.ProjectUrl = projectDto.ProjectUrl;
            updateProject.ImageUrl = projectDto.ImageUrl;
            //otomatik mapper 
            // _mapper.Map(projectDto, updateProject);
            var result = _manager.ProjectWriteRepository.Update(updateProject);
            await _manager.ProjectWriteRepository.SaveAsync();

            return result;

        }
    }
}
