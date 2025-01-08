using Entities.Models;
using My.Application.IRepositories;
using My.Application.IRepositories.Abouts;
using My.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Persistence.Repositories.Abouts
{
    public class AboutWriteRepository : WriteRepository<About>, IAboutWriteRepository
    {
        public AboutWriteRepository(AppDbContext context) : base(context)
        {
        }

       
    }
}
