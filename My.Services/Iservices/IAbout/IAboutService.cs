using Entities.Models;
using My.Application.DTOs.About;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Services.Iservices.IAbout
{
    public interface IAboutService
    {
        Task<About> GetAboutByIdAsync(int id);
        Task<List<AboutDto>> GetAboutAllAsync(bool tracking);
        Task<bool> AddAboutAsync(AboutDto aboutDto);
        Task<bool> UpdateAboutAsync(int id, AboutDto aboutDto);
        Task<bool> DeleteAboutAsync(int id, bool tracking);
    }
}
