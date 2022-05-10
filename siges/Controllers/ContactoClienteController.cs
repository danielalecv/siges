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

namespace siges.Controllers{
  [Authorize(Roles = "Supervisor, Ventas, Admin")]
    public class ContactoClienteController : Controller {
        private readonly ApplicationDbContext _context;
        private readonly IBitacoraRepository _bRepo;
        private readonly IContactoCliente _ccRepo;
        private readonly IPersonaRepository _pRepo;
        private readonly IRoatechIdentityUserRepo riuRepo;
        private String LoggedUser;

        public ContactoClienteController(ApplicationDbContext context, IBitacoraRepository bRepo, IContactoCliente ccR, IPersonaRepository pR, IRoatechIdentityUserRepo riuR) {
            _context = context;
            _bRepo = bRepo;
            _ccRepo = ccR;
            _pRepo = pR;
            riuRepo = riuR;
        }

        // GET: ContactoClienteList
        public async Task<IActionResult> Index() {
          List<ContactoCliente> contactoClienteList = await _context.ContactoCliente.Where(r => r.Estatus == true).OrderBy(r => r.Cliente.RazonSocial).ToListAsync();
          contactoClienteList.Sort((a,b) => a.Cliente.RazonSocial.CompareTo(b.Cliente.RazonSocial));
            return View(contactoClienteList);
        }

        // GET: ContactoCliente/Details/5
        public async Task<IActionResult> Details(int? id){
          if(User.Identity.IsAuthenticated){
            if(id > 0 || id == null){
              try{
                if(!_context.Cliente.Any(r => r.Id == (int)id))
                  return RedirectToAction("Index", "Clientes");
                var contactoCliente = _ccRepo.GetByClienteId((int)id);
                if(!contactoCliente.Any()){
                  ContactoCliente cc = new ContactoCliente();
                  cc.Cliente = _context.Cliente.Where(r => r.Id == (int)id).Single();
                  cc.Estatus = true;
                  cc.FechaCreacion = DateTime.Now;
                  cc.FechaModificacion = DateTime.Now;
                  cc.CreadorPor = riuRepo.GetAllInfoByEmail(this.User.Identity.Name).Single();
                  cc.ModificadoPor = riuRepo.GetAllInfoByEmail(this.User.Identity.Name).Single();
                  ViewData["Cliente"] = cc.Cliente.RazonSocial;
                  ViewData["ID"] = cc.Cliente.Id;
                  Console.Write(cc.Contactos.ToList());
                  return View("~/Views/Clientes/Contacts.cshtml", cc.Contactos.ToList());
                } else {
                  ViewData["Cliente"] = contactoCliente.Single().Cliente.RazonSocial;
                  ViewData["ID"] = contactoCliente.Single().Cliente.Id;
                  return View("~/Views/Clientes/Contacts.cshtml", contactoCliente.Single().Contactos.ToList());
                }
              } catch (Exception e){
                return NotFound(e);
              }
            }
            return NotFound();
          }
            return View("~/Views/Clientes/Contacts.cshtml");
        }

                // GET: ContactoCliente/Detail/5
        public async Task<IActionResult> ccDetail( int? Contactoid){
          if(User.Identity.IsAuthenticated){
            if (Contactoid > 0){
              try{
                var p = _context.Persona.Where(r => r.Id == Contactoid);
                if(p == null)
                    return NotFound();
                else
                return Json(new{success = true, data = p.Single()});
              } catch (Exception e){
                return Json(new{success = false, data = e});
              }

            }
            return NotFound();
          }
          return View("~Views/Clientes/Contacts.cshtml");
        }

        

        // POST: ContactoCliente/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContactoClienteDTO ccDTO){
          if(User.Identity.IsAuthenticated){
            if(ccDTO.clienteId > 0 && ccDTO.contactos.Count > 0){
              if (ModelState.IsValid) {
                if(!_context.Cliente.Any(r => r.Id == ccDTO.clienteId))
                  return RedirectToAction("Index", "Clientes");
                var c = _ccRepo.GetByClienteId(ccDTO.clienteId);
                if(c.Any()){
                  ContactoCliente cc = c.Single();
                  int numContactos = cc.Contactos.Count;
                  foreach(Persona p in ccDTO.contactos){
                    if(!_context.Persona.Any(r => r.Email == p.Email)){
                      p.Estatus = true;
                      p.FaceApiId = "SINFACEAPIID";
                      cc.Contactos.Add(p);
                    }
                  }
                  if(cc.Contactos.Count > numContactos){
                    cc.Opcional1 = ccDTO.opcional1;
                    cc.Opcional2 = ccDTO.opcional2;
                    cc.ModificadoPor = riuRepo.GetAllInfoByEmail(this.User.Identity.Name).Single();
                    cc.FechaModificacion = DateTime.Now;
                    _ccRepo.Update(cc);
                    LoggedUser = this.User.Identity.Name;
                    _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Update", Section = "ContactoCliente", Description = "ContactoCliente No. " + cc.Id + " actualizado." });
                    return Json(new{success=true, data=cc});
                  }
                } else {
                  try {
                    ContactoCliente cc = new ContactoCliente();
                    cc.Cliente = _context.Cliente.Where(r => r.Id == ccDTO.clienteId).Single();
                    cc.Opcional1 = ccDTO.opcional1;
                    cc.Opcional2 = ccDTO.opcional2;
                    cc.Estatus = true;
                    cc.FechaCreacion = DateTime.Now;
                    cc.FechaModificacion = DateTime.Now;
                    cc.CreadorPor = riuRepo.GetAllInfoByEmail(this.User.Identity.Name).Single();
                    cc.ModificadoPor = riuRepo.GetAllInfoByEmail(this.User.Identity.Name).Single();
                    foreach(Persona p in ccDTO.contactos){
                      p.Estatus = true;
                      p.FaceApiId = "SINFACEAPIID";
                      cc.Contactos.Add(p);
                    }
                    var entityEntry = _context.ContactoCliente.Add(cc);
                    await _context.SaveChangesAsync();
                    LoggedUser = this.User.Identity.Name;
                    _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Insert", Section = "ContactoCliente", Description = "ContactoCliente No. " + entityEntry.Entity.Id + " agregado." });
                    return Json(new{success=true, data=cc});
                  } catch ( Exception e) {
                    return new JsonResult(e);
                  }
                }
              } else {
                return NotFound();
              }
            }
            return new JsonResult(false);
          }
          return View("~/Views/ContactoCliente/Index.cshtml");
        }

        // POST: ContactoCliente/Update/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ContactoClienteDTO ccDTO) { 
          if(User.Identity.IsAuthenticated){
            if (id > 0){
              if (ModelState.IsValid) {
                var p = _context.Persona.Where(r => r.Id == id);
                p.Single().Nombre= ccDTO.contactos[0].Nombre;
                p.Single().Paterno= ccDTO.contactos[0].Paterno;
                p.Single().Materno= ccDTO.contactos[0].Materno;
                p.Single().Email= ccDTO.contactos[0].Email;
                p.Single().Telefono= ccDTO.contactos[0].Telefono;
                p.Single().TelefonoContacto= ccDTO.contactos[0].TelefonoContacto;
                p.Single().Opcional1= ccDTO.contactos[0].Opcional1;
                p.Single().Opcional2= ccDTO.contactos[0].Opcional2;
                try {
                  _context.Update(p.Single());
                  await _context.SaveChangesAsync();
                  LoggedUser = this.User.Identity.Name;
                  _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Update", Section = "ContactoCliente", Description = "Persona no. " + p.Single().Id + " actualizado." });
                } catch (DbUpdateConcurrencyException e) {
                   return Json(new{success=false, data=e});
                   
                }
                return new JsonResult(true);
              }
            }
            return new JsonResult(false);
          }
          return RedirectToAction(nameof(Index));
        }

        // POST: ContactoCliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) { 
          if(User.Identity.IsAuthenticated){
            if (id > 0){
              try{
                var contactoCliente = _context.Persona.Where(r => r.Id == id);
                contactoCliente.Single().Estatus = false;
                _context.Update(contactoCliente.Single());
                _context.Persona.Remove(contactoCliente.Single());
                await _context.SaveChangesAsync();
                LoggedUser = this.User.Identity.Name;
                _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Delete", Section =  "ContactoCliente", Description = "Persona no. " + contactoCliente.Single().Id + " eliminado." });
                return new JsonResult(true);
              } catch (Exception e) {
                return new JsonResult(e);
              }
            }
          }
          return RedirectToAction(nameof(Index));
        }

    }
}
