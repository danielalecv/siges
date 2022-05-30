using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using siges.Areas.Identity.Data;
using siges.Models;

using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace siges.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<RoatechIdentityUser> _signInManager;
        private readonly UserManager<RoatechIdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<RoatechIdentityUser> userManager,
            SignInManager<RoatechIdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "Datos personales")]
            public Persona per { get; set; }

            /*[Display(Name = "Dirección")]
            public Direccion dir { get; set; }*/

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} y un máximo de {1} caracteres.", MinimumLength = 8)]
            [DataType(DataType.Password)]
            [Display(Name = "Contraseña")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirmar Contraseña")]
            [Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new RoatechIdentityUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    per = new Persona
                    {
                        Nombre = Input.per.Nombre,
                        Paterno = Input.per.Paterno,
                        Materno = Input.per.Materno,
                        Telefono = Input.per.Telefono,
                        RFC = Input.per.RFC,
                        CURP = Input.per.CURP,
                        Email = Input.Email,
                        Estatus = true,
                        Direccion = "Dirección de " + Input.per.Nombre + " " + Input.per.Paterno + " " + Input.per.Materno,
                        Municipio = "Municipio de " + Input.per.Nombre + " " + Input.per.Paterno + " " + Input.per.Materno,
                        EntidadFederativa = "Entidad federativa de " + Input.per.Nombre + " " + Input.per.Paterno + " " + Input.per.Materno,
                        ClaveEmpleado = "Clave de empleado",
                        TelefonoContacto = "Teléfono de contacto",
                        Categoria = "Categoría",
                        Puesto = "Puesto",
                        Sueldo = 0,
                        Opcional1 = "Opcional1",
                        Opcional2 = "Opcional2",
                        Adscripcion = "Adscripción",
                        Fotografia = new byte[] { },
                        Dir = new Direccion
                        {
                            calle = "Calle",
                            numero = 0,
                            colonia = "Colonia",
                            cp = 0,
                            municipio = "Municipio",
                            entidadFederativa = "Entidad federativa",
                            estatus = true
                        },
                        FaceApiId = "SINFACEAPIID"
                    }
                };

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    await _userManager.AddToRoleAsync(user, "Default");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedEmail)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, password = Input.Password, nombre = Input.per.Nombre + " " + Input.per.Paterno + " " + Input.per.Materno });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}