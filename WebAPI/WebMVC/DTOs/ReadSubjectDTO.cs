using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.DTOs
{
    public class ReadSubjectDTO
    {
        public int Id { get; set; }
        public string SubjectName { get; set; }
        public string Semester { get; set; }
        public int DepartmentId { get; set; }
    }
}
