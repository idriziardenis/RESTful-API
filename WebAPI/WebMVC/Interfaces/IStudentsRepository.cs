using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.DTOs;
using WebMVC.Models;

namespace WebMVC.Interfaces
{
    public interface IStudentsRepository : IDataRepository<Student>
    {
        Task<(bool, IEnumerable<ReadStudentDTO>)> GetAll();
        Task<(bool, IEnumerable<ReadStudentDTO>)> GetLastFive();
    }
}
