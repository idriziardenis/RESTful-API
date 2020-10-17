using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.DTOs;
using WebMVC.Models;

namespace WebMVC.Interfaces
{
    public interface IStudentsRepository
    {
        Task<(bool, string)> Add(AddStudentDTO student);
        Task<(bool, IEnumerable<ReadStudentDTO>)> GetAll();
        Task<(bool, IEnumerable<ReadStudentDTO>)> GetLastFive();
    }
}
