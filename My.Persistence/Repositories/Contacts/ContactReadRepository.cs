using Entities.Models;
using Microsoft.EntityFrameworkCore;
using My.Application.IRepositories.Contacts;
using My.Application.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Application.Repositories.Contacts
{
    public class ContactReadRepository : ReadRepository<Contact>, IContactReadRepository
    {
        public ContactReadRepository(AppDbContext context) : base(context)
        {
        }

        
    }
}
