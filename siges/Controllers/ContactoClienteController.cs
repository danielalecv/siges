using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using siges.Data;
using siges.DTO;
using siges.Models;
using siges.Repository;
using siges.Utilities;
using siges.Services;

namespace siges.Controllers
{
    [Authorize(Roles = "Supervisor, Ventas, Admin")]
    public class ContactoClienteController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IBitacoraRepository _bRepo;
        private readonly IContactoCliente _ccRepo;
        private readonly IClienteRepository _cliRepo;
        private readonly IPersonaRepository _pRepo;
        private readonly IRoatechIdentityUserRepo riuRepo;
        private readonly IMailSupport _mailSupport;
        private readonly IClientContactService _contactClient;
        private String LoggedUser;

        public ContactoClienteController(ApplicationDbContext context, IBitacoraRepository bRepo, IContactoCliente ccR, IPersonaRepository pR, IRoatechIdentityUserRepo riuR, IMailSupport mailSupport, IClienteRepository cliRepo, IClientContactService contactClient)
        {
            _context = context;
            _bRepo = bRepo;
            _ccRepo = ccR;
            _pRepo = pR;
            riuRepo = riuR;
            _mailSupport = mailSupport;
            _cliRepo = cliRepo;
            _contactClient = contactClient;
        }

        // GET: ContactoClienteList
        public async Task<IActionResult> Index()
        {
            List<ContactoCliente> contactoClienteList = await _context.ContactoCliente.Where(r => r.Estatus == true).OrderBy(r => r.Cliente.RazonSocial).ToListAsync();
            contactoClienteList.Sort((a, b) => a.Cliente.RazonSocial.CompareTo(b.Cliente.RazonSocial));
            return View(contactoClienteList);
        }

        // GET: ContactoCliente/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id > 0 || id == null)
                {
                    try
                    {
                        if (!_context.Cliente.Any(r => r.Id == (int)id))
                        {
                            return RedirectToAction("Index", "Clientes");
                        }
                        else
                        {
                            var contactoCliente = _ccRepo.GetByClienteId((int)id);
                            if (contactoCliente == null)
                            {
                                ContactoCliente cc = new ContactoCliente();
                                cc.Cliente = _context.Cliente.Where(r => r.Id == (int)id).Single();
                                cc.Estatus = true;
                                cc.FechaCreacion = DateTime.Now;
                                cc.FechaModificacion = DateTime.Now;
                                cc.CreadorPor = riuRepo.GetAllInfoByEmail(this.User.Identity.Name).Single();
                                cc.ModificadoPor = riuRepo.GetAllInfoByEmail(this.User.Identity.Name).Single();
                                ViewData["Cliente"] = cc.Cliente.RazonSocial;
                                ViewData["ID"] = cc.Cliente.Id;
                                return View("~/Views/Clientes/Contacts.cshtml", cc.Contactos.ToList());
                            }
                            else
                            {
                                ViewData["Cliente"] = contactoCliente.Single().Cliente.RazonSocial;
                                ViewData["ID"] = contactoCliente.Single().Cliente.Id;
                                return View("~/Views/Clientes/Contacts.cshtml", contactoCliente.Single().Contactos.ToList());
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        return NotFound(e);
                    }
                }
                return NotFound();
            }
            return View("~/Views/Clientes/Contacts.cshtml");
        }

        // GET: ContactoCliente/Detail/5
        public async Task<IActionResult> ccDetail(int? Contactoid)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Contactoid > 0)
                {
                    try
                    {
                        var p = _context.Persona.Where(r => r.Id == Contactoid);
                        if (p == null)
                            return NotFound();
                        else
                            return Json(new { success = true, data = p.Single() });
                    }
                    catch (Exception e)
                    {
                        return Json(new { success = false, data = e });
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
        public IActionResult Create(ContactoClienteDTO ccDTO)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ccDTO.clienteId > 0 && ccDTO.contactos.Count > 0)
                {
                    if (ModelState.IsValid)
                    {
                        if (!_cliRepo.ExistById(ccDTO.clienteId))
                            return RedirectToAction("Index", "Clientes");
                        var c = _ccRepo.GetByClienteId(ccDTO.clienteId);

                        if (c != null)
                        {
                            string token = Tools.GenerateToken();
                            string host = GetHostForConfirmContact();

                            ContactoCliente cc = c.Single();
                            int numContactos = cc.Contactos.Count;
                            foreach (Persona p in ccDTO.contactos)
                            {
                                if (!_pRepo.ExistByEmail(p.Email))
                                {
                                    p.Estatus = false;
                                    p.FaceApiId = "SINFACEAPIID";
                                    p.Token = token;
                                    cc.Contactos.Add(p);
                                }
                            }
                            //enviar correo de confirmacion-------------------------------------------------------------------------
                            if (_mailSupport.SendMailConfirmContact(cc.Contactos[cc.Contactos.Count() - 1].Email, token, host))
                            {
                                Console.WriteLine("success mail");
                                if (cc.Contactos.Count > numContactos)
                                {
                                    cc.Opcional1 = ccDTO.opcional1;
                                    cc.Opcional2 = ccDTO.opcional2;
                                    cc.ModificadoPor = riuRepo.GetAllInfoByEmail(this.User.Identity.Name).Single();
                                    cc.FechaModificacion = DateTime.Now;
                                    _ccRepo.Update(cc);
                                    LoggedUser = this.User.Identity.Name;
                                    _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Update", Section = "ContactoCliente", Description = "ContactoCliente No. " + cc.Id + " actualizado." });

                                    return Json(new { success = true, data = cc });
                                }
                                else
                                {
                                    Console.WriteLine("fail mail");
                                }
                            }
                        }
                        else
                        {
                            try
                            {
                                string token = Tools.GenerateToken();
                                string host = GetHostForConfirmContact();

                                ContactoCliente cc = new ContactoCliente();
                                //cc.Cliente = _context.Cliente.Where(r => r.Id == ccDTO.clienteId).Single();
                                cc.Cliente = _cliRepo.GetById(ccDTO.clienteId);
                                cc.Opcional1 = ccDTO.opcional1;
                                cc.Opcional2 = ccDTO.opcional2;
                                cc.Estatus = true;
                                cc.FechaCreacion = DateTime.Now;
                                cc.FechaModificacion = DateTime.Now;
                                cc.CreadorPor = riuRepo.GetAllInfoByEmail(this.User.Identity.Name).Single();
                                cc.ModificadoPor = riuRepo.GetAllInfoByEmail(this.User.Identity.Name).Single();
                                foreach (Persona p in ccDTO.contactos)
                                {
                                    p.Estatus = false;
                                    p.FaceApiId = "SINFACEAPIID";
                                    p.Token = token;
                                    cc.Contactos.Add(p);
                                }
                                //enviar correo de confirmacion-------------------------------------------------------------------------
                                Console.WriteLine("\n\ndestino: " + cc.Contactos[cc.Contactos.Count() - 1].Email + "\n\n");
                                if (_mailSupport.SendMailConfirmContact(cc.Contactos[cc.Contactos.Count() - 1].Email, token, host))
                                {
                                    Console.WriteLine("success mail");

                                    //var entityEntry = _context.ContactoCliente.Add(cc);
                                    //await _context.SaveChangesAsync();
                                    var entityEntry = _contactClient.InsertContactoCliente(cc);
                                    if (entityEntry != null)
                                    {
                                        LoggedUser = this.User.Identity.Name;
                                        _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Insert", Section = "ContactoCliente", Description = "ContactoCliente No. " + entityEntry.Entity.Id + " agregado." });

                                        return Json(new { success = true, data = cc });
                                    }
                                    else
                                    {
                                        return new JsonResult(false);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("fail mail");
                                }
                            }
                            catch (Exception e)
                            {
                                return new JsonResult(e);
                            }
                        }
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                return new JsonResult(false);
            }
            return View("~/Views/ContactoCliente/Index.cshtml");
        }

        [HttpGet]
        public IActionResult ConfirmContact(string token)
        {
            var contactToken = _pRepo.GetByToken(token);
            if (contactToken != null)
            {
                var contactoCliente = _ccRepo.GetByClienteToken(contactToken);
                string clientName = contactoCliente.Cliente.RazonSocial;
                if (contactoCliente != null)
                {
                    contactToken.Estatus = true;
                    contactToken.Token = null;
                    var updatePerson = _contactClient.UpdatePerson(contactToken);
                    if (updatePerson)
                    {
                        ViewData["Message"] = Constants.ContactoCliente.ConfirmSuccess + clientName;
                    }
                    else
                    {
                        ViewData["Message"] = Constants.ContactoCliente.ConfirmFail;
                    }
                }
                else
                {
                    ViewData["Message"] = Constants.ContactoCliente.ConfirmFail;
                }
            }
            else
            {
                ViewData["Message"] = Constants.ContactoCliente.ConfirmFail;
            }
            return View();
        }

        // POST: ContactoCliente/Update/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ContactoClienteDTO ccDTO)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id > 0)
                {
                    if (ModelState.IsValid)
                    {
                        var p = _context.Persona.Where(r => r.Id == id);
                        p.Single().Nombre = ccDTO.contactos[0].Nombre;
                        p.Single().Paterno = ccDTO.contactos[0].Paterno;
                        p.Single().Materno = ccDTO.contactos[0].Materno;
                        p.Single().Email = ccDTO.contactos[0].Email;
                        p.Single().Telefono = ccDTO.contactos[0].Telefono;
                        p.Single().TelefonoContacto = ccDTO.contactos[0].TelefonoContacto;
                        p.Single().Opcional1 = ccDTO.contactos[0].Opcional1;
                        p.Single().Opcional2 = ccDTO.contactos[0].Opcional2;
                        try
                        {
                            _context.Update(p.Single());
                            await _context.SaveChangesAsync();
                            LoggedUser = this.User.Identity.Name;
                            _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Update", Section = "ContactoCliente", Description = "Persona no. " + p.Single().Id + " actualizado." });
                        }
                        catch (DbUpdateConcurrencyException e)
                        {
                            return Json(new { success = false, data = e });

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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id > 0)
                {
                    try
                    {
                        var contactoCliente = _context.Persona.Where(r => r.Id == id);
                        contactoCliente.Single().Estatus = false;
                        _context.Update(contactoCliente.Single());
                        _context.Persona.Remove(contactoCliente.Single());
                        await _context.SaveChangesAsync();
                        LoggedUser = this.User.Identity.Name;
                        _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Delete", Section = "ContactoCliente", Description = "Persona no. " + contactoCliente.Single().Id + " eliminado." });
                        return new JsonResult(true);
                    }
                    catch (Exception e)
                    {
                        return new JsonResult(e);
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }
        public string GetHostForConfirmContact()
        {
            var host = this.Request.Headers.ContainsKey("Referer") ? this.Request.Headers["Referer"].ToString() : "";
            int contDiagonal = (int)Tools.CountUntilFindSlash(host);
            host = host.Remove(host.Length - contDiagonal, contDiagonal);
            return host;
        }
    }
}