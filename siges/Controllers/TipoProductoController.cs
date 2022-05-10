using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using siges.Data;
using siges.DTO;
using siges.Models;
using siges.Repository;

namespace siges.Controllers {
  [Authorize(Roles = "Supervisor")]
    public class TipoProductoController : Controller {
        private readonly ApplicationDbContext _context;
        private readonly IBitacoraRepository _bRepo;
        private readonly ITipoProducto _tpRepo;
        private readonly IRoatechIdentityUserRepo riuRepo;
        private String LoggedUser;

        public TipoProductoController(ApplicationDbContext context, IBitacoraRepository bRepo, ITipoProducto tpR, IRoatechIdentityUserRepo ruR) {
            _context = context;
            _bRepo = bRepo;
            _tpRepo = tpR;
            riuRepo = ruR;
        }

        // GET: Marcas
        public async Task<IActionResult> Index()
        {
          List<TipoProducto> tipoProductos = await _context.TipoProducto.Where(r => r.Estatus == true).OrderBy(r => r.Descripcion).ToListAsync();
          tipoProductos.Sort((a,b) => a.Descripcion.CompareTo(b.Descripcion));
            return View(tipoProductos);
        }

        // GET: TipoProducto/Details/5
        public async Task<IActionResult> Details(int? id){
          if(User.Identity.IsAuthenticated){
            if(id > 0 || id == null){
              try{
                var tipoProducto = await _context.TipoProducto.FirstOrDefaultAsync(m => m.Id == id);
                if(tipoProducto == null)
                  return NotFound();
                else
                  return Json(new{success = true, data = tipoProducto});
              } catch (Exception e){
                return Json(new{success = false, data = e});
              }
            }
            return NotFound();
          }
            return View("~/Views/TipoProducto/Index.cshtml");
        }

        // POST: TipoProducto/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TipoProductoDTO tpDTO){
          if(User.Identity.IsAuthenticated){
            if(!String.IsNullOrEmpty(tpDTO.Descripcion)){
              if (ModelState.IsValid) {
                if(_context.TipoProducto.Any(r => r.Descripcion == tpDTO.Descripcion))
                  return Json(new{success=false, data="Ya existe "+tpDTO.Descripcion});
                try {
                  TipoProducto tp = new TipoProducto();
                  tp.Descripcion = tpDTO.Descripcion;
                  tp.Opcional1 = tpDTO.Opcional1;
                  tp.Opcional2 = tpDTO.Opcional2;
                  tp.Estatus = true;
                  tp.FechaCreacion = DateTime.Now;
                  tp.FechaModificacion = DateTime.Now;
                  tp.CreadorPor = riuRepo.GetAllInfoByEmail(this.User.Identity.Name).Single();
                  tp.ModificadoPor = riuRepo.GetAllInfoByEmail(this.User.Identity.Name).Single();
                  var entityEntry = _context.Add(tp);
                  await _context.SaveChangesAsync();
                  LoggedUser = this.User.Identity.Name;
                  _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Insert", Section = "TipoProducto", Description = "TipoProducto No. " + entityEntry.Entity.Id + " agregado." });
                  return Json(new{success=true, data=tp});
                } catch ( Exception e) {
                  return new JsonResult(e);
                }
              } else {
                return NotFound();
              }
            }
            return new JsonResult(false);
          }
          return View("~/Views/TipoProducto/Index.cshtml");
        }

        // POST: TipoProducto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TipoProductoDTO tpDTO){
          if(User.Identity.IsAuthenticated){
            if (!String.IsNullOrEmpty(tpDTO.Descripcion) && id > 0){
              if (ModelState.IsValid) {
                try {
                  TipoProducto tp = _tpRepo.GetById(id);
                  tp.Descripcion = tpDTO.Descripcion;
                  tp.Opcional1 = tpDTO.Opcional1;
                  tp.Opcional2 = tpDTO.Opcional2;
                  tp.ModificadoPor = riuRepo.GetAllInfoByEmail(this.User.Identity.Name).Single();
                  tp.FechaModificacion = DateTime.Now;
                  _context.Update(tp);
                  await _context.SaveChangesAsync();
                  LoggedUser = this.User.Identity.Name;
                  _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Update", Section = "TipoProducto", Description = "TipoProducto no. " + tp.Id + " actualizado." });
                } catch (DbUpdateConcurrencyException e) {
                  if (!TipoProductoExists(id)){
                    return NotFound();
                  } else {
                    return new JsonResult(e);
                  }
                }
                return Json(new{success=true});
              }
            }
            return new JsonResult(false);
          }
          return RedirectToAction(nameof(Index));
        }

        // POST: TipoProducto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) { 
          if(User.Identity.IsAuthenticated){
            if (id > 0){
              try{
                var tp = await _context.TipoProducto.FindAsync(id);
                tp.Estatus = false;
                _context.Update(tp);
                _context.TipoProducto.Remove(tp);
                await _context.SaveChangesAsync();
                LoggedUser = this.User.Identity.Name;
                _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Delete", Section = "TipoProducto", Description = "TipoProducto no. " + tp.Id + " eliminado." });
                return Json(new{success=true});
              } catch (Exception e) {
                return Json(new{success=false, data=e});
              }
            }
          }
          return RedirectToAction(nameof(Index));
        }

        private bool TipoProductoExists(int id)
        {
            return _context.TipoProducto.Any(e => e.Id == id);
        }
    }
}
