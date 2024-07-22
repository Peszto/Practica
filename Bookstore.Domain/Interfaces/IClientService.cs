using BookStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Interfaces
{
    public interface IClientService : IDisposable
    {
        Task<IEnumerable<Client>> GetAll();
        Task<Client> GetById(int id);
        Task<Client> Add(Client client);
        Task<Client> Update(Client client);
        Task<bool> Remove(Client client);
    }
}
