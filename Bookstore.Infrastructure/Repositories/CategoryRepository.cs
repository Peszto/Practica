using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using BookStore.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(BookStoreDbContext context) : base(context) { }

        public async Task<IEnumerable<IdAndName>> FilterByUserInput (string categoryNamePrefix)
        {
            return await Db.Categories.AsNoTracking()
                .Where(c => c.Name.ToLower().Contains(categoryNamePrefix.ToLower())).
                Select(c => new IdAndName
                {
                    Id = c.Id,
                    FilteredValue = c.Name,
                })
                .ToListAsync();
        }
    }
}