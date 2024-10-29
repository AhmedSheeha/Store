using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.Data.Models;
using Store.Models.Dtos;
using Store.Models.Models;
using Store.Repositories;
using Store.Repositories.IReqositories;
using static Azure.Core.HttpHeader;
using static System.Net.Mime.MediaTypeNames;

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        public ItemsController(IItemRepo itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        private readonly IItemRepo itemRepository;

        [HttpGet]
        public async Task<IActionResult> AllItems()
        {
           var items = itemRepository.AllItems();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemById(int id)
        {
            var item = itemRepository.GetItemById(id);
            if (item == null)
            {
                return NotFound($"Item Code {id} not exists!");
            }
            return Ok(item);
        }

        [HttpGet("ItemsWithCategory/{idCategory}")]
        public async Task<IActionResult> AllItemsWithCategory(int idCategory)
        {
            var item = itemRepository.AllItemsWithCategory(idCategory);
            if (item == null)
            {
                return NotFound($"Category Id {idCategory} has no items!");
            }
            return Ok(item);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> AddItem([FromForm] mdlItem mdl)
        {
            var item = itemRepository.AddItem(mdl);
            return Ok(item);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateItem(int id, [FromForm] mdlItem mdl)
        {
            var item = itemRepository.UpdateItem(id, mdl);
            if (item == null) return NotFound($"Item Id {id} or Category {mdl.CategoryId} Is is wrong");
            return Ok(item);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = itemRepository.DeleteItem(id);
            if(item == null) return NotFound($"Item Code {id} not exists!");
            return Ok(item);
        }
    }
}