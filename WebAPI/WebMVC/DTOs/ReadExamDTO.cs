using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.DTOs
{
    public class ReadExamDTO
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int SubjectId { get; set; }

        public int ProfessorId { get; set; }

        public int Points { get; set; }

        public int Grade { get; set; }
    }
}
