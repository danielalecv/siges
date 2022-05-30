using Microsoft.AspNetCore.Mvc;
using siges.DTO;
using siges.Repository;
using siges.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace siges.Controllers
{
  [Authorize(Roles = "Cliente, SuperUser")]
  public class SettingsController : Controller
  {
    private readonly IBitacoraRepository _bRepo;
    private readonly IEmailConfiguration _emailConf;
    private readonly ISettingsRepository _sRepo;

    public SettingsController(IBitacoraRepository bRepo, IEmailConfiguration emailConf, ISettingsRepository sRepo) {
      _bRepo = bRepo;
      _emailConf = emailConf;
      _sRepo = sRepo;
    }

    public IActionResult Index()
    {
      return View();
    }

    [HttpPost]
    public IActionResult Conf(SettingsDTO conf){
      return RedirectToAction("Index");
    }
  }
}
