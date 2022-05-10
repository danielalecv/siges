using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using siges.DTO;
using siges.Data;
using siges.Models;
using siges.Repository;

namespace siges.Controllers {
  [Authorize(Roles = "Supervisor, Almacen, Ventas")]
  public class ActivoFijosController : Controller {
    private readonly ApplicationDbContext _context;
    private readonly IBitacoraRepository _bRepo;
    private readonly IInventarioAF _iaf;
    private readonly IActivoFijoRepository _afRepo;
    private readonly IPersonaRepository _pRepo;
    private readonly IOrdenServicioRepository _osRepo;
    private readonly IOrdenActivoFijo _oafRepo;
    private readonly IProducto _prodRepo;
    private String LoggedUser;

    public ActivoFijosController(ApplicationDbContext context, IBitacoraRepository bRepo, IInventarioAF iaf, IActivoFijoRepository afRepo, IPersonaRepository pRepo, IOrdenServicioRepository osRepo, IOrdenActivoFijo oafRepo, IProducto prodRepo) {
      _context = context;
      _bRepo = bRepo;
      _iaf = iaf;
      _afRepo = afRepo;
      _pRepo = pRepo;
      _osRepo = osRepo;
      _oafRepo = oafRepo;
      _prodRepo = prodRepo;
    }

    // GET: Historial ActivoFijos/Productos
    public async Task<IActionResult> History(int id) {
      if(User.Identity.IsAuthenticated){
        if(id != null && id > 0) {
          try{
            List<OrdenActivoFijo> ordenActivoFijo = _oafRepo.GetByAFId(id).ToList();
            if(ordenActivoFijo.Count > 0){
              ViewData["ordenAF"] = ordenActivoFijo;
              return View();
            } else {
              ViewData["ordenAF"] = new List<OrdenActivoFijo>();
              return View();
            }
          } catch (Exception e) {
            return Json(new{success=false, data=e});
          }
        }
        return NotFound();
      }
      return View();
    }

    // GET: ActivoFijos
    public async Task<IActionResult> Index() {
      if(User.Identity.IsAuthenticated){
        List<Marca> marca = _context.Marca.Where(r => r.Estatus == true).OrderBy(r => r.Descripcion).ToList();
        marca.Sort((a,b) => a.Descripcion.CompareTo(b.Descripcion));
        ViewData["Marcas"] = marca;
        List<TipoProducto> tipoProducto = _context.TipoProducto.Where(r => r.Estatus == true).OrderBy(r => r.Descripcion).ToList();
        tipoProducto.Sort((a,b) => a.Descripcion.CompareTo(b.Descripcion));
        ViewData["Tipos"] = tipoProducto;
        return View(await _context.ActivoFijo.Where(r => r.Estatus == true).ToListAsync());
      }
      return View();
    }

    public async Task<IActionResult> TransferirInventarioAF(){
      List<ActivoFijo> afList = _afRepo.GetAll(true).ToList();
      ViewData["afList"] = afList;
      return View("~/Views/ActivoFijos/Transferencias.cshtml");
    }

    public async Task<IActionResult> InventarioAF(){
      int salidas = 0;
      Dictionary<int,int> movAF = new Dictionary<int,int>();
      Dictionary<int,int> invAFDict = new Dictionary<int,int>();
      List<OrdenServicio> osList = _osRepo.GetAll(true).ToList();
      List<ActivoFijo> afList = _afRepo.GetAll(true).ToList();
      if(afList != null && afList.Count > 0){
        foreach(ActivoFijo af in afList){
          movAF.Add(af.Id,0);
          invAFDict.Add(af.Id,0);
        }
        if(osList != null && osList.Count > 0){
          foreach(OrdenServicio os in osList){
            foreach(OrdenActivoFijo oaf in os.Activos){
              if(String.Equals(os.EstatusServicio, "programado") || String.Equals(os.EstatusServicio, "reprogramado")){
                salidas = movAF[oaf.ActivoFijo.Id];
                movAF[oaf.ActivoFijo.Id] = salidas + 1;
              }
            }
          }
        }
      }
      List<InventarioAF> invAFList = _iaf.GetAllInventarioAF().ToList();
      int cant = 0;
      if(invAFList != null && invAFList.Count > 0){
        foreach(InventarioAF invaf in invAFList){
          cant = invAFDict[invaf.ActivoFijo.Id];
          invAFDict[invaf.ActivoFijo.Id] = cant + invaf.Cantidad;
        }
      }
      ViewData["movimientos"] = movAF;
      ViewData["inventarioAF"] = invAFList;
      ViewData["invAFDict"] = invAFDict;
      ViewData["activoFijo"] = afList;
      return View();
    }

    /*[Authorize(Roles = "Supervisor, Almacen")]
    public async Task<IActionResult> InventarioAF(){
      if(User.Identity.IsAuthenticated){
        try{
          int salidas = 0;
          Dictionary<int,int> movAF = new Dictionary<int,int>();
          Dictionary<int,int> invAFDict = new Dictionary<int,int>();
          List<OrdenServicio> osList = _osRepo.GetAll(true).ToList();
          List<ActivoFijo> afList = _afRepo.GetAll(true).ToList();
          if(afList != null && afList.Count > 0){
            foreach(ActivoFijo af in afList){
              movAF.Add(af.Id,0);
              invAFDict.Add(af.Id,0);
            }
            if(osList != null && osList.Count > 0){
              foreach(OrdenServicio os in osList){
                foreach(OrdenActivoFijo oaf in os.Activos){
                  salidas = movAF[oaf.ActivoFijo.Id];
                  movAF[oaf.ActivoFijo.Id] = salidas + 1;
                }
              }
            }
          }
          List<InventarioAF> invAFList = _iaf.GetAllInventarioAF().ToList();
          int cant = 0;
          if(invAFList != null && invAFList.Count > 0){
            foreach(InventarioAF invaf in invAFList){
              cant = invAFDict[invaf.ActivoFijo.Id];
              invAFDict[invaf.ActivoFijo.Id] = cant + invaf.Cantidad;
            }
          }
          return Json(new{
              success=true,
              data=new{
                movimientos=movAF,
                inventarioAF=invAFList,
                invAFDict=invAFDict,
                activoFijo=afList
              },
            });
        } catch (Exception e) {
          return Json(new{success=false, data=e});
        }
      }
      return View();
    }*/


    /*[Authorize(Roles = "Supervisor, Almacen")]
    public IActionResult IndexCreateInventarioAF(){
      if(User.Identity.IsAuthenticated){
        try {
          List<ActivoFijo> activoFijo = _afRepo.GetAll(true).ToList();
          return Json(new{success=true, data=activoFijo});
        } catch (Exception e) {
          return Json(new{success=false, data=e});
        }
      }
      return View();
    }*/

    [Authorize(Roles = "Supervisor, Almacen")]
    public IActionResult IndexEditInvAF(int invAfId){
      return View();
    }

    public IActionResult IndexDetailInvAF(int afId){
      List<OrdenActivoFijo> oafList = _oafRepo.GetAllOAFbyAFIdsencillo(afId).ToList();
      ActivoFijo activoFijo = _afRepo.GetById(afId);
      ViewData["oafList"] = oafList;
      ViewData["activoFijo"] = activoFijo;
      return View("~/Views/ActivoFijos/DetailInventarioAF.cshtml");
    }

    /*
    [Authorize(Roles = "Supervisor, Almacen")]
    public IActionResult IndexDetailInvAF(int afId){
      if(User.Identity.IsAuthenticated){
        if(afId != null && afId > 0){
          try{
            ActivoFijo af = _afRepo.GetById(afId);
            List<InventarioAF> invAFList = _iaf.GetAllInventarioAF().ToList();
            List<OrdenServicio> osList = _osRepo.GetAll(true).ToList();
            List<ActivoFijo> afList = _afRepo.GetAll(true).ToList();
            List<InventarioAF> iafL = new List<InventarioAF>();
            List<OrdenServicio> osL = new List<OrdenServicio>();
            if(invAFList != null && invAFList.Count > 0){
              foreach(InventarioAF iaf in invAFList){
                if(iaf.ActivoFijo.Id == afId)
                  iafL.Add(iaf);
              }
            }
            if(osList != null && osList.Count > 0){
              foreach(OrdenServicio os in osList){
                foreach(OrdenActivoFijo aF in os.Activos){
                  if(aF.ActivoFijo.Id == afId)
                    osL.Add(os);
                }
              }
            }
            return Json(new{
                success=true,
                data=new{
                activoFijo=af,
                invAF=iafL,
                ordenServicio=osL
                },
                });
          } catch (Exception e){
            return Json(new{success=false, data=e});
          }
        }
        return NotFound();
      }
      return RedirectToAction("Index", "ActivoFijos");
    }*/

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SaveInventarioAF(InventarioAFDTO invAF){
      try{
        InventarioAF iAF = new InventarioAF(){ActivoFijo = _afRepo.GetById(invAF.ActivoFijoId), Persona = _pRepo.GetByEmail(this.User.Identity.Name).Single(), Estatus = true, FechaAlta = DateTime.Now, Cantidad = invAF.Cantidad, Observaciones = invAF.Observaciones != null ? invAF.Observaciones : ""}; 
        _iaf.Insert(iAF);
        return Json(new{success=true});
      }
      catch(Exception e) {
        return Json(new{success=false, data=e});
      }
    }

    /*[HttpPost]
    [Authorize(Roles = "Supervisor, Almacen")]
    public IActionResult SaveInventarioAF(InventarioAFDTO invAF){
      InventarioAF iAF = new InventarioAF(){ActivoFijo = _afRepo.GetById(invAF.ActivoFijoId), Persona = _pRepo.GetByEmail(this.User.Identity.Name).Single(), Estatus = true, FechaAlta = DateTime.Now, Cantidad = invAF.Cantidad, Observaciones = invAF.Observaciones != null ? invAF.Observaciones : ""};
      _iaf.Insert(iAF);
      return RedirectToAction("InventarioAF");
    }*/

    // GET: ActivoFijos/Details/5
    public async Task<IActionResult> Details(int? id) {
      if(User.Identity.IsAuthenticated){
        if (id != null && id > 0) {
          try{
            var activoFijo = await _context.ActivoFijo.FirstOrDefaultAsync(m => m.Id == id);
            if (activoFijo == null)
              return NotFound();
            else
              return Json(new{success=true, data=activoFijo});
          } catch (Exception e) {
            return Json(new{success=false, data=e});
          }
        }
        return NotFound();
      }
      return View();
    }

    // POST: ActivoFijos/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Supervisor, Almacen")]
    public async Task<IActionResult> Create([Bind("Descripcion,Clave,Marca,Tipo,Precio,Estatus,Opcional1,Opcional2")] ActivoFijo activoFijo) {
      if(User.Identity.IsAuthenticated){
        if(!String.IsNullOrEmpty(activoFijo.Descripcion) && !String.IsNullOrEmpty(activoFijo.Clave) && !String.IsNullOrEmpty(activoFijo.Marca) && !String.IsNullOrEmpty(activoFijo.Tipo)){
          if (ModelState.IsValid) {
            try{
              activoFijo.Estatus = true;
              _context.Add(activoFijo);
              await _context.SaveChangesAsync();
              LoggedUser = this.User.Identity.Name;
              _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Insert", Section = "Activo Fijo", Description = "Activo Fijo no. " + activoFijo.Id + " agregado." });
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

    // POST: ActivoFijos/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Supervisor, Almacen")]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Descripcion,Clave,Marca,Tipo,Precio,Estatus,Opcional1,Opcional2")] ActivoFijo activoFijo) {
      if(User.Identity.IsAuthenticated){
        if (id == activoFijo.Id && !String.IsNullOrEmpty(activoFijo.Descripcion) && !String.IsNullOrEmpty(activoFijo.Clave) && !String.IsNullOrEmpty(activoFijo.Marca) && !String.IsNullOrEmpty(activoFijo.Tipo)) {
          if (ModelState.IsValid) {
            try {
              activoFijo.Estatus = true;
              _context.Update(activoFijo);
              await _context.SaveChangesAsync();
              LoggedUser = this.User.Identity.Name;
              _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Update", Section = "Activo Fijo", Description = "Activo Fijo no. " + activoFijo.Id + " actualizado." });
              return Json(new{success=true});
            } catch (DbUpdateConcurrencyException e) {
              if (!ActivoFijoExists(activoFijo.Id))
                return NotFound();
              else
                return Json(new{success=false, data=e});
            }
          }
        }
        return NotFound();
      }
      return View();
    }

    // POST: ActivoFijos/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Supervisor, Almacen")]
    public async Task<IActionResult> DeleteConfirmed(int id) {
      if(User.Identity.IsAuthenticated){
        if(id != null && id > 0){
            var conn = RelationalDatabaseFacadeExtensions.GetDbConnection(_context.Database);
            JsonResult failResponse = null;
            ((System.Data.SqlClient.SqlConnection) conn).Open();
            SqlCommand cmd = new SqlCommand("select ordenservicioid from ordenactivofijo where activofijoid = @ActFijId", (System.Data.SqlClient.SqlConnection) conn);
            cmd.Parameters.Add("@ActFijId", SqlDbType.Int);
            cmd.Parameters["@ActFijId"].Value = id;
            System.Data.SqlClient.SqlDataReader dataReader = cmd.ExecuteReader();
            if(dataReader.HasRows){
              while(dataReader.Read())
              failResponse = Json(new{success=false, data="No es posible eliminarlo, ya que está asignado a una orden de servicio."});
            }
            if(((System.Data.SqlClient.SqlConnection)conn).State == System.Data.ConnectionState.Open)
              ((System.Data.SqlClient.SqlConnection)conn).Close();
            if(failResponse != null)
              return failResponse;

          try{
            var activoFijo = await _context.ActivoFijo.FindAsync(id);
            activoFijo.Estatus = false;
            _context.Update(activoFijo);
            await _context.SaveChangesAsync();
            LoggedUser = this.User.Identity.Name;
            _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Delete", Section = "Activo Fijo", Description = "Activo Fijo no. " + activoFijo.Id + " eliminado." });
            return Json(new{success=true});
          } catch (Exception e) {
            return Json(new{success=false, data=e});
          }
        }
        return NotFound();
      }
      return View();
    }

    private bool ActivoFijoExists(int id) {
      return _context.ActivoFijo.Any(e => e.Id == id);
    }
  }
}
