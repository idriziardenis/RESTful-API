using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repositorys
{
    public class SubjectsRepository : ISubjectsRepository
    {
        private readonly DBContext _context;

        public SubjectsRepository(DBContext context)
        {
            _context = context;
        }

        public Task<Subject> Add(Subject t)
        {
            throw new NotImplementedException();
        }

        public Task<Subject> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Subject>> GetAll()
        {
            return await _context.Subjects.ToListAsync();
        }

        public Task<Subject> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Subject> Update(int id, Subject newT)
        {
            throw new NotImplementedException();
        }
    }
}
