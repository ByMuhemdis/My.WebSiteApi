using Entities.Models;
using Entities.Pagination;
using Microsoft.EntityFrameworkCore;
using My.Application.IRepositories.Projects;
using My.Application.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Application.Repositories.Projects
{
    public class ProjectReadRepository : ReadRepository<Project>, IProjectReadRepository
    {
        public ProjectReadRepository(AppDbContext context) : base(context)
        {
        }
        //Sadece sayfalama proje kısmında kullanılacagı için burada getirirken yeni bir getAll yapacagız. ve artık listelemelerde bu metot kyullanılacak.
        public async Task<IEnumerable<Project>> GetPaginationForProjectRequestQueryAsync(ProjectPaginationParemeters projectParemeters, bool traking)
        {
            //istersek şu şekilde bir sayfalama yapabilir
            //istersekde böyle bir sayfalama yapabiliriz.
            //var listProject = await GetAllAsync(traking);

            //listProject = listProject.Skip((projectParemeters.PageNumber - 1) * projectParemeters.PageSize).Take(projectParemeters.PageSize);
            //return listProject;
            // Veriyi listeye dönüştürüp işlemleri uygula

            return await (await GetAllAsync(traking))
           .Skip((projectParemeters.PageNumber - 1) * projectParemeters.PageSize)
            //dizinler yazılımda 0 dan başladıgı için burada sayfa sayısı olan pageNumber ilk sayfa için 0 olmalıdır .
            //bu yuzden biz 1 sayfa dedigimizde aslında 0 rıncı sayfayı istyoruz yani ilk sayfa arka planda sıfrıdır.
            //diger tarafda da kullanıcı diyelim li 2 syafaya gecti biz bir sayfada kat kayıt var ise bunu hesaplayıp projectParemeters.PageSize gecerek işlen yapacagız 
           .Take(projectParemeters.PageSize)
           .ToListAsync();
           
        }
    }
}
