using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace My.Application.IRepositories
{
    public interface IWriteRepository<T> :IRepository<T> where T : class
    {
        Task<bool> AddAsync(T entity);
        Task<bool> AddRangeAsync(T entity);
        bool Remove(T entities);

        bool RemoveRange(List<T> datas);
        Task<bool> RemoveAsync(int id);
        bool Update(T entity);
        Task<int> SaveAsync();
    }
}
