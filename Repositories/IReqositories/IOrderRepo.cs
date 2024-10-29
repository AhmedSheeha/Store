using Microsoft.AspNetCore.Mvc;
using Store.Models.Dtos;
using Store.Models.Models;

namespace Store.Repositories.IReqositories
{
    public interface IOrderRepo
    {
        public dtoOrders GetOrderById(int orderId);
        public Item GetOrderItemById(int itemId);
        public IEnumerable<Order> GetAllOrders();
        public dtoOrders AddOrder([FromBody] dtoOrders order);

    }
}
