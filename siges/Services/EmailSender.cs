using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using siges.Services;
using siges.Utilities;

using MailKit;
using MailKit.Net.Smtp;
using MimeKit;

namespace siges.Services
{
  // This class is used by the application to send email for account confirmation and password reset.
  // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
  public class EmailSender : siges.Services.IEmailSender
  {
    private string host;
    private int port;
    private bool enableSSL;
    private string userName;
    private string password;
    private readonly IEmailConfiguration _emailConf;

    public EmailSender(siges.Utilities.IEmailConfiguration emailConf){
      this._emailConf = emailConf;
      this.host = emailConf.Host;
      this.port = emailConf.Port;
      this.enableSSL = emailConf.EnableSSL;
      this.userName = emailConf.UserName;
      this.password = emailConf.Password;
    }

    public Task SendEmailAsync(string email, string subject, string message){
      var mimeMessage = new MimeMessage();
      mimeMessage.From.Add(new MailboxAddress(" SIGES - Centro de notificaciones", _emailConf.UserName));
      mimeMessage.To.Add(new MailboxAddress(email, email));
      mimeMessage.Subject = subject;
      mimeMessage.Body = new TextPart("plain") {
        Text = message
      };

      var client = new SmtpClient();
      client.ServerCertificateValidationCallback = (s,c,h,e) => true;
      client.Connect(_emailConf.Host, _emailConf.Port, _emailConf.EnableSSL);
      client.Authenticate(_emailConf.UserName, _emailConf.Password);
      client.Send(mimeMessage);
      return client.SendAsync(mimeMessage);
    }
  }
}
