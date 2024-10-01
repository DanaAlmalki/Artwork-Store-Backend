using System.Text.Json;
using Backend_Teamwork.src.Entities;
using Backend_Teamwork.src.Services.category;
using Backend_Teamwork.src.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Backend_Teamwork.src.DTO.CategoryDTO;

namespace Backend_Teamwork.src.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryReadDto>>> GetCategories()
        {
            var categories = await _categoryService.GetAllAsync();
            if (categories == null)
            {
                return NotFound();
            }
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryReadDto>> GetCategoryById([FromRoute] Guid id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpGet("search/{name}")]
        public async Task<ActionResult<CategoryReadDto>> GetCategoryByName([FromRoute] string name)
        {
            var category = await _categoryService.GetByNameAsync(name);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpGet("page")]
        public async Task<ActionResult<List<CategoryReadDto>>> GetCategoriesWithPaginationAsync(
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize
        )
        {
            var categories = await _categoryService.GetWithPaginationAsync(pageNumber, pageSize);
            if (categories == null)
            {
                return NotFound();
            }
            return Ok(categories);
        }

        [HttpGet("sort")]
        public async Task<ActionResult<List<CategoryReadDto>>> SortCategoriesByName()
        {
            var categories = await _categoryService.SortByNameAsync();
            if (categories == null)
            {
                return NotFound();
            }
            return Ok(categories);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CategoryReadDto>> CreateCategory(
            [FromBody] CategoryCreateDto categoryDTO
        )
        {
            var category = await _categoryService.CreateAsync(categoryDTO);
            if (category == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(CreateCategory), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CategoryReadDto>> UpdateCategory(
            [FromRoute] Guid id,
            [FromBody] CategoryUpdateDto categoryDTO
        )
        {
            var category = await _categoryService.UpdateAsync(id, categoryDTO);
            if (category == null)
            {
                return NotFound(); // or BadRequest();
            }
            return Ok(category);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteCategory([FromRoute] Guid id)
        {
            var isDeleted = await _categoryService.DeleteAsync(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
