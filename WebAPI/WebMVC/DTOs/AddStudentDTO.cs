using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Enumerations;

namespace WebMVC.DTOs
{
    public class AddStudentDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Index { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        [Required]
        public Status Status { get; set; }
    }
}
