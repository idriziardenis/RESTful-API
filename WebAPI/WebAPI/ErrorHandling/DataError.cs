using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.ErrorHandling
{
    public class DataError
    {
        public string Message { get; set; }

        public DataError(string msg)
        {
            this.Message = msg;
        }
    }
}
