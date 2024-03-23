using AuthorizationServer.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AuthorizationServer.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfiguration;
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(EmailConfiguration emailConfiguration, ILogger<EmailSender> logger)
        {
            _emailConfiguration = emailConfiguration;
            _logger = logger;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                var client = new SmtpClient(_emailConfiguration.Host, _emailConfiguration.Port)
                {
                    Credentials = new NetworkCredential(_emailConfiguration.UserName, _emailConfiguration.Password),
                    EnableSsl = _emailConfiguration.EnableSSL
                };
                await client.SendMailAsync(
                    new MailMessage(_emailConfiguration.UserName, email, subject, htmlMessage) { IsBodyHtml = true }
                );
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, $"Error while sending email\nemail: {email}\nsubject: {subject}\nmessage: {htmlMessage}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while sending email\nemail: {email}\nsubject: {subject}\nmessage: {htmlMessage}");
            }
        }
    }
}