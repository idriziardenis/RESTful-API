using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class Department
    {
        public Department()
        {
            Students = new HashSet<Student>();
            Subjects = new HashSet<Subject>();
        }

        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string Comment { get; set; }

        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
