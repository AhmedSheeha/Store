using Store.Data.Models;
using Store.Models.Models;

namespace Store.Repositories.IReqositories
{
    public interface IAppDbContext
        {
            IRepository<Item> Items { get; }
            ICategoryRepo Categories { get; }
            IRepository<OrderItem> OrderItems { get; }
            IOrderRepo Orders { get; }
            IUserRepo Users { get; }
            void Save();
        }
}
