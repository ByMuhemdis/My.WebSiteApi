using Entities.Models;
using My.Application.IRepositories.Projects;
using My.Application.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Application.Repositories.Projects
{
    public class ProjectWriteRepository : WriteRepository<Project>, IProjectWriteRepository
    {
        public ProjectWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
