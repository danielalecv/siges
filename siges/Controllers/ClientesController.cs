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
    public class ClientesController : Controller {
        private readonly ApplicationDbContext _context;
        private readonly IBitacoraRepository _bRepo;
        private readonly IPersonaRepository _personaRepo;
        private String LoggedUser;

        public ClientesController(ApplicationDbContext context, IBitacoraRepository bRepo, IPersonaRepository personaRepo)
        {
            _context = context;
            _bRepo = bRepo;
            _personaRepo = personaRepo;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cliente.Where(r => r.Estatus == true).OrderBy(r => r.RazonSocial).ToListAsync());
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id){
          if(User.Identity.IsAuthenticated){
            if(id > 0 || id == null){
              try{
                var cliente = await _context.Cliente.FirstOrDefaultAsync(m => m.Id == id);
                if(cliente == null)
                  return NotFound();
                else
                  return Json(new{success = true, data = cliente});
              } catch (Exception e){
                return Json(new{success = false, data = e});
              }
            }
            return NotFound();
          }
            return View("~/Views/Clientes/Index.cshtml");
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RazonSocial,RFC,Telefono,Opcional1,Opcional2,Estatus")] Cliente cliente){
          if(User.Identity.IsAuthenticated){
            if(!String.IsNullOrEmpty(cliente.RazonSocial) && !String.IsNullOrEmpty(cliente.RFC)){
              if (ModelState.IsValid) {
                try {
                  cliente.Estatus = true;
                  _context.Add(cliente);
                  await _context.SaveChangesAsync();
                  LoggedUser = this.User.Identity.Name;
                  _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Insert", Section = "Cliente", Description = "Cliente no. " + cliente.Id + " agregado." });
                  return Json(new{success=true, data=cliente});
                } catch ( Exception e) {
                  return new JsonResult(e);
                }
              } else {
                return NotFound();
              }
            }
            return new JsonResult(false);
          }
          return View("~/Views/Clientes/Index.cshtml");
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RazonSocial,RFC,Telefono,Opcional1,Opcional2,Estatus")] Cliente cliente){
          if(User.Identity.IsAuthenticated){
            if (!String.IsNullOrEmpty(cliente.RazonSocial) && cliente.RazonSocial != "null" && !String.IsNullOrEmpty(cliente.RFC) && cliente.RFC != "null"){
              if (ModelState.IsValid) {
                try {
                  cliente.Estatus = true;
                  _context.Update(cliente);
                  await _context.SaveChangesAsync();
                  LoggedUser = this.User.Identity.Name;
                  _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Update", Section = "Cliente", Description = "Cliente no. " + cliente.Id + " actualizado." });
                } catch (DbUpdateConcurrencyException e) {
                  if (!ClienteExists(cliente.Id)){
                    return NotFound();
                  } else {
                    return new JsonResult(e);
                  }
                }
                return new JsonResult(true);
              }
            }
            return new JsonResult(false);
          }
          return RedirectToAction(nameof(Index));
        }

        // GET: Clientes/Delete/5
        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _context.Cliente
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
          Console.WriteLine("\n\n\tInicio Delete: " + id + "\n\n");
          if(User.Identity.IsAuthenticated){
            if (id > 0){
              try{
                var cliente = await _context.Cliente.FindAsync(id);
                cliente.Estatus = false;
                _context.Update(cliente);
                var RcontactoCliente = await _context.ContactoCliente.Where(r => r.Cliente.Id == id).FirstOrDefaultAsync();
                        if (RcontactoCliente != null)
                        {
                            Console.WriteLine("\n\n\tcontactoCli encontrado: " + RcontactoCliente.Id);
                            _personaRepo.DeleteByContactoClienteId(RcontactoCliente.Id);
                            Console.WriteLine("elimino personas");
                            _context.Cliente.Remove(cliente);
                            RcontactoCliente.Estatus = false;
                            _context.Update(RcontactoCliente);
                            _context.ContactoCliente.Remove(RcontactoCliente);
                            await _context.SaveChangesAsync();
                            LoggedUser = this.User.Identity.Name;
                            _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Delete", Section = "Cliente", Description = "Cliente no. " + cliente.Id + " eliminado." });
                            return new JsonResult(true);
                        }
                        else
                        {
                            Console.WriteLine("contactoCli encontrado: " + RcontactoCliente);
                            _context.Cliente.Remove(cliente);
                            await _context.SaveChangesAsync();
                            LoggedUser = this.User.Identity.Name;
                            _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Delete", Section = "Cliente", Description = "Cliente no. " + cliente.Id + " eliminado." });
                            return new JsonResult(true);

                        }
              } catch (Exception e) {
                return new JsonResult(e);
              }
            }
          }
          return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(int id)
        {
            return _context.Cliente.Any(e => e.Id == id);
        }
    }
}
