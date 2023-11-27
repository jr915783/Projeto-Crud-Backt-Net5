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
    public class CategoryController : ControllerBase
    {
        private readonly ICategory _category;        

        public CategoryController(ICategory category, DataContext dataContex)
        {
            _category = category;           
        }

        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory(CategoryEntity category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest($"Não Foi possível adicionar o herói {category} !");
                }
               var categoryReturn =  await _category.Insert(User.GetUserId(), category);
                return Ok(categoryReturn);
                      
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }           
        }

        [HttpGet("ListCategory")]
        public async Task<IActionResult> ListCategory([FromQuery] PageParams pageParams)
        {
            var result = await _category.GetAll(User.GetUserId(), pageParams);
            Response.AddPagination(result.CurrentPage, result.PageSize, result.TotalCount, result.TotalPages);
            return Ok(result.ToList());
        }

        [HttpGet("GetCategoryById/{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var result = await _category.GetById(User.GetUserId(), id);
            if(result == null)
            {
                return NotFound($"Herói com id {id}, não encontrado!");
            }
            return Ok(result);
        }

        [HttpGet("GetCategoryByName/{name}")]
        public async Task<IActionResult> GetCategoryByName(string name)
        {
            var result = await _category.GetCategoryByName(User.GetUserId(), name);
          
            return Ok(result);
        }

        [HttpDelete("DeletarCategory/{id}")]
        public async Task<IActionResult> DeletarCategory(int id)
        {
            var result = await _category.GetById(User.GetUserId(), id);
            if (result == null)
            {
                return NotFound($"id {id}, não encontrado!");
            }
            await _category.Delete(User.GetUserId(), id);           
            return Ok(new { result = "Herói deletado com sucesso!" });
        }

        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(CategoryEntity category)
        {          

            if(category != null)
            {
                try
                {
                    await _category.Update(User.GetUserId(), category);
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
