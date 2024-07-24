using BookStore.Domain.Models;

namespace BookStore.Domain.Interfaces
{
    public interface IClientRepository: IRepository<Client>
    {
        new Task<List<Client>> GetAll();
        new Task<Client> GetById(int id);
        Task<IEnumerable<BasicModel>> FilterByUserInput(string clientNamePrefix);
    }
}
