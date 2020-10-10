using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Enumerations;

namespace WebMVC.DTOs
{
    public class ReadStudentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Index { get; set; }
        public int DepartmentId { get; set; }
        public Status Status { get; set; }
    }
}
