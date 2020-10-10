using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Interfaces;
using WebMVC.Models;

namespace WebMVC.Repositorys
{
    public class SubjectsRepository : ISubjectsRepository
    {
        public Task<Subject> Add(Subject t)
        {
            throw new NotImplementedException();
        }

        public Task<Subject> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Subject>> GetAll()
        {
            throw new NotImplementedException();
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
