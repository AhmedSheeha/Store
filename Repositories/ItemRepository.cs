using Store.Repositories.IReqositories;
using Microsoft.AspNetCore.Mvc;
using Store.Models.Dtos;
using Store.Models.Models;
using Store.Data;
using System.Collections;
using System.IO;

namespace Store.Repositories
{
    public class ItemRepository : IItemRepo
    {
        private readonly AppDbContext context;

        public ItemRepository(AppDbContext context)
        {
            this.context = context;
        }
        public mdlItem AddItem(mdlItem mdl)
        {
            Item item = new Item();
            context.Items.Add(ConvertMdlToItem(mdl, ref item));
            context.SaveChanges();
            return mdl;
        }
        private Item ConvertMdlToItem(mdlItem mdl, ref Item item)
        {
            item.Name = mdl.Name;
            item.Price = mdl.Price;
            item.Notes = mdl.Notes;
            item.CategoryId = mdl.CategoryId;
            if (mdl.Image != null)
            {
                using var stream = new MemoryStream();
                mdl.Image.CopyToAsync(stream);
                item.Image = stream.ToArray();
            }
            return item;
        }
        public IEnumerable<ItemDto> AllItems()
        {
            var items = context.Items.ToList();
            List<ItemDto> items2 = new List<ItemDto>();
            foreach (var item in items)
            {
                items2.Add(ConvertITemToDto(item));
            }
            return items2;
            }
        private ItemDto ConvertITemToDto(Item item)
        {
            ItemDto mdl = new ItemDto
            {

                Name = item.Name,
                Price = item.Price,
                Notes = item.Notes,
                Image = item.Image
            };
            return mdl;
        }
        public IEnumerable<ItemDto> AllItemsWithCategory(int idCategory)
        {

            var items = context.Items.Where(x => x.CategoryId == idCategory).ToList();
            List<ItemDto> items2 = new List<ItemDto>();
            foreach (var item in items)
            {
                items2.Add(ConvertITemToDto(item));
            }
            return items2;
        }

        public ItemDto DeleteItem(int id)
        {
            var item = context.Items.SingleOrDefault(x => x.Id == id);
            ItemDto mdl = new ItemDto();
            if (item == null) return mdl;
            context.Items.Remove(item);
            context.SaveChanges();
            return ConvertITemToDto(item);
        }

        public ItemDto? GetItemById(int id)
        {
            var item = context.Items.SingleOrDefault(x => x.Id == id);
            ItemDto mdl = new ItemDto();
            if (item == null) return mdl;
            return ConvertITemToDto(item);
        }

        public mdlItem UpdateItem(int id, mdlItem mdl)
        {
            var item = context.Items.Find(id);
            if (item == null) return null;
            var isCategoryExists = context.Categories.Any(x => x.Id == mdl.CategoryId);
            if (isCategoryExists) return null;
            ConvertMdlToItem(mdl, ref item);
            context.SaveChanges();
            return mdl;
        }
    }
}
