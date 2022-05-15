using ForgotPassword.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForgotPassword.Services
{
    public interface IEmailService
    {
        Task SendEmailForForgotPassword(UserEmail userEmail);
        //Task SendEmail(UserEmail userEmail);
    }
}
