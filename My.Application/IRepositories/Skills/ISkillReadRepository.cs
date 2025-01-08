using Entities.Models;
using Entities.Search;
using Entities.Sort;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Application.IRepositories.Skills
{
    public interface ISkillReadRepository : IReadRepository<Skill>
    {
        Task<IQueryable<Skill>> GetSearchParameter(SkillSearchParameters skillSearchParameters, bool traking);
        Task<IQueryable<Skill>> GetSortAllAsync(SkillSortParameters sortParameters,bool traking = true);
    }
} 
