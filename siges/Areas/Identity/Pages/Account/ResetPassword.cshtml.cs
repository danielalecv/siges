using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

using siges.Areas.Identity.Data;

namespace siges.Areas.Identity.Pages.Account {
  [AllowAnonymous]
  public class ResetPasswordModel : PageModel {
    private readonly UserManager<RoatechIdentityUser> _userManager;

    public ResetPasswordModel(UserManager<RoatechIdentityUser> userManager) {
      _userManager = userManager;
    }

    [BindProperty]
    public InputModel Input { get; set; }
    private string decoded {get;set;}

    public class InputModel {
      [Required]
      [EmailAddress]
      public string Email { get; set; }

      [Required]
      [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
      [DataType(DataType.Password)]
      public string Password { get; set; }

      [DataType(DataType.Password)]
      [Display(Name = "Confirm password")]
      [Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
      public string ConfirmPassword { get; set; }

      public string Code { get; set; }
    }

    public IActionResult OnGet(string code = null) {
      if (code == null) {
        return BadRequest("Se debe proporcionar un código para restablecer la contraseña.");
      } else {
        Input = new InputModel {
          Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code))
        };
        decoded = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        Console.WriteLine("\n\t ALGO ESTÄ {0}\t decoded.\n{1}\n", Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code)),decoded);
        return Page();
      }
    }

    public async Task<IActionResult> OnPostAsync(string code = null) {
      if (!ModelState.IsValid) {
        return Page();
      }

        Console.WriteLine("\n\t ALGO ESTÄ {0}\t decoded.\n{1}\n", Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code)),decoded);
      var user = await _userManager.FindByEmailAsync(Input.Email);
      if (user == null) {
        // Don't reveal that the user does not exist
        return RedirectToPage("./ResetPasswordConfirmation");
      }

      Console.WriteLine("\n\n{4} Estoy a punto de cambiar la contraseña con los siguientes datos: email {0}, contraseña {1}, confirm contraseña {2}, code {3}\n", Input.Email, Input.Password, Input.ConfirmPassword, Input.Code, decoded);
      var result = await _userManager.ResetPasswordAsync(user, await _userManager.GeneratePasswordResetTokenAsync(user), Input.Password);

      if (result.Succeeded) {
        return RedirectToPage("./ResetPasswordConfirmation");
      }

      foreach (var error in result.Errors) {
        ModelState.AddModelError(string.Empty, error.Description);
      }
      return Page();
    }
  }
}
