﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface IStudentsRepository : IDataRepository<Student>
    {
        Task<IEnumerable<Student>> GetLastFiveAsync();
    }
}
