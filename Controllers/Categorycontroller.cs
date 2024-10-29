using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Data;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.AspNetCore.JsonPatch;
using Store.Models.Models;
using Store.Repositories.IReqositories;
using Microsoft.AspNetCore.Authorization;

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        public CategoriesController(ICategoryRepo categoryRepo)
        {
            _CategoryRepo = categoryRepo;
        }

        private readonly AppDbContext _db;

        public ICategoryRepo _CategoryRepo { get; }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategories()
        {
            GeneralResponse cats = _CategoryRepo.GetCategories();
            return Ok(cats);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategory(int id)
        {
            var cat = _CategoryRepo.GetCategory(id);
            if (cat.IsSuccess == false)
            {
                return NotFound($"Category Id {id} not exists ");
            }
            return Ok(cat.Data);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> AddCategory(string category)
        {
            var c = _CategoryRepo.AddCategory(category); 
            return Ok(c);
        }

        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateCategory(Category category)
        {
            var c = _CategoryRepo.UpdateCategory(category);
            if (c == null) return NotFound($"there is no Category with Id {category.Id}");
            return Ok(c);
        }

        /*
        [HttpPatch("{id}")]
        public async Task<IActionResult>
            UpdateCategoryPatch([FromBody] JsonPatchDocument<Category> category, [FromRoute] int id)
        {
            var c = await _db.Categories.SingleOrDefaultAsync(x => x.Id == id);
            if (c == null)
            {
                return NotFound($"Category Id {id} not exists ");
            }
            category.ApplyTo(c);
            await _db.SaveChangesAsync();
            return Ok(c);
        }
        */

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> RemoveCategory(int id)
        {
            var c = _CategoryRepo.RemoveCategory(id);
            if (c == null) return NotFound($"there is no Category with Id {id}");
            return Ok(c);
        }

    }
}