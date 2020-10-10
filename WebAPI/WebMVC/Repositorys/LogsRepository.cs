using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Interfaces;
using WebMVC.Models;

namespace WebMVC.Repositorys
{
    public class LogsRepository : ILogsRepository
    {
        public Task<Log> Add(Log t)
        {
            throw new NotImplementedException();
        }

        public Log FindErrorLog(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Log> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Log>> GetAll()
        {
            throw new NotImplementedException();
        }

        public (IEnumerable<Log>, int) GetLogs(int skip, int take, string query)
        {
            throw new NotImplementedException();
        }

        public Task<Log> Remove(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Log> Update(int id, Log newT)
        {
            throw new NotImplementedException();
        }
    }
}
