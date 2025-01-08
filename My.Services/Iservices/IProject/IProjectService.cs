using Entities.Models;
using Entities.Pagination;
using My.Application.DTOs.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Services.Iservices.IProject
{
    public interface IProjectService
    {
        Task<Project> GetProjectByIdAsync(int id);
        Task<List<Project>> GetProjectAllAsync(ProjectPaginationParemeters projectParemeters,bool tracking);//sayfalamadan dolayı burada ProjetcParameter ifadesini ekledik
        Task<bool> AddProjectAsync(ProjectDto projectDto);
        Task<bool> UpdateProjectAsync(int id, ProjectDto projectDto);
        Task<bool> DeleteProjectAsync(int id, bool tracking);
    }
}
