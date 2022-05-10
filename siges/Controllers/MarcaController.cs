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

namespace siges.Controllers
{
  [Authorize(Roles = "Supervisor")]
    public class MarcaController : Controller {
        private readonly ApplicationDbContext _context;
        private readonly IBitacoraRepository _bRepo;
        private readonly IMarca _mRepo;
        private readonly IRoatechIdentityUserRepo riuRepo;
        private String LoggedUser;

        public MarcaController(ApplicationDbContext context, IBitacoraRepository bRepo, IMarca mR, IRoatechIdentityUserRepo ruR) {
            _context = context;
            _bRepo = bRepo;
            _mRepo = mR;
            riuRepo = ruR;
        }

        // GET: Marcas
        public async Task<IActionResult> Index()
        {
          List<Marca> marcas = await _context.Marca.Where(r => r.Estatus == true).OrderBy(r => r.Descripcion).ToListAsync();
          marcas.Sort((a,b) => a.Descripcion.CompareTo(b.Descripcion));
            return View(marcas);
        }

        // GET: Marcas/Details/5
        public async Task<IActionResult> Details(int? id){
          if(User.Identity.IsAuthenticated){
            if(id > 0 || id == null){
              try{
                var marca = await _context.Marca.FirstOrDefaultAsync(m => m.Id == id);
                if(marca == null)
                  return NotFound();
                else
                  return Json(new{success = true, data = marca});
              } catch (Exception e){
                return Json(new{success = false, data = e});
              }
            }
            return NotFound();
          }
            return View("~/Views/Marca/Index.cshtml");
        }

        // POST: Marca/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MarcaDTO mDTO){
          if(User.Identity.IsAuthenticated){
            if(!String.IsNullOrEmpty(mDTO.Descripcion)){
              if (ModelState.IsValid) {
                if(_context.Marca.Any(r => r.Descripcion == mDTO.Descripcion))
                  return Json(new{success=false, data="Ya existe " + mDTO.Descripcion});
                try {
                  Marca m = new Marca();
                  m.Descripcion = mDTO.Descripcion;
                  m.Opcional1 = mDTO.Opcional1;
                  m.Opcional2 = mDTO.Opcional2;
                  m.Estatus = true;
                  m.FechaCreacion = DateTime.Now;
                  m.FechaModificacion = DateTime.Now;
                  m.CreadorPor = riuRepo.GetAllInfoByEmail(this.User.Identity.Name).Single();
                  m.ModificadoPor = riuRepo.GetAllInfoByEmail(this.User.Identity.Name).Single();
                  var entityEntry = _context.Add(m);
                  await _context.SaveChangesAsync();
                  LoggedUser = this.User.Identity.Name;
                  _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Insert", Section = "Marca", Description = "Marca No. " + entityEntry.Entity.Id + " agregado." });
                  return Json(new{success=true, data=m});
                } catch ( Exception e) {
                  return new JsonResult(e);
                }
              } else {
                return NotFound();
              }
            }
            return new JsonResult(false);
          }
          return View("~/Views/Marca/Index.cshtml");
        }

        // POST: Marca/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MarcaDTO mDTO){
          if(User.Identity.IsAuthenticated){
            if (!String.IsNullOrEmpty(mDTO.Descripcion) && id > 0){
              if (ModelState.IsValid) {
                try {
                  Marca m = _mRepo.GetById(id);
                  m.Descripcion = mDTO.Descripcion;
                  m.Opcional1 = mDTO.Opcional1;
                  m.Opcional2 = mDTO.Opcional2;
                  m.ModificadoPor = riuRepo.GetAllInfoByEmail(this.User.Identity.Name).Single();
                  m.FechaModificacion = DateTime.Now;
                  _context.Update(m);
                  await _context.SaveChangesAsync();
                  LoggedUser = this.User.Identity.Name;
                  _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Update", Section = "Marca", Description = "Marca no. " + m.Id + " actualizado." });
                } catch (DbUpdateConcurrencyException e) {
                  if (!MarcaExists(id)){
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

        // POST: Marca/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) { 
          if(User.Identity.IsAuthenticated){
            if (id > 0){
              try{
                var m = await _context.Marca.FindAsync(id);
                m.Estatus = false;
                _context.Update(m);
                _context.Marca.Remove(m);
                await _context.SaveChangesAsync();
                LoggedUser = this.User.Identity.Name;
                _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Delete", Section = "Marca", Description = "Marca no. " + m.Id + " eliminado." });
                return Json(new{success=true});
              } catch (Exception e) {
                return Json(new{success=false, data=e});
              }
            }
          }
          return RedirectToAction(nameof(Index));
        }

        private bool MarcaExists(int id)
        {
            return _context.Marca.Any(e => e.Id == id);
        }
    }
}
