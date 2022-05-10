using System;
using System.Linq;
using System.Globalization;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections.Generic;

using siges.Models;
using siges.DTO;
using siges.Repository;
using siges.Utilities;
using siges.Areas.Identity.Data;

using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace siges.Utilities
{
    public class Notifica
    {
        private string subject;
        private OrdenServicio ordenServicio;
        private MimeMessage message;
        private Multipart multipart;
        private OperadorDTO operador;
        private IPersonaRepository _pRepo;
        private UserManager<RoatechIdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailConfiguration _emailConf;
        private string LoggedUser;
        private Settings settings;

        public Notifica(IEmailConfiguration emailConf, UserManager<RoatechIdentityUser> um, RoleManager<IdentityRole> rm, string subj, Multipart multi, string logUs, Settings set)
        {
            _emailConf = emailConf;
            _userManager = um;
            _roleManager = rm;
            subject = subj;
            multipart = multi;
            LoggedUser = logUs;
            settings = set;
        }

        public Notifica(IEmailConfiguration emailConf, string subj, Multipart multi, Settings set){
          _emailConf = emailConf;
          subject = subj;
          multipart = multi;
          settings = set;
        }

        /*public void NuevaCuentaNotifica(string url, string pass, string usr, string nombre){
          this.message = new MimeMessage();
          this.message.To.Add(new MailboxAddress(nombre, usr));
            using (var client = new SmtpClient()){
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect(_emailConf.Host, _emailConf.Port, _emailConf.EnableSSL);
                client.Authenticate(_emailConf.UserName, _emailConf.Password);
                client.Send(createMessage("NuevaCuenta", "ConfirmEmail", null, null, nombre+"{-}"+usr+"{-}"+pass+"{-}"+url));
                client.Disconnect(true);
            }
            this.message = null;
            return;
        }*/

        public void NuevaCuentaNotifica(string url, string pass, string usr, string nombre){
            Console.WriteLine("Url: " + url);
            Console.WriteLine("Pass: " + pass);
            Console.WriteLine("user: " + usr);
            Console.WriteLine("Nombre: " + nombre);
            this.message = new MimeMessage();
          this.message.To.Add(new MailboxAddress(nombre, usr));
            using (var client = new SmtpClient()){
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect(settings.EmailHost, Int32.Parse(settings.EmailPort), settings.EmailEnableSSL);
                client.Authenticate(settings.EmailUser, settings.EmailPass);
                client.Send(createMessage("NuevaCuenta", "ConfirmEmail", null, null, nombre+"{-}"+usr+"{-}"+pass+"{-}"+url));
                client.Disconnect(true);
            }
            this.message = null;
            return;
        }

        public void OrdenServicioNotifica(string aQuien, string que, List<Operador> osAtendido, OrdenServicio os, string finaDesc)
        {
            sendMessage(aQuien, que, osAtendido, os, finaDesc);
        }

        private MimeMessage createMessage(string aQuien, string que, List<Operador> osAtendido, OrdenServicio os, string finaDesc)
        {
            message.From.Add(new MailboxAddress(" SIGES - Centro de notificaciones", settings.EmailUser));
            message.Subject = String.Format("{0} - {1}.", os != null ? os.Folio : osAtendido != null ? osAtendido[0].OrdenServicio.Folio : "", this.subject);
            message.Body = createBody(aQuien, que, osAtendido, os, finaDesc);
            return message;
        }

        private string getEstatus(string subject)
        {
            foreach (string str in subject.Split(subject, StringSplitOptions.RemoveEmptyEntries))
            {
                Console.WriteLine("\n\nstrings: {0}", str);
            }
            return null;
        }

        private MimeEntity createBody(string aQuien, string que, List<Operador> osAtendido, OrdenServicio os, string finaDesc){
            getEstatus(this.subject);
            BodyBuilder builderBody = new BodyBuilder();
            builderBody.Attachments.Add(multipart);
            if(settings.SendAttachmentFile)
              if(que == "programado"){
                if(os.LineaNegocio.Nombre == "INSTALACIÖN" || os.LineaNegocio.Nombre == "MANTENIMIENTO")
                  builderBody.Attachments.Add(settings.AttachmentFile1Name, settings.AttachmentFile1);
              }
            builderBody.HtmlBody = Emails.GetEmailBody(aQuien, que, osAtendido, os, finaDesc);
            return builderBody.ToMessageBody();
        }

        private async Task<List<RoatechIdentityUser>> getUsersInRole(string role)
        {
            return (List<RoatechIdentityUser>)(await _userManager.GetUsersInRoleAsync(role));
        }

        private IQueryable<IdentityRole> getRoles()
        {
            return _roleManager.Roles;
        }

        //private void enviarMensaje()

        private void enviarMensaje(string aQuien, string que, List<Operador> osAtendido, OrdenServicio os, string finaDesc)
        {
            using (var client = new SmtpClient())
            {
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect(settings.EmailHost, Int32.Parse(settings.EmailPort), settings.EmailEnableSSL);
                client.Authenticate(settings.EmailUser, settings.EmailPass);
                client.Send(createMessage(aQuien, que, osAtendido, os, finaDesc));
                client.Disconnect(true);
            }
        }

        private void sendMessage(string aQuien, string que, List<Operador> osAtendido, OrdenServicio os, string finaDesc) {
          switch (aQuien) {
            case "cliente":
              this.message = new MimeMessage();
              if (os != null) {
                var email1 = Utilities.Tools.ValidaEmail(os.ContactoEmail);
                if(email1 != null)
                  this.message.To.Add(new MailboxAddress(os.Cliente.RazonSocial, email1));
                if (!String.IsNullOrEmpty(os.EmailCC1)) {
                  var email2 = Utilities.Tools.ValidaEmail(os.EmailCC1);
                  if(email2 != null)
                    this.message.Cc.Add(new MailboxAddress(os.NombreCompletoCC1, email2));
                }
                if (!String.IsNullOrEmpty(os.EmailCC2)) {
                  var email3 = Utilities.Tools.ValidaEmail(os.EmailCC2);
                  if (email3 != null)
                    this.message.Cc.Add(new MailboxAddress(os.NombreCompletoCC2, email3));
                }
                enviarMensaje(aQuien, que, osAtendido, os, finaDesc);
                this.message = null;
                return;
              } else if (osAtendido != null) {
                var email1 = Utilities.Tools.ValidaEmail(osAtendido[0].OrdenServicio.ContactoEmail);
                if(email1 != null){
                  this.message.To.Add(new MailboxAddress(osAtendido[0].OrdenServicio.Cliente.RazonSocial, email1));
                }
                if (!String.IsNullOrEmpty(os.EmailCC1)) {
                  var email2 = Utilities.Tools.ValidaEmail(osAtendido[0].OrdenServicio.EmailCC1);
                  if(email2 != null)
                    this.message.Cc.Add(new MailboxAddress(osAtendido[0].OrdenServicio.NombreCompletoCC1, email2));
                }
                if (!String.IsNullOrEmpty(os.EmailCC2)) {
                  var email3 = Utilities.Tools.ValidaEmail(osAtendido[0].OrdenServicio.EmailCC2);
                  if (email3 != null)
                    this.message.Cc.Add(new MailboxAddress(osAtendido[0].OrdenServicio.NombreCompletoCC2, email3));
                }
                enviarMensaje(aQuien, que, osAtendido, os, finaDesc);
                this.message = null;
                return;
              }
              return;

            case "operador":
              if (os != null) {
                foreach (OrdenPersona ordPer in os.Personal) {
                  this.message = new MimeMessage();
                  var email = Utilities.Tools.ValidaEmail(ordPer.Persona.Email);
                  if (email != null) {
                      this.message.To.Add(new MailboxAddress("Cuadrilla", email));
                      enviarMensaje(aQuien, que, osAtendido, os, finaDesc);
                  }
                  this.message = null;
                }
              } else if (osAtendido != null) {
                foreach (Operador oper in osAtendido) {
                  foreach (OrdenPersona ordPer in oper.OrdenServicio.Personal) {
                    this.message = new MimeMessage();
                    var email = Utilities.Tools.ValidaEmail(ordPer.Persona.Email);
                    if (email != null){
                      this.message.To.Add(new MailboxAddress("Cuadrilla", email));
                      enviarMensaje(aQuien, que, osAtendido, os, finaDesc);
                    }
                    this.message = null;
                  }
                }
              }
              return;

            case "ventas":
              this.message = new MimeMessage();
              if (os != null) {
                if (!String.IsNullOrEmpty(os.PersonaComercial.Email)) {
                  var email = Utilities.Tools.ValidaEmail(os.PersonaComercial.Email);
                  if(email != null){
                    this.message.To.Add(new MailboxAddress("Comercial", email));
                    enviarMensaje(aQuien, que, osAtendido, os, finaDesc);
                    this.message = null;
                  }
                  return;
                }
              } else if (osAtendido != null) {
                if (!String.IsNullOrWhiteSpace(osAtendido[0].OrdenServicio.PersonaComercial.Email)) {
                  var email = Utilities.Tools.ValidaEmail(osAtendido[0].OrdenServicio.PersonaComercial.Email);
                  if(email != null){
                    this.message.To.Add(new MailboxAddress("Comercial", email));
                    enviarMensaje(aQuien, que, osAtendido, os, finaDesc);
                  }
                  this.message = null;
                  return;
                }
              }
              return;

            case "supervisor":
              foreach (RoatechIdentityUser user in getUsersInRole("Supervisor").Result) {
                var email = Utilities.Tools.ValidaEmail(user.Email);
                this.message = new MimeMessage();
                if(email != null){
                  this.message.To.Add(new MailboxAddress("Supervisor", email));
                  enviarMensaje(aQuien, que, osAtendido, os, finaDesc);
                }
                this.message = null;
              }
              return;

            case "todos":
              this.message = new MimeMessage();
              foreach (IdentityRole role in getRoles()) {
                foreach (RoatechIdentityUser user in getUsersInRole(role.Name).Result) {
                  this.message = new MimeMessage();
                  this.message.To.Add(new MailboxAddress("SIGES", user.Email));
                  enviarMensaje(aQuien, que, osAtendido, os, finaDesc);
                  this.message = null;
                }
              }
              return;

            case "administracion":
              foreach (RoatechIdentityUser user in getUsersInRole("Administracion").Result) {
                this.message = new MimeMessage();
                this.message.To.Add(new MailboxAddress("Administracion", user.Email));
                enviarMensaje(aQuien, que, osAtendido, os, finaDesc);
                this.message = null;
              }
              return;
          }
        }
    }
}
