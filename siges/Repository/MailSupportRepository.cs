using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using MimeKit;
using MimeKit.Text;
using siges.Utilities;
using System;
using System.Collections.Generic;
using System.IO;

namespace siges.Repository
{
    public class MailSupportRepository : IMailSupport
    {
        private string DestinyEmail = "daniel.cvazquez@roatech.com.mx";
        private readonly IEmailConfiguration _emailConf;

        public MailSupportRepository(IEmailConfiguration emailConf)
        {
            this._emailConf = emailConf;
        }

        public bool SendMessage(string name, string lastname, string email, string priority, string title, string body)
        {
            Console.WriteLine("Appsettings: " + _emailConf.Host);
            Console.WriteLine("Appsettings: " + _emailConf.UserName);
            Console.WriteLine("Appsettings: " + _emailConf.Password);
            Console.WriteLine("Appsettings: " + _emailConf.Port);

            string content = CreateContent(priority, name, lastname, email, body);

            // create message
            var message = CreateHeader(title);
            message.Body = new TextPart(TextFormat.Html) { Text = content };

            // send email
            try
            {
                using var smtp = new SmtpClient();
                smtp.Connect(_emailConf.Host, _emailConf.Port, SecureSocketOptions.Auto);
                smtp.Authenticate(_emailConf.UserName, _emailConf.Password);
                smtp.Send(message);
                smtp.Disconnect(true);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("error envio de correo: " + ex.Message);
                return false;
            }
        }

        public bool SendMessage(string name, string lastname, string email, string priority, string title, string body, IList<IFormFile> capturas)
        {
            string content = CreateContent(priority, name, lastname, email, body);
            var message = CreateHeader(title);
            //builder to add text and files
            BodyBuilder builderBody = new BodyBuilder();
            builderBody.HtmlBody = content;

            //add files
            byte[] fileBytes;
            foreach (var captura in capturas)
            {
                if (captura.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        captura.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }
                    builderBody.Attachments.Add(captura.FileName, fileBytes);
                }
            }

            message.Body = builderBody.ToMessageBody();

            // send email
            try
            {
                using var smtp = new SmtpClient();
                smtp.Connect(_emailConf.Host, _emailConf.Port, SecureSocketOptions.Auto);
                smtp.Authenticate(_emailConf.UserName, _emailConf.Password);
                smtp.Send(message);
                smtp.Disconnect(true);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("error envio de correo: " + ex.Message);
                return false;
            }
        }
    
        private MimeMessage CreateHeader(string title)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(" SIGES - Centro de notificaciones", _emailConf.UserName));
            message.To.Add(MailboxAddress.Parse(DestinyEmail));
            message.Subject = title;

            return message;
        }

        private string CreateContent(string priority, string name, string lastname, string email, string body)
        {
            string content = "<h2>Soporte Técnico</h2><br>" +
                "<p>Prioridad: " + priority + "</p>" +
                "<p>Nombre: " + name + " " + lastname + "</p>" +
                "<p>Usuario: " + email + "</p>" +
                "<br><p>Descripción del problema: " + body + "</p>";
            return content;
        }
    }
}
