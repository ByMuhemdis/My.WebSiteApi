using Entities.Models;
using Microsoft.EntityFrameworkCore;
using My.Application.IRepositories;
using My.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace My.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;

        public ReadRepository(AppDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task<IQueryable<T>> GetAllAsync(bool traking = true)
        {
            var query =Table.AsQueryable();//ürünlerin sorgulanabilir hale gelmesi == aasqueryable
            if (!traking )
                query= query.AsNoTracking();
            return query;
        }

        public async Task<T> GetByIdAsync(int id, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking )
                query= query.AsNoTracking();
            return await query.FirstOrDefaultAsync(data=>data.Id==id);
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
            {
                query = Table.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync(method);
        }

       
        public async Task<IQueryable<T>> GetWhereAsync(Expression<Func<T, bool>> method, bool tracking = true)
        {
           var query = Table.Where(method);
            if (!tracking)
                query = query.AsNoTracking();
            return query;
        }
    }
}
