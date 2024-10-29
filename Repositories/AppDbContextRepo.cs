using Store.Repositories.IReqositories;
using Store.Repositories;
using Store.Data;
using Store.Models.Models;
using Store.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace Store.DataAccess.Repositories
{
    public class AppDbContextRepo : IAppDbContext
    {
        private readonly AppDbContext context;

        public IRepository<Item> Items { get; private set; }
        public ICategoryRepo Categories { get; private set; }
        public IRepository<OrderItem> OrderItems { get; private set; }
        public IOrderRepo Orders { get; private set; }
       
        public IUserRepo Users { get; private set; }
        public AppDbContextRepo(AppDbContext context)
        {
            this.context = context;
            Items = new Repository<Item>(context);
            Categories = new CategoryRepository(context);
            OrderItems = new Repository<OrderItem>(context);
            Orders = new OrderRepository(context);
            Users = new UserRepository(context);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}