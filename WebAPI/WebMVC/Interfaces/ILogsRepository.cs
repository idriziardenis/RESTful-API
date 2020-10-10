using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models;

namespace WebMVC.Interfaces
{
    public interface ILogsRepository : IDataRepository<Log>
    {
        (IEnumerable<Log>, int) GetLogs(int skip, int take, string query);
        Log FindErrorLog(int id);
    }
}
