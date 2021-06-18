using CirculaireICTKeten.Services.EmailService;
using CirculaireICTKeten.Services.EmailService.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CirculaireICTKeten.Interfaces
{
    public interface ISendEmailService
    {
        void SendMessage(EmailMessage emailMessage);
    }
}
