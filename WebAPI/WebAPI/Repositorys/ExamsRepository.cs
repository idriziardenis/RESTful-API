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
    public class ExamsRepository : IExamsRepository
    {
        private readonly DBContext _context;

        public ExamsRepository(DBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<Exam> Add(Exam t)
        {
            if (t == null)
            {
                throw new ArgumentNullException(nameof(t));
            }

            var professor = await _context.Professors.FindAsync(t.ProfessorId);

            if (professor.SubjectId != t.SubjectId)
            {
                throw new Exception("Regjisro lenden qe i perket profesorit adekuat!");
            }

            var exams = await GetAll();

            foreach (var item in exams)
            {
                if (item.StudentId == t.StudentId)
                {
                    if (item.ProfessorId == t.ProfessorId)
                    {
                        if (item.SubjectId == t.SubjectId)
                        {
                            throw new Exception("Ky provim eshte i regjistruar me heret");
                        }
                    }
                }
            }

            _context.Add(t);
            await _context.SaveChangesAsync();
            return t;
        }

        public async Task<Exam> Get(int id)
        {
            return await _context.Exams.FindAsync(id);
        }

        public async Task<IEnumerable<Exam>> GetAll()
        {
            return await _context.Exams.ToListAsync();
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
