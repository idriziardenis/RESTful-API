using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Interfaces;
using WebMVC.Models;

namespace WebMVC.Repositorys
{
    public class ExamsRepository : IExamsRepository
    {
        public Task<Exam> Add(Exam t)
        {
            throw new NotImplementedException();
        }

        public Task<Exam> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Exam>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Exam> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Exam> Update(int id, Exam newT)
        {
            throw new NotImplementedException();
        }
    }
}
