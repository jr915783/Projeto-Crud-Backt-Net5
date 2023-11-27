using Data.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Interface;

namespace Repositories.Repository
{
    public class HeroRepository : IHero
    {
        public readonly DataContext _dataContex;

        
        public HeroRepository(DataContext dataContex)
        {
            _dataContex = dataContex;
        }

        public async Task Delete(int userId, int Id)
        {
            var entity = await GetById(userId, Id);

            _dataContex.Hero.Remove(entity);

            await _dataContex.SaveChangesAsync();

        }

        public async Task<PageList<HeroEntity>> GetAll(int userId, PageParams pageParams)
        {
            
            IQueryable<HeroEntity> query = _dataContex.Hero.Where(e => e.UserId == userId && e.Name.ToLower()
                                                                                   .Contains(pageParams.Term.ToLower())).Include(x =>x.Category)
                                                                        .AsNoTracking().OrderBy(e => e.Id); 
            return await PageList<HeroEntity>.CreateAsync(query, pageParams.PageNumber, pageParams.pageSize);
        }

        public async Task<HeroEntity[]> GetHeroByName(int userId, string name)
        {
            return await _dataContex.Hero.Where(x => x.UserId == userId && x.Name.ToLower().Contains(name.ToLower())).ToArrayAsync();
        }

        public async Task<HeroEntity> GetById(int userId, int Id)
        {
            return await _dataContex.Hero.Where(x => x.UserId == userId && x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<HeroEntity> Insert(int userId, HeroEntity entity)
        {
            entity.UserId = userId;
             _dataContex.Hero.AddAsync(entity);
            await _dataContex.SaveChangesAsync();
            return entity;
        }

        public async Task Update(int userId, HeroEntity entity)
        {
            entity.UserId = userId;
            _dataContex.Hero.Update(entity);
            await _dataContex.SaveChangesAsync();
        }        
    }
}
