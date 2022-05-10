using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using siges.DTO;
using siges.Models;
using siges.Repository;
using siges.Utilities;
using siges.Areas.Identity.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Globalization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace siges.Controllers
{
  [Authorize(Roles = "Administración, SuperUser")]
  public class AdministracionController : Controller
  {
    private readonly IConfiguracionServicioRepository _csRepo;
    private readonly IContratoRepository _cRepo;
    private readonly IServicioRepository _sRepo;
    private readonly ILineaNegocioRepository _lnRepo;
    private readonly IOrdenServicioRepository _osRepo;
    private readonly IUbicacionRepository _uRepo;
    private readonly IPersonaRepository _pRepo;
    private readonly IInsumoRepository _iRepo;
    private readonly IActivoFijoRepository _afRepo;
    private readonly IClienteRepository _clRepo;
    private readonly IBitacoraRepository _bRepo;
    private readonly IOperador _operRepo;
    private readonly UserManager<RoatechIdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IEmailConfiguration _emailConf;
    private readonly IAdministracion _admRepo;
    private readonly IBitacoraEstatusRepository _beRepo;
    private readonly Settings settings;
        private readonly IIndexListOSDTORepository _indexListOSDTORepo;
    private String LoggedUser;

    public AdministracionController(IIndexListOSDTORepository ilosdtor, IConfiguracionServicioRepository csRepo, IContratoRepository cRepo, IServicioRepository sRepo, ILineaNegocioRepository lnRepo, IOrdenServicioRepository osRepo, IUbicacionRepository uRepo, IPersonaRepository pRepo, IInsumoRepository iRepo, IActivoFijoRepository afRepo, IClienteRepository clRepo, IBitacoraRepository bRepo, IOperador operRepo, UserManager<RoatechIdentityUser> um, RoleManager<IdentityRole> rm, IEmailConfiguration emailConf, IAdministracion admRepo, IBitacoraEstatusRepository beRepo, ISettingsRepository setRepo)
    {
      _indexListOSDTORepo = ilosdtor;
      _csRepo = csRepo;
      _cRepo = cRepo;
      _sRepo = sRepo;
      _lnRepo = lnRepo;
      _osRepo = osRepo;
      _uRepo = uRepo;
      _pRepo = pRepo;
      _iRepo = iRepo;
      _afRepo = afRepo;
      _clRepo = clRepo;
      _bRepo = bRepo;
      _operRepo = operRepo;
      _userManager = um;
      _roleManager = rm;
      _emailConf = emailConf;
      _admRepo = admRepo;
      _beRepo = beRepo;
      settings = setRepo.GetByVersion("DAMSA");
    }

    public IActionResult Index()
    {
      List<OrdenServicio> ordSer = _osRepo.GetAll(true).ToList();
      JsonSerializerSettings jsonSettings = new JsonSerializerSettings();
      jsonSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
      jsonSettings.NullValueHandling = NullValueHandling.Ignore;
      string output = JsonConvert.SerializeObject(ordSer, jsonSettings);
      ViewData["ordenesServicio"] = output;
      return View();
    }

    public IActionResult IndexListOS()
    {
      List<OrdenServicio> _all = new List<OrdenServicio>();
      foreach (OrdenServicio os in _osRepo.GetAll(true).OrderByDescending(r => r.Id).ToList())
      {
        if (os.EstatusServicio == "finalizado" || os.EstatusServicio == "cobrado" || os.EstatusServicio == "nocobrado" || os.EstatusServicio == "facturado" || os.EstatusServicio == "cancelado")
          _all.Add(os);
      }
      List<Administracion> _facOS = _admRepo.GetAll(true).ToList();
      Dictionary<int, Administracion> _fact = new Dictionary<int, Administracion>();
      foreach(OrdenServicio os in _all){
        foreach(Administracion adm in _facOS){
          if(os.Id == adm.OrdenServicioId)
            _fact[os.Id] = adm;
        }
      }
      ViewData["ordenes"] = _indexListOSDTORepo.GetAllOS().Where(o => o.EstatusServicio == "finalizado" || o.EstatusServicio == "cobrado" || o.EstatusServicio == "nocobrado" || o.EstatusServicio == "facturado" || o.EstatusServicio == "cancelado").OrderByDescending(o => o.Id).ToList();
      ViewData["facturas"] = _fact;
      return View("~/Views/Administracion/ListOrdenServicio.cshtml");
    }

    /* Editar Orden de servicios */
    public IActionResult IndexEditOS(int ordenId)
    {
      OrdenServicio eOS = _osRepo.GetByIdOS(ordenId);
      List<Cliente> _allcl = _clRepo.GetAll(true).ToList();
      List<Contrato> _allc = _cRepo.GetContratoByCliente(eOS.Cliente.Id).ToList();
      List<Ubicacion> _allu = _uRepo.getUbicacionesByCliente(eOS.Cliente.Id).ToList();
      List<Servicio> _alls = _sRepo.GetAll(true).ToList();
      List<LineaNegocio> _allln = _lnRepo.GetAll(true).ToList();
      List<Operador> _evidencias = _operRepo.GetAtencionServicio(ordenId).ToList();
      List<BitacoraEstatus> _allbe = _beRepo.GetAllByOSId(ordenId).ToList();
      ViewData["orden"] = eOS;
      ViewData["clientes"] = _allcl;
      ViewData["contratos"] = _allc;
      ViewData["servicios"] = _alls;
      ViewData["lineas"] = _allln;
      ViewData["ubicaciones"] = _allu;
      ViewData["evidencias"] = _evidencias;
      ViewData["cambiosEstado"] = _allbe;
      Dictionary<int, Persona> personas = new Dictionary<int, Persona>();
      foreach (Operador oper in _evidencias)
      {
        if (oper.Persona.Id > 0)
        {
          personas.Add(oper.Id, (Persona)_pRepo.GetById(oper.Persona.Id));
        }
      }
      ViewData["personas"] = personas; 
      return View("~/Views/Administracion/EditOrdenServicio.cshtml");
    }

    [HttpPost]
    public IActionResult CapturaDatosFactura(CapturaDatosFacturaDTO cdf){
      OrdenServicio os = _osRepo.GetById(cdf.OrdenServicioId);
      os.EstatusServicio = "facturado";
      _osRepo.Update(os);
      Administracion adm = new Administracion(){
        Estatus = true,
                OrdenServicioId = cdf.OrdenServicioId,
                PersonaId = _pRepo.GetByEmail(this.User.Identity.Name).Single().Id,
                FechaAdministrativa = DateTime.Now,
                FacturaFolio = cdf.FacturaFolio,
                FacturaFecha = cdf.FacturaFecha};
      _admRepo.Insert(adm);
      if(siges.Utilities.Tools.ValidaCamposCompletosOSyP(os, _pRepo.GetByEmail(this.User.Identity.Name).Single())){
        Notifica notifica = new Utilities.Notifica(_emailConf, _userManager, _roleManager, "FACTURADA", new MimeKit.Multipart("Mixed"), this.User.Identity.Name.ToString(), settings);
        List<Operador> osAtendido = _operRepo.GetAtencionServicio(os.Id).ToList();
        notifica.OrdenServicioNotifica("ventas", "factura", osAtendido, osAtendido[0].OrdenServicio, _beRepo.GetFinalizadoDescriptionByOSid(osAtendido[0].OrdenServicio.Id).ToList()[0].Description);
        notifica.OrdenServicioNotifica("supervisor", "factura", osAtendido, osAtendido[0].OrdenServicio, _beRepo.GetFinalizadoDescriptionByOSid(osAtendido[0].OrdenServicio.Id).ToList()[0].Description);
      }
      return RedirectToAction("IndexListOS");
    }

    public IActionResult CapturaComentario(BitacoraEstatusDTO be){
      OrdenServicio os = _osRepo.GetByIdOS(be.OrdenServicioId);
      os.EstatusServicio = be.A;
      _osRepo.Update(os);
      _beRepo.Insert(new BitacoraEstatus(){
          Os = os,
          Folio = os.Folio.ToUpper(),
          QuienCambia = _pRepo.GetByEmail(this.User.Identity.Name).Single(),
          Email = this.User.Identity.Name.ToUpper(),
          De = be.De.ToUpper(),
          A = be.A.ToUpper(),
          FechaAdministrativa = DateTime.Now,
          Description = be.MotivoCambio });
      return RedirectToAction("IndexEditOS", "Servicio", new {ordenId=be.OrdenServicioId});
    }

    [HttpPost]
    public IActionResult EditOS(OrdenServicioDTO nOS)
    {
      OrdenServicio os = _osRepo.GetByIdOS(nOS.Id);
      os.EstatusServicio = nOS.EstatusServicio;
      //if(suwus.Utilities.Tools.ValidaCamposCompletosOSyP(os, _pRepo.GetByEmail(this.User.Identity.Name))){
        Console.WriteLine("\n\n\n{0}\n\n\n", os.EstatusServicio, this);
        /*if(os.EstatusServicio == "cobrado"){
          Notifica notifica = new Utilities.Notifica(_emailConf, _userManager, _roleManager, "COBRADA", new MimeKit.Multipart("Mixed"), this.User.Identity.Name.ToString(), settings);
          List<Operador> osAtendido = _operRepo.GetAtencionServicio(os.Id).ToList();
          notifica.OrdenServicioNotifica("ventas", "cobrado", osAtendido, os, _beRepo.GetFinalizadoDescriptionByOSid(os.Id).ToList()[0].Description);
          notifica.OrdenServicioNotifica("supervisor", "cobrado", osAtendido, os, _beRepo.GetFinalizadoDescriptionByOSid(os.Id).ToList()[0].Description);
          Console.WriteLine("\n\n{0} {1} {2} {3} {4} {5}\n\n", os.Folio, os.Cliente.RazonSocial, os.Contrato.Nombre, os.Ubicacion.Nombre, os.LineaNegocio.Nombre, os.Tipo);
        }
        if(os.EstatusServicio == "nocobrado"){
          Notifica notifica = new Utilities.Notifica(_emailConf, _userManager, _roleManager, "NO COBRADA", new MimeKit.Multipart("Mixed"), this.User.Identity.Name.ToString(), settings);
          List<Operador> osAtendido = _operRepo.GetAtencionServicio(os.Id).ToList();
          notifica.OrdenServicioNotifica("todos", "nocobrado", osAtendido, os, _beRepo.GetFinalizadoDescriptionByOSid(os.Id).ToList()[0].Description);
          Console.WriteLine("\n\n{0} {1} {2} {3} {4} {5}\n\n", os.Folio, os.Cliente.RazonSocial, os.Contrato.Nombre, os.Ubicacion.Nombre, os.LineaNegocio.Nombre, os.Tipo);
        }
        if(os.EstatusServicio == "cancelado"){
          Notifica notifica = new Utilities.Notifica(_emailConf, _userManager, _roleManager, "CANCELADA", new MimeKit.Multipart("Mixed"), this.User.Identity.Name.ToString(), settings);
          List<Operador> osAtendido = _operRepo.GetAtencionServicio(os.Id).ToList();
          notifica.OrdenServicioNotifica("todos", "cancelado", osAtendido, osAtendido[0].OrdenServicio, _beRepo.GetFinalizadoDescriptionByOSid(os.Id).ToList()[0].Description);
        }*/
      //}
      _osRepo.Update(os);
      return RedirectToAction("IndexListOS", "Administracion");
    }

    /* Lista Ordenes de servicios eliminadas */
    public IActionResult IndexListDOS()
    {
      List<OrdenServicio> _all = _osRepo.GetAll(false).OrderBy(r => r.FechaInicio).ToList();
      _all.Sort((a,b) => b.Folio.CompareTo(a.Folio));
      ViewData["ordenes"] = _all;
      return View("~/Views/Administracion/ListDeletedOrdenServicio.cshtml");
    }

    private bool CambiaAProgramado(OrdenServicio os)
    {
      if (os.EstatusServicio != "programado" || os.EstatusServicio != "reprogramado")
        if (os.FechaInicio != null && os.FechaFin != null && os.Contrato != null && os.Ubicacion != null)
          if (os.ContactoNombre != null && os.ContactoAP != null && os.ContactoEmail != null && os.ContactoTelefono != null)
            if (os.Personal.Count > 0 && os.Activos.Count > 0 && os.Insumos.Count > 0)
              return true;
      return false;
    }

    private string CambioEstatusOSa(OrdenServicio osOri, OrdenServicio osNue)
    {
      if (osOri.EstatusServicio != null && osNue.EstatusServicio != null)
        if (osOri.EstatusServicio != osNue.EstatusServicio)
          return osNue.EstatusServicio;
      return null;
    }

  }
}
