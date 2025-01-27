using Entities.Models;
using My.Application.IRepositories.Skills;
using My.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Persistence.Repositories.Skills
{
    public class SkillWriteRepository : WriteRepository<Skill>, ISkillWriteRepository
    {
        public SkillWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
