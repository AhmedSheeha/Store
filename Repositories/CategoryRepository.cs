using Store.Repositories.IReqositories;
using Microsoft.AspNetCore.Mvc;
using Store.Data;
using Store.Models.Models;
using Store.Models.Dtos;
using Azure;
using Microsoft.EntityFrameworkCore;

namespace Store.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepo
        {
            private readonly AppDbContext context;

            public CategoryRepository(AppDbContext context) : base(context)
            {
                this.context = context;
            }

        public CategoryDto AddCategory(string category)
        {
            Category c = new() { Name = category };
            context.Categories.Add(c);
            context.SaveChanges();
            CategoryDto dto = new() { Name = category };
            return dto;
        }

        public GeneralResponse GetCategories()
        {
            GeneralResponse response = new GeneralResponse();
            var cats = context.Categories.Include(i => i.Items).ToList();
            if (cats != null)
            {
                List<CategoryDto> Categorieswithproductsname = new();
                foreach (Category category in cats)
                {
                    CategoryDto categoryWith = new CategoryDto();
                    categoryWith.Id = category.Id;
                    categoryWith.Name = category.Name;
                    if (category.Items != null)
                    {
                        foreach (var item in category.Items)
                        {
                            categoryWith.productNames.Add(item.Name);
                        }
                    }
                    Categorieswithproductsname.Add(categoryWith);
                }

                response.IsSuccess = true;
                response.Data = Categorieswithproductsname;
            }
            else
            {
                response.IsSuccess = false;
                response.Data = "The Data is invalid";
            }
            return response;
        }

        public  GeneralResponse GetCategory(int id)
        {
            var cat =  context.Categories.Include(i => i.Items).SingleOrDefault(x => x.Id == id);
            GeneralResponse generalResponse = new GeneralResponse();
            if (cat != null)
            {
                CategoryDto categories = new();
                categories.Id = id;
                categories.Name = cat.Name;
                if (cat.Items != null)
                {
                    foreach (var item in cat.Items)
                    {
                        categories.productNames.Add(item.Name);
                    }
                    generalResponse.IsSuccess = true;
                    generalResponse.Data = categories;
                }
            }
            else
            {
                generalResponse.IsSuccess = false;
                generalResponse.Data = "The Id is invalid";
            }
            return generalResponse;
        }

        public CategoryDto? RemoveCategory(int id)
        {
            var c = context.Categories.SingleOrDefault(x => x.Id == id);
            if (c == null) return null;
            context.Categories.Remove(c);
            context.SaveChanges();
            CategoryDto category = new CategoryDto()
            {
                Name = c.Name,
                Id = id,
            };
            return category;
        }

        public CategoryDto? UpdateCategory(Category category)
        {
            Category? c =  context.Categories.SingleOrDefault(x => x.Id == category.Id);
            CategoryDto dto = new CategoryDto();
            if (c == null) return dto;
            c.Name = category.Name;
            context.SaveChanges();
            dto.Id = category.Id;
            dto.Name = category.Name;
            return dto;
        }
    }
}
