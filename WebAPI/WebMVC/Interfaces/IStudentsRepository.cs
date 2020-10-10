using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models;

namespace WebMVC.Interfaces
{
    public interface IStudentsRepository : IDataRepository<Student>
    {
        IEnumerable<Student> GetLastFive();
    }
}
