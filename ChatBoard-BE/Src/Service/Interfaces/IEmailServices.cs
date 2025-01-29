﻿using System.Net.Mail;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IEmailServices
    {
        Task<bool> SendForgotEmailAsync(string ToName, string ToEmail, string Token);
        Task<bool> SendEmail(MailMessage Body);
    }
}
