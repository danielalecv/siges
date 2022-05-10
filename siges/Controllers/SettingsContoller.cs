using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using siges.DTO;
using siges.Models;
using siges.Repository;
using siges.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

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
