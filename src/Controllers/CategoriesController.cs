using System.Text.Json;
using Backend_Teamwork.src.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Teamwork.src.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private static readonly List<Category> _categories = new List<Category>()
        {
            new Category { Id = Guid.NewGuid(), Name = "Paintings" },
            new Category { Id = Guid.NewGuid(), Name = "Sculptures" },
            new Category { Id = Guid.NewGuid(), Name = "Digital arts" },
        };

        [HttpGet]
        public ActionResult GetCategories()
        {
            if (_categories.Count == 0)
            {
                return NotFound();
            }
            return Ok(_categories);
        }

        [HttpGet("{id}")]
        public ActionResult GetCategoryById(Guid id)
        {
            Category? foundCategory = _categories.FirstOrDefault(c => c.Id == id);
            if (foundCategory == null)
            {
                return NotFound();
            }
            return Ok(foundCategory);
        }

        [HttpGet("search-by-name/{name}")]
        public ActionResult GetCategoryByName(string name)
        {
            Category? foundCategory = _categories.FirstOrDefault(c => c.Name == name);
            if (foundCategory == null)
            {
                return NotFound();
            }
            return Ok(foundCategory);
        }

        [HttpGet("page/{pageNumber}/{pageSize}")]
        public ActionResult GetCategoriesByBage(int pageNumber, int pageSize)
        {
            if (_categories.Count == 0)
            {
                return NotFound();
            }
            return Ok(
                _categories
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .OrderBy(c => c.Name)
                    .ToList()
            );
        }

        [HttpGet("sort-by-name")]
        public ActionResult SortCategoriesByName()
        {
            if (_categories.Count == 0)
            {
                return NotFound();
            }
            return Ok(_categories.OrderBy(c => c.Name).ToList());
        }

        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            Category? foundCategory = _categories.FirstOrDefault(c => c.Name == category.Name);
            if (foundCategory != null)
            {
                return BadRequest();
            }
            _categories.Add(category);
            return CreatedAtAction(nameof(AddCategory), new { id = category.Id }, category);
        }

        [HttpPatch("{id}")]
        public ActionResult UpdateCategory(Guid id, JsonElement category)
        {
            Category? foundCategory = _categories.FirstOrDefault(c => c.Id == id);
            if (foundCategory == null)
            {
                return NotFound();
            }
            if (category.TryGetProperty("Name", out var name))
            {
                foundCategory.Name = name.GetString();
            }
            else
            {
                return BadRequest();
            }
            return Ok(foundCategory);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCategory(Guid id)
        {
            Category? foundCategory = _categories.FirstOrDefault(c => c.Id == id);
            if (foundCategory == null)
            {
                return NotFound();
            }
            _categories.Remove(foundCategory);
            return NoContent();
        }
    }
}
