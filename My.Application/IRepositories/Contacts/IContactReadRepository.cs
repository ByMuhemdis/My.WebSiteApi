using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Application.IRepositories.Contacts
{
    public interface IContactReadRepository :IReadRepository<Contact>
    {
      
    }
}
