using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repositorys
{
    public class StudentsRepository : IStudentsRepository
    {
        private readonly DBContext _context;

        public StudentsRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<Student> Get(int id)
        {
            return await _context.Students.Include(x => x.Exams).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> Remove(int id)
        {
            var itemToDelete = await Get(id);
            if (itemToDelete != null)
            {
                _context.Remove(itemToDelete);
                await _context.SaveChangesAsync();
                return itemToDelete;
            }
            else
                throw new ArgumentNullException(nameof(itemToDelete));
        }

        
        public async Task<Student> Update(int id, Student newT)
        {
            var item = await Get(id);

            if (item == null)
                return null;

            newT.Id = id;
            newT.LastModifiedDate = DateTime.Now;
            _context.Entry(await _context.Students.FirstOrDefaultAsync(x => x.Id == newT.Id)).CurrentValues.SetValues(newT);
            await _context.SaveChangesAsync();

            return newT;
        }

        public async Task<Student> Add(Student student)
        {
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student));
            }

            var exists = _context.Students.Any(x => x.Index == student.Index);

            if (exists)
            {
                throw new Exception("Studenti me id e dhene ekziston");
            }
            else
            {
                _context.Students.Add(student);

                await _context.SaveChangesAsync();

                return student;
            }
        }

        public IEnumerable<Student> GetLastFive()
        {
            return _context.Students.Take(5).OrderBy(x => x.RegisteredDate).ToList();
        }
    }
}
