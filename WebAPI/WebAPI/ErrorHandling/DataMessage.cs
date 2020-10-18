using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.ErrorHandling
{
    public class DataMessage
    {
        public string Message { get; set; }

        public DataMessage(string msg)
        {
            this.Message = msg;
        }
    }
}
