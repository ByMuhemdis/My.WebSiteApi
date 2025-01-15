using Entities.Models;
using My.Application.IRepositories.Abouts;
using My.Application.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Application.Repositories.Abouts
{
    public class AboutReadRepository : ReadRepository<About>,IAboutReadRepository
    {
        public AboutReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}
