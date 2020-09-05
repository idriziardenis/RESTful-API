using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class Log
    {
        public int LogId { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string RemoteIpAddress { get; set; }
        public string RemotePort { get; set; }
        public string LocalIpaddress { get; set; }
        public string LocalPort { get; set; }
        public string PortalUrl { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
    }
}
