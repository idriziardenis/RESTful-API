using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.DTOs;

namespace WebMVC.Interfaces
{
    public interface IStatusRepository
    {
        Task<(bool, IEnumerable<ReadStatusesDTO>)> GetAll();
    }
}
