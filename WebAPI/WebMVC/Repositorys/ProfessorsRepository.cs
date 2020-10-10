using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Interfaces;
using WebMVC.Models;

namespace WebMVC.Repositorys
{
    public class ProfessorsRepository : IProfessorsRepository
    {
        public Task<Professor> Add(Professor t)
        {
            throw new NotImplementedException();
        }

        public Task<Professor> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Professor>> GetAll()
        {
            throw new NotImplementedException();
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
