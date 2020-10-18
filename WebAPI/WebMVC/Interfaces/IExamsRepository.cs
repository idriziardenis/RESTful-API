using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.DTOs;
using WebMVC.Models;

namespace WebMVC.Interfaces
{
    public interface IExamsRepository
    {
        Task<(bool, string)> Add(AddExamDTO t);
        Task<(bool, IEnumerable<ReadExamDTO>)> GetAll();
    }
}
