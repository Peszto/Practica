using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using BookStore.Domain.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BookStore.Domain.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly ClientValidator _clientValidator;

        public ClientService(IClientRepository clientRepository, ClientValidator clientValidator)
        {
            _clientRepository = clientRepository;
            _clientValidator = clientValidator;
        }

        public async Task<IEnumerable<Client>> GetAll()
        {
            return await _clientRepository.GetAll();
        }
        public async Task<Client> GetById(int id)
        {
            return await _clientRepository.GetById(id);
        }
        public async Task<Client> Add(Client client)
        {
            if (!_clientValidator.IsValidEmailAddress(client.Email) || !_clientValidator.IsValidPhoneNr(client.PhoneNr))
                return null;
            await _clientRepository.Add(client);
            return client;
        }
        public async Task<Client> Update(Client client)
        {
            if (_clientRepository.Search(c => c.FirstName==client.FirstName
            && c.LastName ==client.LastName
            && c.Email ==client.Email
            && c.PhoneNr == client.PhoneNr
            && c.Address ==client.Address
            && c.Id !=client.Id).Result.Any())
                return null;

            await _clientRepository.Update(client);
            return client;
        }
        public async Task<bool> Remove(Client client)
        {
            await _clientRepository.Remove(client);
            return true;
        }

        public void Dispose()
        {
            _clientRepository?.Dispose();
        }

      
        public async Task<IEnumerable<BasicModel>> FilterByUserInput(string clientName)
        {
            return await _clientRepository.FilterByUserInput(clientName);
        }

    }
}
