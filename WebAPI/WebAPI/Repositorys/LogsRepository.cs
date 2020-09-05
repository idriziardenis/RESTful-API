using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Repositorys
{
    public class LogsRepository : ILogsRepository
    {
        private readonly DBContext _context;

        public LogsRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<Log> Add(Log log)
        {
            if (log == null)
            {
                throw new ArgumentNullException(nameof(log));
            }

            _context.Logs.Add(log);

            await _context.SaveChangesAsync();

            return log;
        }

        public Log FindErrorLog(int id)
        {
            try
            {
                return _context.Logs
                    .Where(x => x.LogId == id).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
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
            try
            {
                if (String.IsNullOrWhiteSpace(query))
                {
                    var selectQuery = _context.Logs
                        .OrderByDescending(x => x.LogId);
                    var count = selectQuery.Count();
                    return (selectQuery.Skip(skip).Take(take).AsEnumerable(), count);
                }
                else
                {
                    var selectQuery = _context.Logs
                        .Where(x => x.Controller.ToLower().Contains(query.ToLower())
                        || x.Action.ToLower().Contains(query.ToLower())
                        || x.RemoteIpAddress.ToLower().Contains(query.ToLower())
                        || x.RemotePort.ToLower().Contains(query)
                        ).OrderByDescending(x => x.LogId);
                    var count = selectQuery.Count();
                    return (selectQuery.Skip(skip).Take(take).AsEnumerable(), count);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
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
