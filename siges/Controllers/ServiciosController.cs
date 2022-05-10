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

namespace siges.Controllers {
  [Authorize(Roles = "Supervisor, SuperUser")]
  public class ServiciosController : Controller{
    private readonly ApplicationDbContext _context;
    private readonly IBitacoraRepository _bRepo;
    private String LoggedUser;

    public ServiciosController(ApplicationDbContext context, IBitacoraRepository bRepo){
      _context = context;
      _bRepo = bRepo;
    }

    // GET: Servicios
    public async Task<IActionResult> Index(){
      ViewData["LineasNegocio"] = _context.LineaNegocio.Where(r => r.Estatus == true).OrderBy(r => r.Nombre).ToList();
      return View(await _context.Servicio.Where(r => r.Estatus == true).Include(r => r.LineaNegocio).OrderBy(r => r.Nombre).ToListAsync());
    }

    // GET: Servicios/Details/5
    public IActionResult Details(int? id){
      if(User.Identity.IsAuthenticated){
        if (id != null && id > 0){
          try{
            var servicio = _context.Servicio.Where(r => r.Id == id).Include(r => r.LineaNegocio).Single();
            if (servicio == null){
              return NotFound();
            }
            return Json(new{success=true, data=servicio});
          } catch (Exception e) {
            return Json(new{success=false, data=e});
          }
        }
        return NotFound();
      }
      return View();
    }

    // POST: Servicios/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,Estatus,Opcional1,Opcional2,LineaNegocioId")] Servicio servicio){
      if(User.Identity.IsAuthenticated){
        if(!String.IsNullOrEmpty(servicio.Nombre) && !String.IsNullOrEmpty(servicio.Descripcion)){
          if (ModelState.IsValid){
            try {
            servicio.Estatus = true;
            servicio.LineaNegocio = _context.LineaNegocio.Where(r => r.Id == servicio.LineaNegocioId).Single();
            _context.Add(servicio);
            await _context.SaveChangesAsync();
            LoggedUser = this.User.Identity.Name;
            _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Insert", Section = "Servicio", Description = "Servicio no. " + servicio.Id + " agregado." });
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

    // POST: Servicios/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,Estatus,Opcional1,Opcional2,LineaNegocioId")] Servicio servicio){
      if(User.Identity.IsAuthenticated){
        if (id == servicio.Id && !String.IsNullOrEmpty(servicio.Nombre) && !String.IsNullOrEmpty(servicio.Descripcion)){
          if (ModelState.IsValid){
            try{
              servicio.LineaNegocio = _context.LineaNegocio.Where(r => r.Id == servicio.LineaNegocioId).Single();
              servicio.Estatus = true;
              _context.Update(servicio);
              await _context.SaveChangesAsync();
              LoggedUser = this.User.Identity.Name;
              _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Update", Section = "Servicio", Description = "Servicio no. " + servicio.Id + " actualizado." });
              return Json(new{success=true});
            } catch (DbUpdateConcurrencyException e) {
              if (!ServicioExists(servicio.Id)) {
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

    // POST: Servicios/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id){
      if(User.Identity.IsAuthenticated){
        if(id > 0){
          try {
            var servicio = _context.Servicio.Where(r => r.Id == id).Include(r => r.LineaNegocio).Single();
            servicio.Estatus = false;
            _context.Update(servicio);
            //_context.Servicio.Remove(servicio);
            await _context.SaveChangesAsync();
            LoggedUser = this.User.Identity.Name;
            _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Delete", Section = "Servicio", Description = "Servicio no. " + servicio.Id + " eliminado." });
            return Json(new{success=true});
          } catch (Exception e) {
            return Json(new{success=false, data=e});
          }
        }
        return NotFound();
      }
      return View();
    }

    private bool ServicioExists(int id){
      return _context.Servicio.Any(e => e.Id == id);
    }
  }
}
