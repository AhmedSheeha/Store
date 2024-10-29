using Microsoft.AspNetCore.Mvc;
using Store.Models.Dtos;
using Store.Models.Models;

namespace Store.Repositories.IReqositories
{
    public interface ICategoryRepo
    {
        public GeneralResponse GetCategories();
        public GeneralResponse GetCategory(int id);
        public CategoryDto AddCategory(string category);
        public CategoryDto? UpdateCategory(Category category);
        public CategoryDto RemoveCategory(int id);
    }
}
