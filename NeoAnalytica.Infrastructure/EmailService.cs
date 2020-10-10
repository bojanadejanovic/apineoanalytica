using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using NeoAnalytica.AppCore.Models;
using NeoAnalytica.Infrastructure.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoAnalytica.Infrastructure
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailConfig;
        private readonly ILogger<EmailService> _logger;

        public EmailService(EmailSettings emailConfig, ILogger<EmailService> logger)
        {
            _emailConfig = emailConfig;
            _logger = logger;
        }
        public async Task SendEmail(Message message)
        {
            var mailMessage = CreateEmailMessage(message);
            await SendMessage(mailMessage);
        }

        private async Task SendMessage(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfig.Server, _emailConfig.Port, MailKit.Security.SecureSocketOptions.StartTls);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(Environment.GetEnvironmentVariable("EmailConfiguration__Username"), Environment.GetEnvironmentVariable("EmailConfiguration__Password"));
                    await client.SendAsync(mailMessage);
                    _logger.LogInformation($"Email to : {mailMessage.To.ToString()} successfully sent.");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error ocurred while sending an email: {ex.Message}");
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var newEmailMessage = new MimeMessage();
            newEmailMessage.From.Add(new MailboxAddress("NeoAnalytica",_emailConfig.FromAddress));
            newEmailMessage.Subject = message.Subject;
            newEmailMessage.To.AddRange(message.To);

            var bodyBuilder = new BodyBuilder { HtmlBody = string.Format("<h3 style='font-weight:bold;'>{0}</h3>", message.Content) };

            if (message.Attachments != null && message.Attachments.Any())
            {
                foreach (var attachment in message.Attachments.Keys)
                {
                    string filePath;
                    message.Attachments.TryGetValue(attachment, out filePath);
                    // create an csv attachment for the file located at path
                    var attachmentCsv = new MimePart("text", "csv")
                    {
                        Content = new MimeContent(File.OpenRead(filePath), ContentEncoding.Default),
                        ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                        ContentTransferEncoding = ContentEncoding.Base64,
                        FileName = Path.GetFileName(filePath)
                    };
                    bodyBuilder.Attachments.Add(attachmentCsv);
                }
            }

            newEmailMessage.Body = bodyBuilder.ToMessageBody();

            return newEmailMessage;
        }
    }
}
