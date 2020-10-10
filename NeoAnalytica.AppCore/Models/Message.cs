using MimeKit;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeoAnalytica.AppCore.Models
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public Dictionary<string, string> Attachments { get; set; }
        public Message(IEnumerable<string> to, string subject, string content, Dictionary<string, string> attachements)
        {
            To = new List<MailboxAddress>();

            To.AddRange(to.Select(x => new MailboxAddress(x)));
            Subject = subject;
            Content = content;
            Attachments = attachements;
        }
    }
}
