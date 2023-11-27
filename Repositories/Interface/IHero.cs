using Domain.Entities;
using Repositories.Base;

namespace Repositories.Interface
{
    public interface IHero
    {
        Task<HeroEntity> Insert(int userId, HeroEntity entity);
        Task Update(int userId, HeroEntity entity);
        Task Delete(int userId, int Id);
        Task<PageList<HeroEntity>> GetAll(int userId, PageParams pageParams);
        Task<HeroEntity> GetById(int userId, int Id);
        Task<HeroEntity[]> GetHeroByName(int userId, string name);
    }

    
}
