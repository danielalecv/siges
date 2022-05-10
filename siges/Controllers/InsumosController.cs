using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json.Linq;

using siges.Data;
using siges.Models;
using siges.Repository;
using siges.DTO;

namespace siges.Controllers {
  [Authorize(Roles = "Supervisor, Almacen")]
  public class InsumosController : Controller {
    private readonly ApplicationDbContext _context;
    private readonly IBitacoraRepository _bRepo;
    private readonly IInsumoRepository _iRepo;
    private readonly IOrdenServicioRepository _osRepo;
    private readonly IOrdenInsumo _oiRepo;
    private readonly IInventarioI _iiRepo;
    private readonly ILote _lRepo;
    private readonly IPersonaRepository _pRepo;
    private readonly IPaqueteInsumo _piRepo;
    private readonly IPaquete _pqRepo;
    private readonly Settings settings;
    private String LoggedUser;

    public InsumosController(ApplicationDbContext context, IBitacoraRepository bRepo, IInsumoRepository iRepo, IOrdenServicioRepository osRepo, IOrdenInsumo oiRepo, IInventarioI iiRepo, ILote lRepo, IPersonaRepository pRepo, IPaqueteInsumo piRepo, IPaquete pqRepo, ISettingsRepository _set) {
      _context = context;
      _bRepo = bRepo;
      _iRepo = iRepo;
      _osRepo = osRepo;
      _oiRepo = oiRepo;
      _iiRepo = iiRepo;
      _lRepo = lRepo;
      _pRepo = pRepo;
      _piRepo = piRepo;
      _pqRepo = pqRepo;
      settings = _set.GetByVersion("DAMSA");
    }

    public IActionResult IndexCreateInventarioI(){
      List<Insumo> insumos = _iRepo.GetAll(true).ToList();
      ViewData["insumos"] = insumos;
      return View("~/Views/Insumos/CreateInventarioI.cshtml");
    }

    /*public IActionResult IndexDetailInvI(int iId){
      List<OrdenInsumo> oiList = _oiRepo.GetAllOIbyInsumoId(iId).ToList();
      Insumo insumo = _iRepo.GetById(iId);
      ViewData["oiList"] = oiList;
      ViewData["insumo"] = insumo;
      return View("~/Views/Insumos/DetailInventarioI.cshtml");
    }*/

    public async Task<IActionResult> IndexDetailInvI(int iId){
      List<OrdenInsumo> oiList = _oiRepo.GetAllOIbyInsumoIdsencillo(iId).ToList();
      List<Lote> lotes = _lRepo.GetLotesByInsumoId(iId).ToList();
      Dictionary<string, int> salidaXlote = new Dictionary<string, int>();
      foreach(Lote lote in lotes){
        salidaXlote.Add(lote.Descripcion, 0);
      }
      foreach(OrdenInsumo oi in oiList){
        if(oi.OrdenServicio.EstatusServicio == "finalizado")
          try{
            //salidaXlote[oi.LoteType.Descripcion] = salidaXlote[oi.LoteType.Descripcion] + oi.Cantidad;
            salidaXlote[oi.Lote] = salidaXlote[oi.Lote] + oi.Cantidad;
          } catch (KeyNotFoundException ArgumentException){
            Console.WriteLine("\n\nNo se consideró {0} {1}\n", oi.Lote, oi.Cantidad);
          }
      }
      Insumo insumo = _iRepo.GetById(iId);
      ViewData["oiList"] = oiList;
      ViewData["lotes"] = lotes;
      ViewData["insumo"] = insumo;
      ViewData["salidaXlote"] = salidaXlote;
      return View("~/Views/Insumos/DetailInventarioI.cshtml");
    }

    /*public async Task<IActionResult> Movimientos(int id){
      List<OrdenInsumo> oiList = _oiRepo.GetAllOIbyInsumoId(id).ToList();
      ViewData["oiList"] = oiList;
      return View("~/Views/Insumos/DetailInventarioI.cshtml");
    }*/

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SaveInventarioI(LoteDTO loteDto){
      try{
        _lRepo.Insert(new Lote(){
          Descripcion = loteDto.Descripcion,
          Insumo = _iRepo.GetById(loteDto.InsumoId),
          Caducidad = loteDto.Caducidad,
          Persona = _pRepo.GetByEmail(this.User.Identity.Name).Single(),
          Estatus = true,
          FechaAlta = DateTime.Now,
          Cantidad = loteDto.Cantidad,
          Observaciones = String.IsNullOrEmpty(loteDto.Observaciones) ? null : loteDto.Observaciones
        });
        return Json(new{success=true});
      }
      catch(Exception e){
        return Json(new{success=false, data=e});
      } 
    }
     
        [HttpGet]
    public async Task<IActionResult> InventarioI(){
      Dictionary<int, int> movI = new Dictionary<int, int>();
      Dictionary<int, int> entI = new Dictionary<int, int>();
      List<Insumo> iList = _iRepo.GetAll(true).ToList();
      foreach(Insumo i in iList) {
        movI.Add(i.Id, 0);
        entI.Add(i.Id, 0);
      }
      List<OrdenInsumo> oiList = _oiRepo.GetAllsencillo(true).ToList();
      if(oiList != null && oiList.Count > 0){
        foreach(OrdenInsumo oi in oiList){
          if(oi.Insumo.Estatus)
            movI[oi.Insumo.Id] = movI[oi.Insumo.Id] + oi.Cantidad;
        }
      }
      List<Lote> lotes = _lRepo.GetAll(true).ToList();
      if(lotes != null && lotes.Count > 0){
       foreach(Lote lote in lotes){
         if(lote.Insumo.Estatus)
           entI[lote.Insumo.Id] = entI[lote.Insumo.Id] + lote.Cantidad;
        } 
      }
      ViewData["movI"] = movI;
      ViewData["entI"] = entI;
      ViewData["iList"] = iList;
      return View();
    }

    // GET: Insumos
    public async Task<IActionResult> Index() {
      if(User.Identity.IsAuthenticated){
        ViewData["siPaquete"] = (bool)settings.UsoDePaquetes;
        List<Marca> marca = _context.Marca.Where(r => r.Estatus == true).OrderBy(r => r.Descripcion).ToList();
        marca.Sort((a,b) => a.Descripcion.CompareTo(b.Descripcion));
        ViewData["Marcas"] = marca;
        List<TipoProducto> tipoProducto = _context.TipoProducto.Where(r => r.Estatus == true).OrderBy(r => r.Descripcion).ToList();
        tipoProducto.Sort((a,b) => a.Descripcion.CompareTo(b.Descripcion));
        ViewData["Tipos"] = tipoProducto;
        return View(await _context.Insumo.Where(r => r.Estatus == true).ToListAsync());
      }
      return View();
    }

    [HttpGet]
    public async Task<IActionResult> CreatePaquete(){
      List<Insumo> insumos = _iRepo.GetAll(true).ToList();
      List<Lote> lotes = _lRepo.GetAll(true).ToList();
      ViewData["insumos"] = insumos;
      ViewData["lotes"] = lotes;
      return View("~/Views/Insumos/CreatePaquete.cshtml");
    }

    [HttpPost]
    public async Task<IActionResult> SaveCreaPaqueteInsumo(PaqueteInsumoDTO piDTO){
      if(piDTO.JsonInsumos != "[]" && !String.IsNullOrEmpty(piDTO.DescripcionPaquete) && !String.IsNullOrEmpty(piDTO.ClasificacionPaquete)){
        List<int> iIdL = new List<int>();
        foreach(var i in JArray.Parse(piDTO.JsonInsumos)){
          iIdL.Add(Int32.Parse(
                JObject.Parse(i.ToString())
                .Property("iId")
                .Value.ToString()));
        }
        _pqRepo.Insert(new Paquete(){
            Clasificacion = piDTO.ClasificacionPaquete,
            Descripcion = piDTO.DescripcionPaquete,
            Crea = _pRepo.GetByEmail(this.User.Identity.Name).Single(),
            Estatus = true,
            FechaAdmin = DateTime.Now,
            Observaciones = "Sin observaciones"
            });
        foreach(int iId in iIdL){
          _piRepo.Insert(
              new PaqueteInsumo(){
              Estatus = true,
              Insumo = _iRepo.GetById(iId),
              Paquete = _pqRepo.GetById(_pqRepo.GetAll().ToList().Count),
              Cantidad = 1
              });
        }
      }
      return RedirectToAction("Paquetes", "Insumos");
    }

    [HttpGet]
    public async Task<IActionResult> Paquetes(){
      List<PaqueteInsumo> paquetesInsumo = _piRepo.GetAll(true).ToList();
      List<Paquete> paquetes = _pqRepo.GetAll(true).ToList();
      Dictionary<int,int> cantInsumosPaq = new Dictionary<int,int>();
      foreach(Paquete p in paquetes)
        cantInsumosPaq.Add(p.Id, 0);
      foreach(PaqueteInsumo pi in paquetesInsumo){
        cantInsumosPaq[pi.Paquete.Id] = cantInsumosPaq[pi.Paquete.Id] + 1;
      }
      ViewData["cantInsumosPaq"] = cantInsumosPaq;
      ViewData["paquetesInsumo"] = paquetesInsumo;
      return View("~/Views/Insumos/ListPaquetes.cshtml");
    }

    // GET: Insumos/Details/5
    public async Task<IActionResult> Details(int? id) {
      if(User.Identity.IsAuthenticated){
        if (id != null && id > 0) {
          try{
            var insumo = await _context.Insumo.FirstOrDefaultAsync(m => m.Id == id);
            if (insumo == null)
              return NotFound();
            else
              return Json(new{success=true, data=insumo});
          } catch (Exception e){
            return Json(new{success=false, data=e});
          }
        }
        return NotFound();
      }
      return View();
    }

    // POST: Insumos/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Descripcion,Clave,Marca,Tipo,Precio,Estatus,Opcional1,Opcional2")] Insumo insumo) {
      if(User.Identity.IsAuthenticated){
        if(!String.IsNullOrEmpty(insumo.Descripcion) && !String.IsNullOrEmpty(insumo.Clave) && !String.IsNullOrEmpty(insumo.Marca) && !String.IsNullOrEmpty(insumo.Tipo)){
          if (ModelState.IsValid) {
            try {
              insumo.Estatus = true;
              _context.Add(insumo);
              await _context.SaveChangesAsync();
              LoggedUser = this.User.Identity.Name;
              _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Insert", Section = "Insumo", Description = "Insumo no. " + insumo.Id + " agregada." });
              return Json(new{success=true});
            } catch (Exception e) {
              return Json(new{success=false, data=e});
            }
          }
        }
        return NotFound();
      }
      return View();
    }

    // POST: Insumos/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,Clave,Marca,Tipo,Precio,Estatus,Opcional1,Opcional2")] Insumo insumo) {
      if(User.Identity.IsAuthenticated){
        if(id == insumo.Id && !String.IsNullOrEmpty(insumo.Descripcion) && !String.IsNullOrEmpty(insumo.Clave) && !String.IsNullOrEmpty(insumo.Marca) && !String.IsNullOrEmpty(insumo.Tipo)){
          if (ModelState.IsValid) {
            try {
              insumo.Estatus = true;
              _context.Update(insumo);
              await _context.SaveChangesAsync();
              LoggedUser = this.User.Identity.Name;
              _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Update", Section = "Insumo", Description = "Insumo no. " + insumo.Id + " actualizado." });
              return Json(new{success=true});
            } catch (DbUpdateConcurrencyException e) {
              if (!InsumoExists(insumo.Id)) {
                return NotFound();
              } else {
                return Json(new{success=false, data=e});
              }
            }
          }
        }
        return NotFound();
      }
      return View();
    }

    // POST: Insumos/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id) {
      if(User.Identity.IsAuthenticated){
        if(id != null && id > 0){
          try {
            var insumo = await _context.Insumo.FindAsync(id);
            insumo.Estatus = false;
            _context.Update(insumo);
            //_context.Insumo.Remove(insumo);
            await _context.SaveChangesAsync();
            LoggedUser = this.User.Identity.Name;
            _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Delete", Section = "Insumo", Description = "Insumo no. " + insumo.Id + " eliminada." });
            return Json(new{success=true});
          } catch (Exception e) {
            return Json(new{success=false, data=e});
          }
        }
        return NotFound();
      }
      return RedirectToAction("Index", "Insumos");
    }

    private bool InsumoExists(int id) {
      return _context.Insumo.Any(e => e.Id == id);
    }
  }
}
