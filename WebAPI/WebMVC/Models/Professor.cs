using System;
using System.Collections.Generic;

namespace WebMVC.Models
{
    public partial class Professor
    {
        public Professor()
        {
            Exams = new HashSet<Exam>();
        }

        public int Id { get; set; }
        public string ProfessorName { get; set; }
        public int SubjectId { get; set; }

        public virtual Subject Subject { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }
    }
}
