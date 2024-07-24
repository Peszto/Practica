using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using BookStore.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(BookStoreDbContext context) : base(context) { }

        public async Task<IEnumerable<BasicModel>> FilterByUserInput (string categoryNamePrefix)
        {
            return await Db.Categories.AsNoTracking()
                .Where(c => c.Name.ToLower().Contains(categoryNamePrefix.ToLower())).
                Select(c => new BasicModel
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();
        }
    }
}