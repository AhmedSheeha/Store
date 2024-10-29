using Store.Repositories.IReqositories;
using Microsoft.AspNetCore.Mvc;
using Store.Data;
using Store.Models.Dtos;
using Store.Models.Models;
using Microsoft.EntityFrameworkCore;
using Store.Data.Models;

namespace Store.Repositories
{
    public class OrderRepository : IOrderRepo
    {
        private readonly AppDbContext context;

        public OrderRepository(AppDbContext context)
        {
            this.context = context;
        }
        public dtoOrders AddOrder(dtoOrders order)
        {
            Order mdl = new()
            {
                CreatedDate = order.OrderDate,
                ordersItems = new List<OrderItem>()
            };
            foreach (var item in order.items)
            {
                OrderItem orderItem = new()
                {
                    ItemId = item.itemId,
                    Price = item.price,
                };
                mdl.ordersItems.Add(orderItem);
            }
            context.Orders.Add(mdl);
            context.SaveChanges();
            order.orderId = mdl.id;
            return order;
        }

         public IEnumerable<Order> GetAllOrders()
        {
            var orders = context.Orders.Include(i => i.ordersItems).ThenInclude(io => io.items).ToArray();
            return orders;
        }

        public dtoOrders? GetOrderById(int orderId)
        {
            //var order = context.Orders.Include(i => i.ordersItems).ThenInclude(io => io.items).Where(x => x.id == orderId).FirstOrDefault();
            var order = context.Orders.Include(i => i.ordersItems).ThenInclude(io => io.items).Where(x => x.id == orderId).FirstOrDefault();

            if (order != null)
            {
                dtoOrders dto = new()
                {
                    orderId = order.id,
                    OrderDate = order.CreatedDate,
                };
                if (order.ordersItems != null && order.ordersItems.Any())
                {
                    foreach (var item in order.ordersItems)
                    {
                        dtoOrdersItems dtoItem = new()
                        {
                            itemId = item.items.Id,
                            itemName = item.items.Name,
                            price = item.Price,
                            quantity = 1,
                        };
                        dto.items.Add(dtoItem);
                    }
                }
                return dto;
            }
            return null;
        }

        public Item GetOrderItemById(int itemId)
        {
            throw new NotImplementedException();
        }
    }
}
