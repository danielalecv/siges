using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using siges.Models;
using siges.Repository;

namespace siges.Controllers
{
  [Authorize(Roles = "Supervisor, SuperUser")]
  public class ContratoController : Controller
    {
        private readonly IContratoRepository _repository;

        public ContratoController(IContratoRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}