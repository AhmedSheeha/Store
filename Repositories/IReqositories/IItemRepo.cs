using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Store.Models.Dtos;
using Store.Models.Models;

namespace Store.Repositories.IReqositories
{
    public interface IItemRepo
    {
        public  IEnumerable<ItemDto> AllItems();
        public ItemDto GetItemById(int id);
        public mdlItem AddItem(mdlItem mdl);
        public IEnumerable<ItemDto> AllItemsWithCategory(int idCategory);
        public mdlItem UpdateItem(int id, mdlItem mdl);
        //public Item pdateItem(int id, [FromForm] mdlItem mdl);
        public ItemDto DeleteItem(int id);
    }
}
