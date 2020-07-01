using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhotoMakerStudio.Data.Interfaces;
using PhotoMakerStudio.DTO;
using PhotoMakerStudio.Model;

namespace PhotoMakerStudio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepo _categoryRepo;

        public CategoryController(ICategoryRepo categoryRepo)
        {
          _categoryRepo = categoryRepo;
        }

        [HttpPost("NewCategory")]
        public async Task<IActionResult> AddNewCategory([FromBody]CategoryDto categoryDto)
        {
            if (await _categoryRepo.GetCategory(categoryDto.CategoryName) != null)
                return BadRequest("this category is already exist");

            await _categoryRepo.AddCategory(categoryDto.CategoryName);
            return StatusCode(201);
        }

        [HttpGet("CategoryList")]
        public async Task<List<Category>> CategoryList()
        {
            return await _categoryRepo.GetAllCategories();
        }

        [HttpPost("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory([FromBody] CategoryDto categoryDto)
        {
            if (await _categoryRepo.DeleteCategory(categoryDto.CategoryName))
                return Ok("Category Deleted");
            return BadRequest();
        }


        
    }
}
