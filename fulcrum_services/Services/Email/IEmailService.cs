using fulcrum_services.Repsonses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fulcrum_services.Services.Email
{
    public interface IEmailService
    {
        EmailResponse sendGeneric(string subject, string message);

        EmailResponse sendWithAttachment(string subject, string message);
    }
}
