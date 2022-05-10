using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using siges.Data;
using siges.DTO;
using siges.Models;
using siges.Repository;

namespace siges.Controllers {
  public class PersonasController : Controller {
    private readonly ApplicationDbContext _context;
    private readonly IBitacoraRepository _bRepo;
    private readonly IPersonaRepository _pRepo;
    private readonly Settings settings;
    private String LoggedUser;

    public PersonasController(ApplicationDbContext context, IBitacoraRepository bRepo, IPersonaRepository pRepo, ISettingsRepository _set) {
      _context = context;
      _bRepo = bRepo;
      _pRepo = pRepo;
      settings = _set.GetByVersion("DAMSA");
    }

    // GET: Personas
    [Authorize(Roles = "Supervisor, SuperUser")]
    public async Task<IActionResult> Index() {
      if(User.Identity.IsAuthenticated){
        return View(await _context.Persona.Where(r => r.Estatus == true).ToListAsync());
      }
      return View();
    }

    public IActionResult IndexListDP(){
      if(User.Identity.IsAuthenticated){
        ViewData["persona"] = _pRepo.GetAll(false).ToList();
      }
      return View("~/Views/Personas/PersonsDeleted.cshtml");
    }
    [HttpPost]
    public IActionResult UpdateDP(int pid){
      if(User.Identity.IsAuthenticated){
        if(pid != null && pid > 0){
          try{
            Persona nP = _pRepo.GetById(pid);
            nP.Estatus = true;
            _pRepo.Update(nP);
            LoggedUser = this.User.Identity.Name;
            _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Update", Section = "Persona", Description = "Persona no. " + nP.Id + " actualizada." });
            return Json(new{success=true});
          } catch (Exception e) {
            return Json(new{success=false, data=e});
          }
        }
        return NotFound();
      }
      return View();
    }

    // GET: Personas/Details/5
    [Authorize(Roles = "Supervisor, SuperUser")]
    public async Task<IActionResult> Details(int? id) {
      if(User.Identity.IsAuthenticated){
        if (id != null && id > 0) {
          try{
            var persona = await _context.Persona.FirstOrDefaultAsync(m => m.Id == id);
            if (persona == null) {
              return NotFound();
            }
            return Json(new{success=true, data=persona});
          } catch (Exception e){
            return Json(new{success=false, data=e});
          }
        }
        return NotFound();
      }
      return View();
    }

    // POST: Personas/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> Create(IFormFile fotografia, [Bind("Id,Nombre,Paterno,Materno,RFC,CURP,Email,ClaveEmpleado,Telefono,TelefonoContacto,Estatus,Puesto,Categoria,Direccion,EntidadFederativa,Municipio,Sueldo,Opcional1,Opcional2,Adscripcion,FaceApiId,FaceApiCount")] PersonaDTO persona) {
      // if(User.Identity.IsAuthenticated){
      //   if(!String.IsNullOrEmpty(persona.Nombre) && !String.IsNullOrEmpty(persona.Paterno) && !String.IsNullOrEmpty(persona.Materno) && !String.IsNullOrEmpty(persona.RFC) && !String.IsNullOrEmpty(persona.Email) && !String.IsNullOrEmpty(persona.Puesto) && !String.IsNullOrEmpty(persona.Categoria)){
      //     if (ModelState.IsValid) {
      try {
        Persona p = new Persona();
        p.Nombre = persona.Nombre;
        p.Paterno = persona.Paterno;
        p.Materno = persona.Materno;
        p.RFC = persona.RFC;
        p.CURP = persona.CURP;
        p.Email = persona.Email;
        p.ClaveEmpleado = persona.ClaveEmpleado;
        p.Telefono = persona.Telefono;
        p.TelefonoContacto = persona.TelefonoContacto;
        p.Categoria = persona.Categoria;
        p.Puesto = persona.Puesto;
        p.Direccion = persona.Direccion;
        p.EntidadFederativa = persona.EntidadFederativa;
        p.Municipio = persona.Municipio;
        p.Sueldo = persona.Sueldo;
        p.Adscripcion = persona.Adscripcion;
        p.Opcional1 = persona.Opcional1;
        p.Opcional2 = persona.Opcional2;
        p.Estatus = true;
        p.FaceApiId = "SINFACEAPIID";
        if(settings.FaceApiUso){
          p.FaceApiCount = persona.FaceApiCount;
        }
        if(fotografia != null && fotografia.Length > 0){
          using(var memoryStream = new MemoryStream()){
            await fotografia.CopyToAsync(memoryStream);
            p.Fotografia = memoryStream.ToArray();
          }
        }  
        _context.Add(p);
        await _context.SaveChangesAsync();
        LoggedUser = this.User.Identity.Name;
        _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Insert", Section = "Persona", Description = "Persona no. " + persona.Id + " agregada." });
        return Json(new{success=true});
      } catch (Exception e){
        return Json(new{success=false, data=e});
      }
      //     }
      //   }
      //   return NotFound();
      // }
      // return View();
    }

    // POST: Personas/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[ValidateAntiForgeryToken]
    [HttpPost]
    [Authorize(Roles = "Supervisor, SuperUser")]
    public async Task<IActionResult> Edit(int id, IFormFile fotografia, [Bind("Id,Nombre,Paterno,Materno,RFC,CURP,Email,ClaveEmpleado,Telefono,TelefonoContacto,Estatus,Puesto,Categoria,Direccion,EntidadFederativa,Municipio,Sueldo,Opcional1,Opcional2,Adscripcion,FaceApiId,FaceApiCount")] PersonaDTO persona) {

      //if(id == persona.Id && !String.IsNullOrEmpty(persona.Nombre) && !String.IsNullOrEmpty(persona.Paterno) && !String.IsNullOrEmpty(persona.Materno) && !String.IsNullOrEmpty(persona.RFC) && !String.IsNullOrEmpty(persona.Email) && !String.IsNullOrEmpty(persona.Puesto) && !String.IsNullOrEmpty(persona.Categoria)){
         //if (ModelState.IsValid) {
      try {
        Persona p = _pRepo.GetById(persona.Id);
        p.Nombre = persona.Nombre;
        p.Paterno = persona.Paterno;
        p.Materno = persona.Materno;
        p.RFC = persona.RFC;
        p.CURP = persona.CURP;
        p.Email = persona.Email;
        p.ClaveEmpleado = persona.ClaveEmpleado;
        p.Telefono = persona.Telefono;
        p.TelefonoContacto = persona.TelefonoContacto;
        p.Categoria = persona.Categoria;
        p.Puesto = persona.Puesto;
        p.Direccion = persona.Direccion;
        p.EntidadFederativa = persona.EntidadFederativa;
        p.Municipio = persona.Municipio;
        p.Sueldo = persona.Sueldo;
        p.Adscripcion = persona.Adscripcion;
        p.Opcional1 = persona.Opcional1;
        p.Opcional2 = persona.Opcional2;
        if(settings.FaceApiUso){
          p.FaceApiId = String.IsNullOrEmpty(p.FaceApiId) ? "SINFACEAPIID" : p.FaceApiId;
          p.FaceApiCount = p.FaceApiCount > 0 ? p.FaceApiCount : 0;
        }
        if(fotografia != null && fotografia.Length > 0){
          using(var memoryStream = new MemoryStream()){
            await fotografia.CopyToAsync(memoryStream);
            p.Fotografia = memoryStream.ToArray();
          }
        } else {
          p.Fotografia = p.Fotografia;
        }
        p.Estatus = true;
        _context.Update(p);
        await _context.SaveChangesAsync();
        LoggedUser = this.User.Identity.Name;
        _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Update", Section = "Persona", Description = "Persona no. " + persona.Id + " actualizada." });
        return Json(new{success=true});
      } catch (DbUpdateConcurrencyException e) {
             if (!PersonaExists(persona.Id)) {
               return NotFound();
             } else {
               return Json(new{success=false, data=e});
             }
           }
         //}
         //return NotFound();
       //}
       return View();
    }

    // POST: Personas/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Supervisor, SuperUser")]
    public async Task<IActionResult> DeleteConfirmed(int id) {
      if(User.Identity.IsAuthenticated){
        if(id != null && id > 0){
          try{
            var persona = await _context.Persona.FindAsync(id);
            persona.Estatus = false;
            persona.Email = " ";
            _context.Update(persona);
            await _context.SaveChangesAsync();
            LoggedUser = this.User.Identity.Name;
            _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Delete", Section = "Persona", Description = "Persona no. " + persona.Id + " eliminada." });
            return Json(new{success=true});
          }catch(Exception e){
            return Json(new{success=false, data=e});
          }
        }
        return NotFound();
      }
      return View();
    }

    private bool PersonaExists(int id) {
      return _context.Persona.Any(e => e.Id == id);
    }
  }
}
