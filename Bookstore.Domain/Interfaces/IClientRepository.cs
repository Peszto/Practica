using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;

namespace Bookstore.Domain.Interfaces
{
    public interface IClientRepository: IRepository<Client>
    {
        new Task<List<Client>> GetAll();
        new Task<Client> GetById(int id);
    }
}
