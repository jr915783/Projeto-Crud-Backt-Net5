using Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Base
{
    public class BaseRepositorio<T> : IBaseRepository<T> where T : class
    {
        private readonly DataContext _dataContex;

        public BaseRepositorio(DataContext dataContex)
        {
            _dataContex = dataContex;
        }

        public async Task Delete(int userId, int Id)
        {
            var entity = await GetById(userId, Id);
            _dataContex.Set<T>().Remove(entity);
            await _dataContex.SaveChangesAsync();

        }

        public async Task<IEnumerable<T>> GetAll(int userId)
        {
            return await _dataContex.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetById(int userId, int Id)
        {
           return await _dataContex.Set<T>().FindAsync(Id);
        }       

        public async Task Insert(int userId, T entity)
        {
            await _dataContex.Set<T>().AddAsync(entity);
            await _dataContex.SaveChangesAsync();
        }

        public async Task Update(int userId, T entity)
        {
            _dataContex.Set<T>().Update(entity);
            await _dataContex.SaveChangesAsync();
        }
    }
}
