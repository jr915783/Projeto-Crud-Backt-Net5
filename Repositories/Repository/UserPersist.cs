using Data.Context;
using Domain.Entities;
using Domain.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories.Base;
using Repositories.Interface;

namespace Repositories.Repository
{
    public class UserPersist : BaseRepositorio<User>, IUserPersist
    {
        public readonly DataContext _contexto;
        public UserPersist(DataContext dataContex) : base(dataContex)
        {
            _contexto = dataContex;
           
        }       

        public async Task<IEnumerable<User>> GetUserAsync()
        {
            return  await _contexto.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _contexto.Users.FindAsync(id);
        }

        public async Task<User> GetUserByUserNameAsync(string username)
        {
             return await _contexto.Users.SingleOrDefaultAsync(user => user.UserName == username.ToLower());
        }
    }
}
