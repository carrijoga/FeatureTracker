using System.Text;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Utils;

namespace FeatureTracker.Application.Parameters.Email;

public class EmailSenderApplication
{
    #region Properties

    private readonly IConfiguration _configuration;

    #endregion

    #region Constructor

    public EmailSenderApplication(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    #endregion

    #region Methods

    public async Task<bool> SendEmail(string email, string subject, string message)
    {
        var messageMail = new MimeMessage
        {
            From = { new MailboxAddress("FeatureTracker", _configuration["EmailConfiguration:SmtpUsername"] ?? string.Empty) },
            To = { new MailboxAddress("", email) },
            Subject = subject
        };

        messageMail.MessageId = MimeUtils.GenerateMessageId();
        messageMail.Date = DateTimeOffset.Now;
        messageMail.Headers.Add("Return-Path", _configuration["EmailConfiguration:SmtpUsername"] ?? string.Empty);
        messageMail.ReplyTo.Add(new MailboxAddress("Support",
            _configuration["EmailConfiguration:SmtpUsername"] ?? string.Empty));

        var builder = new BodyBuilder
        {
            HtmlBody = message,
            TextBody = "Your email client does not support HTML messages."
        };

        messageMail.Body = builder.ToMessageBody();

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(_configuration["EmailConfiguration:SmtpServer"]!,
                int.Parse(_configuration["EmailConfiguration:SmtpPort"]!), true);
            await client.AuthenticateAsync(_configuration["EmailConfiguration:SmtpUsername"] ?? string.Empty,
                Encoding.UTF8.GetString(
                    Convert.FromBase64String(_configuration["EmailConfiguration:SmtpPassword"] ?? string.Empty)));
            await client.SendAsync(messageMail);
            await client.DisconnectAsync(true);
        }

        return true;
    }

    #endregion
}
