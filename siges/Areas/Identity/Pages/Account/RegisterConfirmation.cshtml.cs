using System;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

using siges.Areas.Identity.Data;
using siges.Utilities;
using siges.Repository;
using siges.Models;

namespace siges.Areas.Identity.Pages.Account {
  [AllowAnonymous]
  public class RegisterConfirmationModel : PageModel {
    private readonly UserManager<RoatechIdentityUser> _userManager;
    private readonly IEmailSender _sender;
    private readonly IEmailConfiguration _emailConf;
    private readonly Settings settings;

    public RegisterConfirmationModel(UserManager<RoatechIdentityUser> userManager, IEmailSender sender, IEmailConfiguration emailConf, ISettingsRepository _set) {
      _userManager = userManager;
      _sender = sender;
      _emailConf = emailConf;
      settings = _set.GetByVersion("DAMSA");
    }

    public string Email { get; set; }
    public string Password { get; set; }
    public string Nombre { get; set; }
    public bool DisplayConfirmAccountLink { get; set; }
    public string EmailConfirmationUrl { get; set; }

    public async Task<IActionResult> OnGetAsync(string email, string password, string nombre) {
      if (email == null && password == null && nombre == null) {
        return RedirectToPage("/Index");
      }

      var user = await _userManager.FindByEmailAsync(email);
      if (user == null) {
        return NotFound($"Unable to load user with email '{email}'.");
      }

      Email = email;
      Password = password;
      Nombre = nombre;
      // Once you add a real email sender, you should remove this code that lets you confirm the account
      DisplayConfirmAccountLink = true;
      if (DisplayConfirmAccountLink) {
        var userId = await _userManager.GetUserIdAsync(user);
        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        EmailConfirmationUrl = Url.Page(
            "/Account/ConfirmEmail",
            pageHandler: null,
            values: new { area = "Identity", userId = userId, code = code },
            protocol: Request.Scheme);
      }

      try{
        Notifica notifica = new Utilities.Notifica(_emailConf, "Confirmación de correo electrónico", new MimeKit.Multipart("Mixed"), settings);
        notifica.NuevaCuentaNotifica(EmailConfirmationUrl, Password, Email, Nombre);
      } catch (Exception e){
        return NotFound($"No fue posible enviar corre electrónico '{e.ToString()}'.");
      }

      /*string m = String.Format(@"Saludos,

        Le informamos que su usuario a sido dado de alta con las siguientes credenciales:
usuario: {0}
contraseña: {1}

Para activar su cuenta es necesario confirmar su email dando click en la siguiente liga:
{2}
",Email, Password,EmailConfirmationUrl);
var message = new MimeMessage ();
message.From.Add (new MailboxAddress ("SIGES - Centro de notificaciones", _emailConf.UserName));
message.To.Add (new MailboxAddress ("Nueva cuenta", Email));
message.Subject = "Confirmación de correo electrónico";

message.Body = new TextPart ("plain") {
Text = m
};

Console.WriteLine("\n\n\tMensaje: "+m+"Host: "+_emailConf.Host+" Port: "+_emailConf.Port+" SSL: "+_emailConf.EnableSSL+" UserName: "+_emailConf.UserName+" Password: "+_emailConf.Password+"\n");
using (var client = new MailKit.Net.Smtp.SmtpClient()){
client.ServerCertificateValidationCallback = (s, c, h, e) => true;
client.Connect(_emailConf.Host, _emailConf.Port, SecureSocketOptions.SslOnConnect);
client.Authenticate(_emailConf.UserName, _emailConf.Password);
client.Send(message);
client.Disconnect(true);
}*/
      //return Page();
      return RedirectToAction("AdminUsers", "Admin");
      }
  }
}
