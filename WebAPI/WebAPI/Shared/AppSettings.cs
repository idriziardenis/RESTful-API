using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Shared
{
    public class AppSettings
    {
        public string SecretKey { get; set; }
        public string ConnectionStrings_Local { get; set; }
        public string ConnectionStrings_Public { get; set; }
    }
}
