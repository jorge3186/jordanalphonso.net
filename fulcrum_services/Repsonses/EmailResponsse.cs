using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fulcrum_services.Repsonses
{
    public class EmailResponse
    {
        public int Status { get; set; }

        public string Message { get; set; }

        public EmailResponse(int status, string message)
        {
            Status = status;
            Message = message;
        }

        public EmailResponse(int status)
        {
            Status = status;
        }
    }
}
