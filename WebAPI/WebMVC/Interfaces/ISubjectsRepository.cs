﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.DTOs;
using WebMVC.Models;

namespace WebMVC.Interfaces
{
    public interface ISubjectsRepository : IDataRepository<Subject>
    {
        Task<(bool, IEnumerable<ReadSubjectDTO>)> GetAll();
    }
}
