using Data.Context;
using Domain.Entities;
using Inlog.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interface;

namespace TesteCrud.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HeroController : ControllerBase
    {
        private readonly IHero _hero;        

        public HeroController(IHero hero, DataContext dataContex)
        {
            _hero = hero;           
        }

        [HttpPost("AddHero")]
        public async Task<IActionResult> AddHero(HeroEntity hero)
        {
            try
            {
                if (hero == null)
                {
                    return BadRequest($"Não Foi possível adicionar o herói {hero} !");
                }
               var heroReturn =  await _hero.Insert(User.GetUserId(), hero);
                return Ok(heroReturn);
                      
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }           
        }

        [HttpGet("ListHero")]
        public async Task<IActionResult> ListHero([FromQuery] PageParams pageParams)
        {
            var result = await _hero.GetAll(User.GetUserId(), pageParams);
            Response.AddPagination(result.CurrentPage, result.PageSize, result.TotalCount, result.TotalPages);
            return Ok(result.ToList());
        }

        [HttpGet("GetHeroById/{id}")]
        public async Task<IActionResult> GetHeroById(int id)
        {
            var result = await _hero.GetById(User.GetUserId(), id);
            if(result == null)
            {
                return NotFound($"Herói com id {id}, não encontrado!");
            }
            return Ok(result);
        }

        [HttpGet("GetHeroByName/{name}")]
        public async Task<IActionResult> GetHeroByName(string name)
        {
            var result = await _hero.GetHeroByName(User.GetUserId(), name);
          
            return Ok(result);
        }

        [HttpDelete("DeletarHero/{id}")]
        public async Task<IActionResult> DeletarHero(int id)
        {
            var result = await _hero.GetById(User.GetUserId(), id);
            if (result == null)
            {
                return NotFound($"id {id}, não encontrado!");
            }
            await _hero.Delete(User.GetUserId(), id);           
            return Ok(new { result = "Herói deletado com sucesso!" });
        }

        [HttpPut("UpdateHero")]
        public async Task<IActionResult> UpdateHero(HeroEntity hero)
        {          

            if(hero != null)
            {
                try
                {
                    await _hero.Update(User.GetUserId(), hero);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                return Ok(new { result  = "Herói atualizado com sucesso!" });

            }
            else { return BadRequest("Objeto não encontrado!"); }            

        }
       
    }
}
