using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Interfaces
{
    public interface ILogsService
    {
        void AddLog(HttpRequest httpRequest, IHttpContextAccessor _httpContextAccessor, string Controller = "", string Action = "", string Comment = "");
    }
}
