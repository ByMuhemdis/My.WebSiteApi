using Entities.Models;
using Entities.Search;
using Entities.Sort;
using My.Application.IRepositories.Skills;
using My.Application.Context;
using My.Application.Extensions.SkilRepositoyExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Application.Repositories.Skills
{
    public class SkillReadRepository : ReadRepository<Skill>, ISkillReadRepository
    {
        public SkillReadRepository(AppDbContext context) : base(context)
        {
        }

        //Arama
        public async Task<IQueryable<Skill>> GetSearchParameter(SkillSearchParameters skillSearchParameters, bool traking)
        {
            var skill = await GetAllAsync(false);

            var skillSearch = skill.Search(skillSearchParameters.SearchTerm);

            return skillSearch;
        }
        //sıralama

        public async Task<IQueryable<Skill>> GetSortAllAsync(SkillSortParameters sortParameters, bool traking = true)
        {
            var skill= await GetAllAsync(false);
            var skillSort = skill.Sort(sortParameters.OrderBy);
            return skillSort;
        }
    }
}
