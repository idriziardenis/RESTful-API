using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.DTOs;

namespace WebMVC.Interfaces
{
    public interface IDepartamentsRepository
    {
        Task<(bool, IEnumerable<ReadDepartamentDTO>)> GetAll();
    }
}
