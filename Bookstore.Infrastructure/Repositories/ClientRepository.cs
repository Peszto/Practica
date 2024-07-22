﻿using Bookstore.Domain.Interfaces;
using BookStore.Domain.Models;
using BookStore.Infrastructure.Context;
using BookStore.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Infrastructure.Repositories
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(BookStoreDbContext context) : base(context) { } 

        public override async Task<List<Client>> GetAll()
        {
            return await Db.Clients.AsNoTracking().ToListAsync();
        }

        public override async Task<Client> GetById(int id)
        {
            return await Db.Clients.AsNoTracking().
                Where(c => c.Id == id).
                FirstOrDefaultAsync();
        }
    }
}