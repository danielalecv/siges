using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using siges.Data;

namespace siges.Controllers{
  [Authorize(Roles = "SuperUser")]
  public class AdministraController : Controller {
    private readonly UserManager<IdentityUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly ApplicationDbContext context;

    public AdministraController(ApplicationDbContext _contex, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
      this.userManager = userManager;
      this.roleManager = roleManager;
      this.context = _contex;
    }
    public async Task<IActionResult> Index()
    {
      return View(await context.Persona.Where(r => r.Estatus == true).ToListAsync());
    }
  }
}
