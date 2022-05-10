using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using siges.Data;
using siges.Models;
using siges.Repository;

namespace siges.Controllers
{
  [Authorize(Roles = "Supervisor, Ventas, SuperUser")]
  public class ContratosController : Controller
  {
    private readonly ApplicationDbContext _context;
    private readonly IBitacoraRepository _bRepo;
    private String LoggedUser;

    public ContratosController(ApplicationDbContext context, IBitacoraRepository bRepo)
    {
      _context = context;
      _bRepo = bRepo;
    }

    // GET: Contratos
    public async Task<IActionResult> Index()
    {
      var applicationDbContext = _context.Contrato.Include(c => c.Cliente).Include(c => c.Servicio).Where(r => r.Estatus == true).OrderBy(c => c.Cliente.RazonSocial);

      List<Cliente> cL = _context.Cliente.Where(r => r.Estatus == true).OrderBy(r => r.RazonSocial).ToList();
      cL.Sort((a,b) => a.RazonSocial.CompareTo(b.RazonSocial));
      ViewData["Clientes"] = cL;

      List<Servicio> sL = _context.Servicio.Where(r => r.Estatus == true).OrderBy(r => r.Nombre).ToList();
      sL.Sort((a,b) => a.Nombre.CompareTo(b.Nombre));
      ViewData["Servicios"] = sL;

      return View(await applicationDbContext.ToListAsync());
    }

    // GET: Contratos/Details/5
    public async Task<IActionResult> Details(int? id) {
      if(User.Identity.IsAuthenticated){
        if (id != null && id > 0){
          try{
            var contrato = await _context.Contrato.Include(c => c.Cliente).Include(c => c.Servicio).FirstOrDefaultAsync(m => m.Id == id);
            if (contrato == null) {
              return NotFound();
            }
            return Json(new{success=true, data=contrato});
          } catch(Exception e){
            return Json(new{success=false,data=e});
          }
        }
        return NotFound();
      }
      return View();
    }

    // POST: Contratos/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nombre,Tipo,Estatus,Opcional1,Opcional2,ClienteId,ServicioId")] Contrato contrato) {
      if(User.Identity.IsAuthenticated){
        if(!String.IsNullOrEmpty(contrato.Nombre) && !String.IsNullOrEmpty(contrato.Tipo)){
          if (ModelState.IsValid) {
            try{
              contrato.Estatus = true;
              _context.Add(contrato);
              await _context.SaveChangesAsync();
              LoggedUser = this.User.Identity.Name;
              _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Insert", Section = "Contrato", Description = "Contrato no. " + contrato.Id + " agregado." });
              return Json(new{success=true});
            } catch(Exception e){
              return Json(new{success=false, data=e});
            }
          }
          return Json(new{success=false});
        }
        return NotFound();
      }
      return View();
    }

    // POST: Contratos/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Tipo,Estatus,Opcional1,Opcional2,ClienteId,ServicioId")] Contrato contrato) {
      if(User.Identity.IsAuthenticated){
        if (id == contrato.Id && !String.IsNullOrEmpty(contrato.Nombre) && !String.IsNullOrEmpty(contrato.Tipo)){
          if (ModelState.IsValid) {
            try {
              contrato.Estatus = true;
              _context.Update(contrato);
              await _context.SaveChangesAsync();
              LoggedUser = this.User.Identity.Name;
              _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Update", Section = "Contrato", Description = "Contrato no. " + contrato.Id + " actualizado." });
              return Json(new{success=true, data=contrato});
            } catch (DbUpdateConcurrencyException e) {
              if (!ContratoExists(contrato.Id)) {
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

    // POST: Contratos/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id){
      if(User.Identity.IsAuthenticated){
        if(id != null && id > 0){
          try{
            var contrato = await _context.Contrato.FindAsync(id);
            contrato.Estatus = false;
            _context.Update(contrato);
            await _context.SaveChangesAsync();
            LoggedUser = this.User.Identity.Name;
            _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Delete", Section = "Contrato", Description = "Contrato no. " + contrato.Id + " eliminado." });
            return Json(new{success=true});
          } catch (Exception e){
            return Json(new{success=false, data=e});
          }
        }
        return NotFound();
      }
      return View();
    }

    private bool ContratoExists(int id){
      return _context.Contrato.Any(e => e.Id == id);
    }
  }
}
