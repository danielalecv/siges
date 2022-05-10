using System;
using System.IO;
using System.Text;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using siges.Data;
using siges.Models;
using siges.Repository;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace siges.Controllers{
  [Authorize(Roles = "Supervisor")]
  public class BulkUploadController : Controller {
    private readonly ApplicationDbContext _context;
    private readonly IInsumoRepository _iRepo;
    private readonly IActivoFijoRepository _afRepo;

    public BulkUploadController(ApplicationDbContext context, IInsumoRepository iR, IActivoFijoRepository afR){
      _context = context;
      _iRepo = iR;
      _afRepo = afR;
    }

    [HttpPost]
    public IActionResult Upload(String body){
      if(User.Identity.IsAuthenticated){
        switch(Request.Headers["Referer"].ToString().Substring(Request.Headers["Origin"].ToString().Length + 1)){
          case "Insumos":
            foreach(var o in JArray.Parse(body)){
              Insumo r = JsonConvert.DeserializeObject<Insumo>(o.ToString());
              r = Utilities.ModelsToUpperCase.ToUpper(r);
              r.Estatus = true;
              if(!_iRepo.Exist(r.Clave)){
                _context.Insumo.Add(r);
                _context.SaveChanges();
              }
            }
          break;
          case "ActivoFijos":
            foreach(var o in JArray.Parse(body)){
              ActivoFijo af = JsonConvert.DeserializeObject<ActivoFijo>(o.ToString());
              af = Utilities.ModelsToUpperCase.ToUpper(af);
              af.Estatus = true;
              if(!_afRepo.Exist(af.Clave)){
                _context.ActivoFijo.Add(af);
                _context.SaveChanges();
              }
            }
          break;
        }
        return Json(new{success=true});
      }
      return NotFound();
    }
  }
}
