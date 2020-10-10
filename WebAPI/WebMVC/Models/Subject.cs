using System;
using System.Collections.Generic;

namespace WebMVC.Models
{
    public partial class Subject
    {
        public Subject()
        {
            Exams = new HashSet<Exam>();
            Professors = new HashSet<Professor>();
        }

        public int Id { get; set; }
        public string SubjectName { get; set; }
        public string Semester { get; set; }
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }
        public virtual ICollection<Professor> Professors { get; set; }
    }
}
