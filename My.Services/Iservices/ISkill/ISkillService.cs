using Entities.Models;
using Entities.Search;
using Entities.Sort;
using My.Application.DTOs.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Services.Iservices.ISkill
{
    public interface ISkillService
    {
        Task<Skill> GetSkillByIdAsync(int id);
        Task<List<Skill>> GetSkillAllAsync(bool tracking);
        Task<List<Skill>> GetSkillSearcParameterAllAsync(SkillSearchParameters skillSearchParameters ,bool tracking);//Arama apisi için sonradan geliştirdik
        Task<List<Skill>> GetSortSkillAsync(SkillSortParameters skillSortParameters ,bool tracking);
        Task<bool> AddSkillAsync(SkillDto skillDto);
        Task<bool> UpdateSkillAsync(int id, SkillDto skillDto);
        Task<bool> DeleteSkillAsync(int id, bool tracking);
    }
}
