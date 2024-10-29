using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Models.Dtos;
using Store.Repositories.IReqositories;

namespace Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        public OrdersController(IOrderRepo orderRepo)
        {
            this.orderRepo = orderRepo;
        }

        private readonly IOrderRepo orderRepo;

        [HttpGet("one/{orderId:int}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var dto = orderRepo.GetOrderById(orderId);
            if(dto == null) return NotFound($"Order Id: {orderId} is not found");
            return Ok(dto);
        }

        [HttpGet("[action]/{itemId:int}")]
        public async Task<IActionResult> GetOrderItemById(int itemId)
        {

            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = orderRepo.GetAllOrders();
            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] dtoOrders order)
        {
            if (ModelState.IsValid)
            {
                dtoOrders dto = orderRepo.AddOrder(order);
                if(dto  != null)
                    return Ok(order);
            }
            return BadRequest();
        }
    }
}