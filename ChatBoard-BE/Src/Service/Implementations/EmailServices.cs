using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Common.Helpers;
using Logger.Interfaces;
using Service.Interfaces;

namespace Service.Implementations
{
    public class EmailServices : IEmailServices
    {
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly IEventLogger eventLogger;

        public EmailServices(IWebHostEnvironment hostingEnvironment, IEventLogger eventLogger)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.eventLogger = eventLogger;
        }

        public async Task<bool> SendEmail(MailMessage Body)
        {
            try
            {
                var smtp = new SmtpClient
                {
                    Host = AppSettingHelper.GetSmtpServerName(),
                    Port = AppSettingHelper.GetSmtpServerPort(),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    EnableSsl = true,
                    Credentials = new NetworkCredential(AppSettingHelper.GetSmtpEmailAddress(), AppSettingHelper.GetSmtpEmailPassword())
                };
                await smtp.SendMailAsync(Body);
                Body.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> SendForgotEmailAsync(string ToName, string ToEmail, string Token)
        {
            try
            {
                MailMessage message = new MailMessage(new MailAddress(AppSettingHelper.GetSmtpEmailAddress(), AppSettingHelper.GetSmtpEmailFrom()), new MailAddress(ToEmail, ToName));
                message.Subject = $"Account: Forgot Password";
                message.IsBodyHtml = true;

                string htmlbody = "";
                string pathToFile = hostingEnvironment.WebRootPath +
                    Path.DirectorySeparatorChar + "html" +
                    Path.DirectorySeparatorChar + "emails" +
                    Path.DirectorySeparatorChar + "forgotemail.html";
                using (StreamReader SourceReader = File.OpenText(pathToFile))
                {
                    htmlbody = SourceReader.ReadToEnd();
                }
                htmlbody = htmlbody.Replace("{link}", Token);
                message.Body = htmlbody;
                return await SendEmail(message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error sending confirm email");
            }
        }
    }
}
