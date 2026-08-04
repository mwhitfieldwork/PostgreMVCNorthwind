using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace NWCodeFirstMVC.Infrastructure.Repositories
{
    public class GenericRepository<T> where T : class
    {
        protected readonly PgNwContext _dc;

        public GenericRepository(PgNwContext dc)
        {
            _dc = dc;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dc.Set<T>().ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            return await _dc.Set<T>().FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dc.Set<T>().AddAsync(entity);
            await _dc.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _dc.Set<T>().Update(entity);
            await _dc.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            if (entity != null)
            {
                _dc.Set<T>().Remove(entity);
                await _dc.SaveChangesAsync();
            }
        }
    }
}

