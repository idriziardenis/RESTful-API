using System;
using System.Collections.Generic;

namespace WebMVC.Models
{
    public partial class Student
    {
        public Student()
        {
            Exams = new HashSet<Exam>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Index { get; set; }
        public int DepartmentId { get; set; }
        public int Status { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public DateTime? RegisteredDate { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }
    }
}
