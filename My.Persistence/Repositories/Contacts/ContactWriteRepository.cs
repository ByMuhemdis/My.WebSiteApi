using Entities.Models;
using My.Application.IRepositories.Contacts;
using My.Application.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Application.Repositories.Contacts
{
    public class ContactWriteRepository : WriteRepository<Contact>, IContactWriteRepository
    {
        public ContactWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
