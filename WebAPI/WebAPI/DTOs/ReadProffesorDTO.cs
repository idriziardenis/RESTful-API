using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs
{
    public class ReadProffesorDTO
    {
        public int Id { get; set; }
        public string ProfessorName { get; set; }
        public int SubjectId { get; set; }
    }
}
