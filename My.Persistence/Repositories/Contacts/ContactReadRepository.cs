using Entities.Models;
using Microsoft.EntityFrameworkCore;
using My.Application.IRepositories.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My.Persistence.Context;

namespace My.Persistence.Repositories.Contacts
{
    public class ContactReadRepository : ReadRepository<Contact>, IContactReadRepository
    {
        public ContactReadRepository(AppDbContext context) : base(context)
        {
        }

        
    }
}
