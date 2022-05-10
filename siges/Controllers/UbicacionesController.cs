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
  [Authorize(Roles = "Supervisor, Ventas, Admin")]
  public class UbicacionesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUbicacionRepository _uRepo;
        private readonly IBitacoraRepository _bRepo;
        private String LoggedUser;

        public UbicacionesController(ApplicationDbContext context, IBitacoraRepository bRepo, IUbicacionRepository uRepo)
        {
            _context = context;
            _bRepo = bRepo;
            _uRepo = uRepo;
        }

        // GET: Ubicaciones
        public async Task<IActionResult> Index() {
            var applicationDbContext = _context.Ubicacion.Include(u => u.Cliente).Where(r => r.Estatus == true).OrderBy(r => r.Cliente.RazonSocial);
            List<Cliente> cL = _context.Cliente.Where(r => r.Estatus == true).ToList();
            cL.Sort((a,b) => a.RazonSocial.CompareTo(b.RazonSocial));
            ViewData["ClienteId"] = cL;
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Ubicaciones/Details/5
        public async Task<IActionResult> Details(int? id){
          if(User.Identity.IsAuthenticated){
            if (id != null && id > 0) {
              try {
                var ubicacion = await _context.Ubicacion.Include(u => u.Cliente).FirstOrDefaultAsync(m => m.Id == id);
                if (ubicacion == null)
                  return NotFound();
                else 
                  return Json(new{success = true, data = ubicacion});
              } catch(Exception e) {
                  return Json(new{success = false, data = e});
              }
            }
            return NotFound();
          }
          return View();
        }

        // POST: Ubicaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Direccion,Distancia,Contacto,ContactoTelefono,ContactoEmail,Latitude,Longitude,ContactoOpcional,ContactoOpcionalTelefono,ContactoOpcionalEmail,Estatus,ClienteId")] Ubicacion ubicacion){
          if(User.Identity.IsAuthenticated){
            if(String.IsNullOrEmpty(ubicacion.Nombre) || String.IsNullOrEmpty(ubicacion.Direccion) )
            { return Json(new {success=false, message= "Algún campo obligatorio está vacío" }); }
            if(_uRepo.Exist(ubicacion.ClienteId, ubicacion.Nombre)){ return Json(new {success=false, message="La ubicación ya existe"}); }
            try{
              ubicacion.Estatus = true;
              _context.Add(ubicacion);
              await _context.SaveChangesAsync();
              LoggedUser = this.User.Identity.Name;
              _bRepo.Insert(new Bitacora() { 
                UserId = LoggedUser, EventDate = DateTime.Now, Event = "Insert", Section = "Ubicación", Description = "Ubicación no. " + ubicacion.Id + " agregada." });
            }
            catch(Exception e){
              return Json(new{success=false, error=e, message="Ocurrio un error"});
            }
            return Json(new {success=true});
          }
          return View();
        }

        // POST: Ubicaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Direccion,Distancia,Contacto,ContactoTelefono,ContactoEmail,Latitude,Longitude,ContactoOpcional,ContactoOpcionalTelefono,ContactoOpcionalEmail,Estatus,ClienteId")] Ubicacion ubicacion) {
          if(User.Identity.IsAuthenticated){
            if (id == ubicacion.Id && !String.IsNullOrEmpty(ubicacion.Nombre) && !String.IsNullOrEmpty(ubicacion.Direccion)) {
              if (ModelState.IsValid) {
                try {
                  ubicacion.Estatus = true;
                  _context.Update(ubicacion);
                  await _context.SaveChangesAsync();
                  LoggedUser = this.User.Identity.Name;
                  _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Update", Section = "Ubicación", Description = "Ubicación no. " + ubicacion.Id + " actualizada." });
                  return Json(new{success = true});
                } catch (DbUpdateConcurrencyException e) {
                  if (!UbicacionExists(ubicacion.Id)) {
                    return NotFound();
                  } else {
                    return Json(new{success = false, data = e});
                  }
                }
              }
            }
            return NotFound();
          }
          return View();
        }

        // POST: Ubicaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
          if(User.Identity.IsAuthenticated) {
            if ( id > 0) {
              try{
                var ubicacion = await _context.Ubicacion.FindAsync(id);
                ubicacion.Estatus = false;
                _context.Update(ubicacion);
                await _context.SaveChangesAsync();
                LoggedUser = this.User.Identity.Name;
                _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Delete", Section = "Ubicación", Description = "Ubicación no. " + ubicacion.Id + " eliminada." });
                return new JsonResult(true);
              } catch (Exception e) {
                return new JsonResult(e);
              }
            }
            return NotFound();
          }
          return View();
        }

        private bool UbicacionExists(int id)
        {
            return _context.Ubicacion.Any(e => e.Id == id);
        }
    }
}
