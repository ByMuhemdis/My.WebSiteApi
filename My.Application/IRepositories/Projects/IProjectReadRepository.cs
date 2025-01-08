using Entities.Models;
using Entities.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Application.IRepositories.Projects
{
    public interface IProjectReadRepository : IReadRepository<Project>
    {
        //sayfalamanın sadece proje için olmasını istedigimden dolayı burada ReadRepository dışında sadece ProjetcRpositorynin kullanabilecegi bir yapı elde etmek için bu tanımı burada yaptım.
         Task<IEnumerable<Project>> GetPaginationForProjectRequestQueryAsync(ProjectPaginationParemeters projectParemeters, bool traking );
    }
}
