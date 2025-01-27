using Entities.Models;
using My.Application.IRepositories.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My.Persistence.Context;

namespace My.Persistence.Repositories.Projects
{
    public class ProjectWriteRepository : WriteRepository<Project>, IProjectWriteRepository
    {
        public ProjectWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
