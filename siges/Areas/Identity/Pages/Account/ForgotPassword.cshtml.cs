using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

using siges.Areas.Identity.Data;
using siges.Utilities;
using siges.Repository;
using siges.Models;

namespace siges.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<RoatechIdentityUser> _userManager;
        //private readonly IEmailSender _emailSender;
        private readonly IEmailConfiguration _emailConf;
        private Settings settings;

        public ForgotPasswordModel(UserManager<RoatechIdentityUser> userManager, IEmailSender emailSender, IEmailConfiguration emailConf, ISettingsRepository _set)
        {
            _userManager = userManager;
            //_emailSender = emailSender;
            _emailConf = emailConf;
            settings = _set.GetByVersion("DAMSA");
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress(ErrorMessage = "El campo Usuario no es una dirección de correo electrónico válida.")]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine(Input.Email);
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", code },
                    protocol: Request.Scheme);

                /*await _emailSender.SendEmailAsync(
                    Input.Email,
                    "Reset Password",
                    $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");*/
              Notifica notifica = new Utilities.Notifica(_emailConf, "Restablecer Contraseña", new MimeKit.Multipart("Mixed"), settings);
              notifica.NuevaCuentaNotifica(callbackUrl, "Restablecer Contraseña",user.Email, ".");

                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }
    }
}
