using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Interfaces;
using WebMVC.Models;

namespace WebMVC.Repositorys
{
    public class StudentsRepository : IStudentsRepository
    {
        public Task<Student> Add(Student t)
        {
            throw new NotImplementedException();
        }

        public Task<Student> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Student>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Student> GetLastFive()
        {
            throw new NotImplementedException();
        }

        public Task<Student> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Student> Update(int id, Student newT)
        {
            throw new NotImplementedException();
        }
    }
}
