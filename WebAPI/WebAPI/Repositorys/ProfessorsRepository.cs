using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repositorys
{
    public class ProfessorsRepository : IProfessorsRepository
    {
        private readonly DBContext _context;

        public ProfessorsRepository(DBContext context)
        {
            _context = context;
        }

        public Task<Professor> Add(Professor t)
        {
            throw new NotImplementedException();
        }

        public Task<Professor> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Professor>> GetAll()
        {
            return await _context.Professors.ToListAsync();
        }

        public Task<Professor> Remove(int id)
        {
            throw new NotImplementedException();
        }
        public Task<Professor> Update(int id, Professor newT)
        {
            throw new NotImplementedException();
        }
    }
}
