using BookStore.Domain.Models;

namespace BookStore.Domain.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<IdAndName>> FilterByUserInput(string categoryNamePrefix);
    }
}