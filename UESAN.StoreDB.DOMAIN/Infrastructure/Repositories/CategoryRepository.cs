using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace UESAN.StoreDB.DOMAIN.Infrastructure.Repositories
{
    public class CategoryRepository
    {
        private readonly StoreDbContext _dbContext;

        public CategoryRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string obtenerApellido()
        {
            return ""
        }

		//METODO SINCRONO
		//public IEnumerable<Category> GetCategories()
		//{
		//    var categories = _dbContext.Category.ToList();
		//    return categories;
		//} ctrl + k + c/u

		public async Task<IEnumerable<Category>> GetCategories()
		{
			var categories = await _dbContext.Category.ToListAsync();
			return categories;
		}

		//GET CATEGORY BY ID
		public async Task<Category> GetCategoryById(int id)
		{
			var category = await _dbContext.Category.Where(c => c.Id == id && c.IsActive==true).FirstOrDefaultAsync();
			return category;
		}

		//Create category
		public async Task<int> Insert(CategoryRepository category)
		{
			await _dbContext.Categories.AddAsync(category);
			int rows = await _dbContext.SaveChangesAsync();
			return rows >0 ? category.Id : -1;
		}
		
		//Update category
		public async Task<bool> Update(Category category)
		{
			_dbContext.Category.Update(category);
			int rows = await _dbContext.SaveChangesAsync();
			return rows > 0;
		}

		//Delete category
		public async Task<bool> Delete(int id)
		{
			var category = await _dbContext.Category.FirstOrDefaultAsync(c => c.Id == id);
			if (category == null) return false;

			category.IsActive = false;
			int rows = await _dbContext.SaveChangesAsync();
			return (rows > 0);

			//_dbContext.Category.Remove(category);

		}

	}
}
