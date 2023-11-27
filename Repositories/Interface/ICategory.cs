using Domain.Entities;
using Repositories.Base;

namespace Repositories.Interface
{
    public interface ICategory
    {
        Task<CategoryEntity> Insert(int userId, CategoryEntity entity);
        Task Update(int userId, CategoryEntity entity);
        Task Delete(int userId, int Id);
        Task<PageList<CategoryEntity>> GetAll(int userId, PageParams pageParams);
        Task<CategoryEntity> GetById(int userId, int Id);
        Task<CategoryEntity[]> GetCategoryByName(int userId, string name);
    }

    
}
