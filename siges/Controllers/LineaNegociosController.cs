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
  public class LineaNegociosController : Controller {
    private readonly ApplicationDbContext _context;
    private readonly IBitacoraRepository _bRepo;
    private String LoggedUser;

    public LineaNegociosController(ApplicationDbContext context, IBitacoraRepository bRepo) {
      _context = context;
      _bRepo = bRepo;
    }

    // GET: LineaNegocios
    public async Task<IActionResult> Index() {
      return View(await _context.LineaNegocio.Where(r => r.Estatus == true).OrderBy(r => r.Nombre).ToListAsync());
    }

    // GET: LineaNegocios/Details/5
    public IActionResult Details(int? id) {
      if(User.Identity.IsAuthenticated){
        if (id != null && id > 0) {
          try {
            var lineaNegocio = _context.LineaNegocio.Where(m => m.Id == id).Single();
            if (lineaNegocio == null) {
              return NotFound();
            }
            return Json(new{success=true, data=lineaNegocio});
          } catch (Exception e) {
            return Json(new{success=false, data=e});
          }
        }
        return NotFound();
      }
      return View();
    }

    // POST: LineaNegocios/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nombre,Estatus,Opcional1,Opcional2")] LineaNegocio lineaNegocio) {
      if(User.Identity.IsAuthenticated){
        if(!String.IsNullOrEmpty(lineaNegocio.Nombre)){
          if (ModelState.IsValid) {
            try {
              lineaNegocio.Estatus = true;
              _context.Add(lineaNegocio);
              await _context.SaveChangesAsync();
              LoggedUser = this.User.Identity.Name;
              _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Insert", Section = "Linea de Negocio", Description = "Linea de negocio no. " + lineaNegocio.Id + " agregada." });
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

    // POST: LineaNegocios/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Estatus,Opcional1,Opcional2")] LineaNegocio lineaNegocio) {
      if(User.Identity.IsAuthenticated){
        if (id == lineaNegocio.Id && !String.IsNullOrEmpty(lineaNegocio.Nombre)) {
          if (ModelState.IsValid) {
            try {
              lineaNegocio.Estatus = true;
              _context.Update(lineaNegocio);
              await _context.SaveChangesAsync();
              LoggedUser = this.User.Identity.Name;
              _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Update", Section = "Linea de Negocio", Description = "Linea de negocio no. " + lineaNegocio.Id + " actualizada." });
              return Json(new{success=true});
            } catch (DbUpdateConcurrencyException e) {
              if (!LineaNegocioExists(lineaNegocio.Id)) {
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

    // POST: LineaNegocios/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id) {
      if(User.Identity.IsAuthenticated){
        if(id != null && id > 0) {
          try {
            var lineaNegocio = await _context.LineaNegocio.FindAsync(id);
            lineaNegocio.Estatus = false;
            _context.Update(lineaNegocio);
            await _context.SaveChangesAsync();
            LoggedUser = this.User.Identity.Name;
            _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Delete", Section = "Linea de Negocio", Description = "Linea de negocio no. " + lineaNegocio.Id + " eliminada." });
            return Json(new{success=true});
          } catch (Exception e) {
            return Json(new{success=false, data=e});
          }
        }
        return NotFound();
      }
      return View();
    }

    private bool LineaNegocioExists(int id) {
      return _context.LineaNegocio.Any(e => e.Id == id);
    }
  }
}
