using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fulcrum_services.Repsonses;

namespace fulcrum_services.Services.Email
{
    public class EmailService : IEmailService
    {
        public EmailResponse sendGeneric(string subject, string message)
        {
            throw new NotImplementedException();
        }

        public EmailResponse sendWithAttachment(string subject, string message)
        {
            throw new NotImplementedException();
        }
    }
}
