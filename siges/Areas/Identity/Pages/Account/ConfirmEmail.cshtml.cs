using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

using siges.Areas.Identity.Data;
using siges.Data;
using siges.Models;
using siges.Utilities;
using siges.Repository;

namespace siges.Areas.Identity.Pages.Account {
  [AllowAnonymous]
  public class ConfirmEmailModel : PageModel {
    private readonly UserManager<RoatechIdentityUser> _userManager;
    private readonly IEmailConfiguration _emailConf;
    private readonly ISettingsRepository _setRepo;
    private readonly Settings settings;

    public ConfirmEmailModel(UserManager<RoatechIdentityUser> userManager, ISettingsRepository _set, IEmailConfiguration emailConf) {
      _userManager = userManager;
      _emailConf = emailConf;
      settings = _set.GetByVersion("DAMSA");
    }

    [TempData]
    public string StatusMessage { get; set; }
    public string SolicitarReenvio { get; set; }

    public async Task<IActionResult> OnGetAsync(string userId, string code) {
      if (userId == null || code == null) {
        return RedirectToPage("/Index");
      }

      var user = await _userManager.FindByIdAsync(userId);
      if (user == null) {
        return NotFound($"Unable to load user with ID '{userId}'.");
      }

      Console.WriteLine("\n\nuser id: {0}\n", userId);
      code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
      var result = await _userManager.ConfirmEmailAsync(user, code);
      StatusMessage = result.Succeeded ? "Gracias, tu correo electrónico ha sido confirmado." : "Error al confirmar tu correo electrónico.{-}Ha caducado este mensaje.{-}Se ha reenviado otro correo electrónico a su bandeja de antrada.";
      if(!result.Succeeded){
        var EmailConfirmationUrl = "";
            var u = await _userManager.FindByIdAsync(userId);
            if(!await _userManager.IsEmailConfirmedAsync(u)){
              var c = await _userManager.GenerateEmailConfirmationTokenAsync(u);
              c = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(c));
              EmailConfirmationUrl = Url.Page(
                  "/Account/ConfirmEmail",
                  pageHandler: null,
                  values: new { area = "Identity", userId = userId, code = c },
                  protocol: Request.Scheme);
              Notifica notifica = new Utilities.Notifica(_emailConf, "Confirmación de correo electrónico", new MimeKit.Multipart("Mixed"), settings);
              notifica.NuevaCuentaNotifica(EmailConfirmationUrl, "********", u.Email, ".");
            }
      }
      return Page();
    }
  }
}
