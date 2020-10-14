using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repositorys
{
    public class DepartamentsRepository : IDepartamentsRepository
    {
        private readonly DBContext _context;

        public DepartamentsRepository(DBContext context)
        {
            _context = context;
        }

        public Task<Department> Add(Department t)
        {
            throw new NotImplementedException();
        }

        public Task<Department> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Department>> GetAll()
        {
            return await _context.Departments.ToListAsync();
        }

        public Task<Department> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Department> Update(int id, Department newT)
        {
            throw new NotImplementedException();
        }
    }
}
