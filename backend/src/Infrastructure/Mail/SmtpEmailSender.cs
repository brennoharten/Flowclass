using Application.Common.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Infrastructure.Mail;

public class SmtpEmailSender : IEmailSender
{
    private readonly IConfiguration _cfg;
    public SmtpEmailSender(IConfiguration cfg) => _cfg = cfg;

    public async Task SendAsync(string to, string subject, string htmlBody)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_cfg["Mail:FromName"], _cfg["Mail:FromEmail"]));
        message.To.Add(MailboxAddress.Parse(to));
        message.Subject = subject;
        message.Body = new TextPart("html") { Text = htmlBody };

        using var client = new SmtpClient();
        await client.ConnectAsync(_cfg["Mail:SmtpHost"], int.Parse(_cfg["Mail:SmtpPort"]!), _cfg["Mail:Ssl"] == "true");
        await client.AuthenticateAsync(_cfg["Mail:User"], _cfg["Mail:Pass"]);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}

