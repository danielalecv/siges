using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using siges.Areas.Identity.Data;
using siges.Repository;
using siges.Models;
using siges.Utilities;

namespace siges.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<RoatechIdentityUser> _userManager;
        private readonly SignInManager<RoatechIdentityUser> _signInManager;
        private readonly IPersonaRepository perRepo;
        private readonly IBitacoraRepository _bRepo;
        private string LoggedUser;

        public IndexModel(UserManager<RoatechIdentityUser> userManager, SignInManager<RoatechIdentityUser> signInManager, IPersonaRepository _peR, IBitacoraRepository bR)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _bRepo = bR;
            perRepo = _peR;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
            [Display(Name = "Datos completos")]
            public Persona Datos { get; set; }
        }

        private async Task LoadAsync(RoatechIdentityUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var datosCompletos = perRepo.GetByEmail(userName).Single();

            Username = userName;

            Input = new InputModel { PhoneNumber = phoneNumber, Datos = datosCompletos };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"{Constants.Manage.UnableToLoadUser} '{_userManager.GetUserId(User)}'.");
            }
            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"{Constants.Manage.UnableToLoadUser} '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                ViewData["Message"] = Constants.Manage.ValidateGeneralDirection;
                return Page();
                //return RedirectToPage();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    ViewData["Message"] = Constants.Manage.ValidateGeneralDirection;
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"{Constants.Manage.UnexpectedErrorPhoneNumber} '{userId}'.");
                }
            }

            Persona persona = perRepo.GetByEmail(await _userManager.GetUserNameAsync(user)).Single();
            //if(!String.IsNullOrEmpty(Input.Datos.Nombre) && !String.IsNullOrEmpty(Input.Datos.Paterno) && !String.IsNullOrEmpty(Input.Datos.Materno) && !String.IsNullOrEmpty(Input.Datos.RFC) && !String.IsNullOrEmpty(Input.Datos.CURP) && !String.IsNullOrEmpty(Input.Datos.Telefono) && !String.IsNullOrEmpty(Input.Datos.TelefonoContacto) && !String.IsNullOrEmpty(Input.Datos.Direccion) && !String.IsNullOrEmpty(Input.Datos.EntidadFederativa) && !String.IsNullOrEmpty(Input.Datos.Municipio)){
            persona.Nombre = Input.Datos.Nombre;
            persona.Paterno = Input.Datos.Paterno;
            persona.Materno = Input.Datos.Materno;
            persona.RFC = Input.Datos.RFC;
            persona.CURP = Input.Datos.CURP;
            persona.Telefono = Input.Datos.Telefono;
            persona.TelefonoContacto = Input.Datos.TelefonoContacto;
            persona.Direccion = Input.Datos.Direccion;
            persona.EntidadFederativa = Input.Datos.EntidadFederativa;
            persona.Municipio = Input.Datos.Municipio;
            //Console.WriteLine("\n\n\tfoto: " + Input.Datos.Fotografia + "\n");
            //}
            try
            {
                perRepo.Update(persona);
                LoggedUser = this.User.Identity.Name;
                _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Update", Section = "Catálogo de Persona", Description = "Persona no. " + persona.Id + " actualizada." });
                StatusMessage = Constants.Manage.SuccessfulUpdate;
            }
            catch (Exception e)
            {
                ContentResult cr = new ContentResult();
                cr.ContentType = "application/json";
                cr.StatusCode = 401;
                cr.Content = e.ToString();
                return cr;
            }

            await _signInManager.RefreshSignInAsync(user);
            //return RedirectToPage();
            return Page();
        }
    }
}
