using Microsoft.EntityFrameworkCore;
using PhotoMakerStudio.Data.Interfaces;
using PhotoMakerStudio.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoMakerStudio.Data
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly DataContext _dataContext;

        public CategoryRepo(DataContext dataContext)
        {
           _dataContext = dataContext;
        }

        public async Task<Category> AddCategory(string categoryName)
        {
            Category category = new Category
            {
                CategoryName = categoryName
            };
            await _dataContext.Category.AddAsync(category);
            await _dataContext.SaveChangesAsync();
            return category;
        }
        public async Task<Category> GetCategory(string categoryName)
        {
            var Category = await _dataContext.Category.FirstOrDefaultAsync(x => x.CategoryName == categoryName);
            return Category;
        }

        public async Task<bool> DeleteCategory(string categoryName)
        {

            Category category = await _dataContext.Category.Where(x => x.CategoryName == categoryName).FirstOrDefaultAsync();
            _dataContext.Category.Remove(category);
            if (await _dataContext.SaveChangesAsync() > 0)
                return true;

            return false;
        }

        public async Task<List<Category>> GetAllCategories()
        {
             return await _dataContext.Category.ToListAsync();
        }
    }
}
