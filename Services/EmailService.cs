using ForgotPassword.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ForgotPassword.Services
{
    public class EmailService : IEmailService
    {
        private const string templatePath = @"EmailTemplate/{0}.html";
        private readonly SMTPConfig _smtpConfig;
        public EmailService(IOptions<SMTPConfig> smtpConfig)
        {
            _smtpConfig = smtpConfig.Value;
        }
        public async Task SendEmailForForgotPassword(UserEmail userEmail)
        {
            userEmail.subject = UpdatePlaceHolders("Hello {{UserName}}, reset your password.", userEmail.placeHolders);

            userEmail.body = UpdatePlaceHolders(GetEmailBody("ForgotPassword"), userEmail.placeHolders);

            await SendEmail(userEmail);
        }

        private string UpdatePlaceHolders(string text, List<KeyValuePair<string, string>> keyValuePairs)
        {
            if (!string.IsNullOrEmpty(text) && keyValuePairs != null)
            {
                foreach (var placeholder in keyValuePairs)
                {
                    if (text.Contains(placeholder.Key))
                    {
                        text = text.Replace(placeholder.Key, placeholder.Value);
                    }
                }
            }

            return text;
        }

        private async Task SendEmail(UserEmail userEmail)
        {
            MailMessage mail = new MailMessage
            {
                Subject = userEmail.subject,
                Body = userEmail.body,
                From = new MailAddress(_smtpConfig.senderAddress, _smtpConfig.senderDisplayName),
                IsBodyHtml = _smtpConfig.isBodyHTML
            };

            foreach (var toEmail in userEmail.toEmails)
            {
                mail.To.Add(toEmail);
            }

            NetworkCredential networkCredential = new NetworkCredential(_smtpConfig.username, _smtpConfig.password);

            SmtpClient smtpClient = new SmtpClient
            {
                Host = _smtpConfig.host,
                Port = _smtpConfig.port,
                EnableSsl = _smtpConfig.enableSSL,
                UseDefaultCredentials = _smtpConfig.useDefaultCredentials,
                Credentials = networkCredential
            };

            mail.BodyEncoding = Encoding.Default;

            await smtpClient.SendMailAsync(mail);
        }

        private string GetEmailBody(string templateName)
        {
            var body = File.ReadAllText(string.Format(templatePath, templateName));
            return body;
        }
    }
}
