using LibrarySystem.Application.Mail;
using LibrarySystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Application.IServices
{
    public interface IEmailService
    {
        bool SendEmailAsync(MailData maildata);
    }
}
