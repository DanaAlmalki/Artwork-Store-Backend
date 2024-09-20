using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Backend.src.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Backend.src.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private static readonly List<Category> _categories = new List<Category>()
        {
            new Category { Id = 1, Name = "Paintings" },
            new Category { Id = 2, Name = "Sculptures" },
            new Category { Id = 3, Name = "Digital arts" },
        };

        [HttpGet]
        public ActionResult GetCategories()
        {
            if (_categories.Count == 0)
            {
                return NotFound();
            }
            return Ok(_categories.OrderBy(c => c.Name).ToList());
        }

        [HttpGet("{id}")]
        public ActionResult GetCategoryById(int id)
        {
            Category? foundCategory = _categories.FirstOrDefault(c => c.Id == id);
            if (foundCategory == null)
            {
                return NotFound();
            }
            return Ok(foundCategory);
        }

        [HttpGet("search/{name}")]
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

        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            _categories.Add(category);
            return Created("Category has been added successfully", category);
        }

        [HttpPatch("{id}")]
        public ActionResult UpdateCategory(int id, JsonElement category)
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
        public ActionResult DeleteCategory(int id)
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
