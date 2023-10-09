using System.Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using Senior.Services.Helper;
using Senior.Services.Purchase.Contracts;

namespace Senior.Infrastructure.Others.Email
{
    public class EmailService : IEmail
    {
        private readonly string _emailHosting;
        private readonly string _emailUsername;
        private readonly string _emailPassword;

        public EmailService(IConfiguration configuration)
        {
            this._emailHosting = configuration["EmailHandler:Host"]!;
            this._emailUsername = configuration["EmailHandler:Username"]!;
            this._emailPassword = configuration["EmailHandler:Password"]!;
        }

        public async Task SendEmail(SendEmail email)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Santiago", "slargodiaz@gmail.com"));
                message.To.Add(new MailboxAddress("Dear", email.ToEmail));
                message.Subject = email.Subject;
                message.Body = new TextPart("html")
                {
                    Text = email.BodyHtml
                };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(this._emailHosting, 587, false);
                    await client.AuthenticateAsync(this._emailUsername, this._emailPassword);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                };
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}