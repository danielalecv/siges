using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore;
using siges.Data;
using siges.Models;
using siges.DTO;
using siges.Utilities;
using siges.Repository;
using siges.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using MimeKit;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace siges.Controllers {
  [Authorize(Roles = "Ventas, SuperUser")]
  public class VentasController : Controller {
    private readonly IConfiguracionServicioRepository _csRepo;
    private readonly IContratoRepository _cRepo;
    private readonly IServicioRepository _sRepo;
    private readonly ILineaNegocioRepository _lnRepo;
    private readonly IUbicacionRepository _uRepo;
    private readonly IInsumoRepository _iRepo;
    private readonly IActivoFijoRepository _afRepo;
    private readonly IClienteRepository _clRepo;
    private readonly IBitacoraRepository _bRepo;
    private readonly IEmailConfiguration _emailConf;
    private String LoggedUser;
    private readonly ApplicationDbContext _context;

    private readonly IHostingEnvironment _hostingEnvironment;
    private readonly List<OrdenServicio> _listOrdenServicio;
    private List<Persona> _listOperador;
    private readonly UserManager<RoatechIdentityUser> _userManager;
    private IPersonaRepository _pRepo;
    private IOrdenPersona _opRepo;
    private IOperador _operRepo;
    private IOrdenServicioRepository _osRepo;
    private Persona operadorP;
    private Operador operadorActual;
    private List<string> EstadosOperador;
    private readonly IComercial _comRepo;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ISettingsRepository _setRepo;
    private readonly Settings settings;
    private int opId;

    public VentasController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<RoatechIdentityUser> userManager,IOrdenPersona opRepo, IOrdenServicioRepository osRepo, IPersonaRepository pRepo, IOperador operRepo, IHostingEnvironment hEnv, IConfiguracionServicioRepository csRepo, IContratoRepository cRepo, IServicioRepository sRepo, ILineaNegocioRepository lnRepo, IUbicacionRepository uRepo, IInsumoRepository iRepo, IActivoFijoRepository afRepo, IClienteRepository clRepo, IBitacoraRepository bRepo, IEmailConfiguration emailConf, IComercial comRepo, ISettingsRepository setRepo) {
      _pRepo = pRepo;
      _opRepo = opRepo;
      _listOrdenServicio = osRepo.GetAll(true).OrderBy(r => r.FechaInicio).ToList();
      _userManager = userManager;
      _roleManager = roleManager;
      _operRepo = operRepo;
      _osRepo = osRepo;
      _hostingEnvironment = hEnv;
      operadorActual = new Operador();
      _csRepo = csRepo;
      _cRepo = cRepo;
      _sRepo = sRepo;
      _lnRepo = lnRepo;
      _uRepo = uRepo;
      _iRepo = iRepo;
      _afRepo = afRepo;
      _clRepo = clRepo;
      _bRepo = bRepo;
      _context = context;
      _emailConf = emailConf;
      _comRepo = comRepo;
      _setRepo = setRepo;
      settings = setRepo.GetByVersion("DAMSA");
    }

    public IActionResult Index() {
      List<OrdenServicio> ordSer = _osRepo.GetAll(true).ToList();
      JsonSerializerSettings jsonSettings = new JsonSerializerSettings();
      jsonSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
      jsonSettings.NullValueHandling = NullValueHandling.Ignore;
      string output = JsonConvert.SerializeObject(ordSer, jsonSettings);
      ViewData["ordenesServicio"] = output;
      return View();
    }

    public IActionResult SolicitarServicio() {
      List<Cliente> _allcl = _clRepo.GetAll(true).ToList();
      List<Servicio> _alls = _sRepo.GetAll(true).ToList();
      List<LineaNegocio> _allln = _lnRepo.GetAll(true).ToList();
      ViewData["clientes"] = _allcl;
      ViewData["servicios"] = _alls;
      ViewData["lineas"] = _allln;
      return View("~/Views/Ventas/SolicitarServicio.cshtml");
    }

    public IActionResult CreateCliente() {
      return View();
    }

    [HttpPost]
    public IActionResult SaveCliente(string RazonSocial, string RFC, string Telefono, string Opcional1, string Opcional2) {
      Console.WriteLine(String.Format("RazonSocial {0} RFC {1} Telefono {2} Opcional1 {3} Opcional2{4} ModelState: {5}", RazonSocial, RFC, Telefono, Opcional1, Opcional2, ModelState.IsValid));
      if(ModelState.IsValid) {
        Cliente nuevoCliente = new Cliente(){RazonSocial = RazonSocial, RFC = RFC, Telefono = Telefono, Opcional1 = Opcional1, Opcional2 = Opcional2, Estatus = true};
        _clRepo.Insert(nuevoCliente);
      }
      return RedirectToAction("CreateUbicacion", "Ventas");
    }

    public IActionResult CreateContrato(){
      ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "RazonSocial");
      ViewData["ServicioId"] = new SelectList(_context.Servicio, "Id", "Nombre");
      return View();
    }

    // POST: Ventas/CreateContrato
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateContrato([Bind("Id,Nombre,Tipo,Estatus,Opcional1,Opcional2,ClienteId,ServicioId")] Contrato contrato) {
      if (ModelState.IsValid) {
        contrato.Estatus = true;
        _context.Add(contrato);
        await _context.SaveChangesAsync();
        LoggedUser = this.User.Identity.Name;
        _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Insert", Section = "Contrato", Description = "Contrato no. " + contrato.Id + " agregado." });
        return RedirectToAction(nameof(Index));
      }
      ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "RazonSocial", contrato.ClienteId);
      ViewData["ServicioId"] = new SelectList(_context.Servicio, "Id", "Nombre", contrato.ServicioId);
      return RedirectToAction("SolicitarServicio", "Ventas");
    }

    public IActionResult CreateUbicacion(){
      ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "RazonSocial");
      return View();
    }

    // POST: Ventas/CreateUbicacion
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateUbicacion([Bind("Id,Nombre,Direccion,Contacto,ContactoTelefono,ContactoEmail,ContactoOpcional,ContactoOpcionalTelefono,ContactoOpcionalEmail,Estatus,ClienteId")] Ubicacion ubicacion) {
      if (ModelState.IsValid) {
        ubicacion.Estatus = true;
        _context.Add(ubicacion);
        await _context.SaveChangesAsync();
        LoggedUser = this.User.Identity.Name;
        _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Insert", Section = "Ubicación", Description = "Ubicación no. " + ubicacion.Id + " agregada." });
        return RedirectToAction(nameof(Index));
      }
      ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "RazonSocial", ubicacion.ClienteId);
      return RedirectToAction("SolicitarServicio", "Ventas");
    }

    [HttpPost]
    public IActionResult SaveOS(OrdenServicioDTO nOS) {
      List<OrdenPersonalDTO> personal = new List<OrdenPersonalDTO>();
      List<OrdenInsumoDTO> insumos = new List<OrdenInsumoDTO>();
      List<OrdenActivoFijoDTO> activos = new List<OrdenActivoFijoDTO>();
      foreach (var aP in JArray.Parse(nOS.dtPersonas)) {
        JObject p = JObject.Parse(aP.ToString());
        personal.Add(new OrdenPersonalDTO { Id = Int32.Parse(p.Property("idordper").Value.ToString()), PersonaId = Int32.Parse(p.Property("idpersona").Value.ToString()) });
      }
      foreach (var aA in JArray.Parse(nOS.dtActivos)) {
        JObject a = JObject.Parse(aA.ToString());
        activos.Add(new OrdenActivoFijoDTO { Id = Int32.Parse(a.Property("idordact").Value.ToString()), ActivoFijoId = Int32.Parse(a.Property("idactivofijo").Value.ToString()) });
      }
      foreach (var aI in JArray.Parse(nOS.dtInsumos)) {
        JObject i = JObject.Parse(aI.ToString());
        insumos.Add(new OrdenInsumoDTO { Id = Int32.Parse(i.Property("idordins").Value.ToString()), InsumoId = Int32.Parse(i.Property("idinsumo").Value.ToString()), Cantidad = Int32.Parse(i.Property("cantidad").Value.ToString()) });
      }
      OrdenServicio os = new OrdenServicio();
      CultureInfo cin = new CultureInfo("es-MX");
      //os.Folio = nOS.Folio;
      os.Cliente = _clRepo.GetById(nOS.Cliente);
      os.Contrato = _cRepo.GetById(nOS.Contrato);
      os.Ubicacion = _uRepo.GetById(nOS.Ubicacion);
      os.LineaNegocio = _lnRepo.GetById(nOS.LineaNegocio);
      os.Servicio = _sRepo.GetById(nOS.Servicio);
      os.Tipo = nOS.Tipo;
      //
      // TODO: to get from settings if is necessary to validate LineaNegocio to restrict the minimum date
      //
      //if(os.LineaNegocio == "INSTALACION"){
      //}else if (os.LineaNegocio == "MANTENIMIENTO")
      //
      if(settings.ValidateMinimumDate){
        string[] criteria = settings.MinimumDateCriteria.Split(new Char[]{' '});
        for(int i = 0; i < criteria.Length/2; i++){
          if(criteria[i*2] == os.LineaNegocio.Nombre){
            if(DateTime.Compare(DateTime.Parse(nOS.FechaInicio, cin), DateTime.Today.AddDays(Double.Parse(criteria[(i*2)+1]))) < 0 || DateTime.Compare(DateTime.Parse(nOS.FechaFin, cin), DateTime.Today.AddDays(Double.Parse(criteria[(i*2)+1]))) < 0){
              ViewData["ln"] = os.LineaNegocio.Nombre;
              ViewData["fi"] = DateTime.Parse(nOS.FechaInicio, cin);
              ViewData["ff"] = DateTime.Parse(nOS.FechaFin, cin);
              return View("~/Views/Servicios/InvalidDate.cshtml");
            } else {
              os.FechaInicio = String.IsNullOrEmpty(nOS.FechaInicio) ? (DateTime?)null : DateTime.Parse(nOS.FechaInicio, cin);
              os.FechaFin = String.IsNullOrEmpty(nOS.FechaFin) ? (DateTime?)null : DateTime.Parse(nOS.FechaFin, cin);
            }
            break;
          }
          Console.WriteLine("\n\n{0} - {1}\n\n", nOS.FechaInicio, nOS.FechaFin);
          os.FechaInicio = String.IsNullOrEmpty(nOS.FechaInicio) ? (DateTime?)null : DateTime.Parse(nOS.FechaInicio, cin);
          os.FechaFin = String.IsNullOrEmpty(nOS.FechaFin) ? (DateTime?)null : DateTime.Parse(nOS.FechaFin, cin);
        }
      } else {
        os.FechaInicio = String.IsNullOrEmpty(nOS.FechaInicio) ? (DateTime?)null : DateTime.Parse(nOS.FechaInicio, cin);
        os.FechaFin = String.IsNullOrEmpty(nOS.FechaFin) ? (DateTime?)null : DateTime.Parse(nOS.FechaFin, cin);
      }
      //os.FechaInicio = String.IsNullOrEmpty(nOS.FechaInicio) ? (DateTime?)null : DateTime.Parse(nOS.FechaInicio, cin);
      //os.FechaFin = String.IsNullOrEmpty(nOS.FechaFin) ? (DateTime?)null : DateTime.Parse(nOS.FechaFin, cin);
      os.EstatusServicio = nOS.EstatusServicio;
      os.Observaciones = nOS.Observaciones;
      os.ContactoNombre = nOS.ContactoNombre;
      os.ContactoAP = nOS.ContactoAP;
      os.ContactoAM = nOS.ContactoAM;
      os.ContactoEmail = nOS.ContactoEmail;
      os.ContactoTelefono = nOS.ContactoTelefono;
      os.Opcional1 = nOS.Opcional1;
      os.Opcional2 = nOS.Opcional2;
      os.Opcional3 = nOS.Opcional3;
      os.Opcional4 = nOS.Opcional4;
      os.Estatus = nOS.Estatus;
      os.PersonaComercial = _pRepo.GetByEmail(this.User.Identity.Name).Single();
      if (personal.Count > 0) {
        foreach (var i in personal) {
          os.Personal.Add(new OrdenPersona() { Persona = _pRepo.GetById(i.PersonaId), Estatus = true });
        }
      }
      if (insumos.Count > 0) {
        foreach (var x in insumos) {
          os.Insumos.Add(new OrdenInsumo() { Insumo = _iRepo.GetById(x.InsumoId), Cantidad = x.Cantidad, Estatus = true });
        }
      }
      if (activos.Count > 0) {
        foreach (var y in activos) {
          os.Activos.Add(new OrdenActivoFijo() { ActivoFijo = _afRepo.GetById(y.ActivoFijoId), Estatus = true });
        }
      }
      os.FechaAdministrativa = DateTime.Now;
      if(!(String.IsNullOrEmpty(nOS.NombreCompletoCC1) && String.IsNullOrEmpty(nOS.EmailCC1))) {
        os.NombreCompletoCC1 = nOS.NombreCompletoCC1;
        os.EmailCC1 = nOS.EmailCC1;
      }
      if(!(String.IsNullOrEmpty(nOS.NombreCompletoCC2) && String.IsNullOrEmpty(nOS.EmailCC2))) {
        os.NombreCompletoCC2 = nOS.NombreCompletoCC2;
        os.EmailCC2 = nOS.EmailCC2;
      }
      os.Folio = getOsFolio(_setRepo.GetByVersion("DAMSA").FolioPrefix);
      _osRepo.Insert(os);
      int perId = _pRepo.GetByEmail(this.User.Identity.Name).Single().Id;
      int newOSId = 0;
      List<OrdenServicio> osPersonaList = _osRepo.GetByPersonaComercialId(perId).ToList();
      if(osPersonaList != null && osPersonaList.Count > 0){
        foreach(OrdenServicio osP in osPersonaList){
          if(osP.Folio == os.Folio)
            newOSId = os.Id;
        }
      }
      Comercial com = new Comercial(){Estatus = true, OrdenServicioId = newOSId, PersonaId = perId, FechaAdministrativa = DateTime.Now};
      _comRepo.Insert(com);
      LoggedUser = this.User.Identity.Name;
      if(siges.Utilities.Tools.ValidaCamposCompletosOSyP(os, _pRepo.GetByEmail(this.User.Identity.Name).Single())){
        Notifica notifica = new Notifica(_emailConf, _userManager, _roleManager, "Orden de servicio generada", new Multipart("mixed"), LoggedUser.ToString(), _setRepo.GetByVersion("DAMSA"));
        notifica.OrdenServicioNotifica("supervisor", "solicita", null, os, null);
        notifica.OrdenServicioNotifica("ventas", "solicita", null, os, null);
      }
      _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Insert", Section = "Orden de servicio", Description = "Orden de servicio no. " + nOS.Id + " agregada." });
      ViewData["ordenServicio"] = os;
      return View("~/Views/Ventas/ConfirmOrdenServicio.cshtml");
    }

    private string getOsFolio(string typeOS){
      List<OrdenServicio> osList = _osRepo.GetAll().ToList();
      if(!String.IsNullOrEmpty(typeOS))
        return String.Format("{0}{1:D7}", typeOS, osList[osList.Count -1].Id+1).ToUpper();
      else
        return null;
    }
  }
}
