using Entities.Models;
using My.Application.IRepositories.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My.Persistence.Context;

namespace My.Persistence.Repositories.Contacts
{
    public class ContactWriteRepository : WriteRepository<Contact>, IContactWriteRepository
    {
        public ContactWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
