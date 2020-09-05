using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs
{
    public class ReadUserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public DateTime? TokenExpireTime { get; set; }
        public int RoleId { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
