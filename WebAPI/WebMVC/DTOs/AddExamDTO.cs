using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.DTOs
{
    public class AddExamDTO
    {
        [Required]
        public int StudentId { get; set; }

        [Required]
        public int SubjectId { get; set; }

        [Required]
        public int ProfessorId { get; set; }

        [Required]
        public int Points { get; set; }

        [Required]
        public int Grade { get; set; }
    }
}
