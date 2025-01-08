using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace My.Application.IRepositories
{
    public interface IReadRepository<T> :IRepository<T> where T : BaseEntity
    {
        Task<IQueryable<T>> GetAllAsync(bool traking = true);
        Task<IQueryable<T>> GetWhereAsync(Expression<Func<T, bool>> method, bool tracking = true);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);
        Task<T> GetByIdAsync(int id,bool tracking = true);

    }
}
