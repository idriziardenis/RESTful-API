using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repositorys
{
    public class StatusRepository : IStatusRepository
    {
        private readonly DBContext _context;

        public StatusRepository(DBContext context)
        {
            _context = context;
        }

        public Task<Status> Add(Status t)
        {
            throw new NotImplementedException();
        }

        public Task<Status> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Status>> GetAll()
        {
            return await _context.Status.ToListAsync();
        }

        public Task<Status> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Status> Update(int id, Status newT)
        {
            throw new NotImplementedException();
        }
    }
}
