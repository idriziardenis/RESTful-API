using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Interfaces;
using WebMVC.Models;

namespace WebMVC.Services
{
    public class LogsService : ILogsService
    {
        private ILogsRepository _logRepository;

        public LogsService(ILogsRepository logRepository)
        {
            _logRepository = logRepository;
        }
        public void AddLog(HttpRequest httpRequest, IHttpContextAccessor _httpContextAccessor, string Controller = "", string Action = "", string Comment = "")
        {
            try
            {
                var RemoteIp = GetRemoteIpAddress(httpRequest);
                var RemotePort = GetRemotePort(httpRequest);
                var LocalIp = GetLocalIPAddress(httpRequest);
                var LocalPort = GetLocalPort(httpRequest);

                var log = new Log
                {
                    Controller = Controller,
                    Action = Action,
                    RemoteIpAddress = RemoteIp,
                    RemotePort = RemotePort,
                    LocalIpaddress = LocalIp,
                    LocalPort = LocalPort,
                    Comment = Comment,
                    Date = DateTime.Now
                };
                _logRepository.Add(log);
            }
            catch (Exception)
            {
                var error = new Log
                {
                    Controller = "LogService",
                    Action = "AddLog",
                    Comment = "Error"
                };
                _logRepository.Add(error);
            }
        }

        public static string GetRemoteIpAddress(HttpRequest httpRequest)
        {
            return httpRequest.HttpContext.Connection.RemoteIpAddress.ToString();
        }

        public static string GetRemotePort(HttpRequest httpRequest)
        {
            return httpRequest.HttpContext.Connection.RemotePort.ToString();
        }

        public static string GetLocalIPAddress(HttpRequest httpRequest)
        {
            return httpRequest.HttpContext.Connection.LocalIpAddress.ToString();
        }
        public static string GetLocalPort(HttpRequest httpRequest)
        {
            return httpRequest.HttpContext.Connection.LocalPort.ToString();
        }
    }
}
