using PhotoMakerStudio.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoMakerStudio.Data.Interfaces
{
    public interface ICategoryRepo
    {
        public Task<Category> AddCategory(string categoryName);
        public Task<Category> GetCategory(string categoryName);
        public Task<bool> DeleteCategory(string categoryName);
        public  Task<List<Category>> GetAllCategories();
    }
}
