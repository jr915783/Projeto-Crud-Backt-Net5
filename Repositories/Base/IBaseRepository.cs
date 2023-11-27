using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Base
{
    public interface IBaseRepository<T> where T : class
    {
        Task Insert(int userId, T entity);
        Task Update(int userId,T entity);
        Task Delete(int userId,int Id);
        Task<IEnumerable<T>> GetAll(int userId);
        Task<T> GetById(int userId, int Id);

    }
}
