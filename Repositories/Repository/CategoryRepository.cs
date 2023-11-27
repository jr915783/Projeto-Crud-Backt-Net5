using Data.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Interface;

namespace Repositories.Repository
{
    public class CategoryRepository : ICategory
    {
        public readonly DataContext _dataContex;

        
        public CategoryRepository(DataContext dataContex)
        {
            _dataContex = dataContex;
        }

        public async Task Delete(int userId, int Id)
        {
            var entity = await GetById(userId, Id);

            _dataContex.Category.Remove(entity);

            await _dataContex.SaveChangesAsync();

        }

        public async Task<PageList<CategoryEntity>> GetAll(int userId, PageParams pageParams)
        {
            
            IQueryable<CategoryEntity> query = _dataContex.Category.Where(e => e.UserId == userId && e.Name.ToLower().Contains(pageParams.Term.ToLower()))
                                                                        .AsNoTracking().OrderBy(e => e.Id); 
            return await PageList<CategoryEntity>.CreateAsync(query, pageParams.PageNumber, pageParams.pageSize);
        }

        public async Task<CategoryEntity[]> GetCategoryByName(int userId, string name)
        {
            return await _dataContex.Category.Where(x => x.UserId == userId && x.Name.ToLower().Contains(name.ToLower())).ToArrayAsync();
        }

        public async Task<CategoryEntity> GetById(int userId, int Id)
        {
            return await _dataContex.Category.Where(x => x.UserId == userId && x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<CategoryEntity> Insert(int userId, CategoryEntity entity)
        {
            entity.UserId = userId;
             _dataContex.Category.AddAsync(entity);
            await _dataContex.SaveChangesAsync();
            return entity;
        }

        public async Task Update(int userId, CategoryEntity entity)
        {
            entity.UserId = userId;
            _dataContex.Category.Update(entity);
            await _dataContex.SaveChangesAsync();
        }        
    }
}
