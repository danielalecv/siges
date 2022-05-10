using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

using siges.Models;
using siges.Repository;
using siges.Areas.Identity.Data;
using siges.Data;

namespace siges.Controllers
{
  //[Authorize(Roles = "Supervisor, SuperUser")]
  public class RestController : Controller
  {
    private readonly IActivoFijoRepository _afRepo;
    private readonly IEntradaActivoFijoRepository _entradaAfRepo;
    private readonly IContratoRepository _cRepo;
    private readonly IUbicacionRepository _uRepo;
    private readonly IPersonaRepository _pRepo;
    private readonly IInsumoRepository _iRepo;
    private readonly IClienteRepository _clRepo;
    private readonly IOrdenServicioRepository _osRepo;
    private readonly IProducto _prodRepo;
    private readonly IServicioRepository _sRepo;
    private readonly ILineaNegocioRepository _lnRepo;
    private readonly UserManager<RoatechIdentityUser> _usrManager;
    private readonly RoleManager<IdentityRole> _rolManager;
    private readonly IOrdenActivoFijo _oafRepo;
    private readonly Settings _settings;
    private readonly ApplicationDbContext _context;
    private readonly IPaquete _pqRepo;
    private readonly IPaqueteInsumo _piRepo;
    private readonly IMarca _mRepo;
    private readonly ITipoProducto _tpRepo;

    public RestController(IActivoFijoRepository repository, IEntradaActivoFijoRepository entradaafrepo, IContratoRepository cRepo, IUbicacionRepository uRepo, IPersonaRepository pRepo, IInsumoRepository iRepo, IOrdenServicioRepository osRepo, UserManager<RoatechIdentityUser> um, RoleManager<IdentityRole> rm, ISettingsRepository setRepo, IProducto prodRepo, IClienteRepository clRepo, IServicioRepository sRepo, ILineaNegocioRepository lnRepo, IOrdenActivoFijo oafRepo, ApplicationDbContext ctx, IPaquete pqR, IPaqueteInsumo piR, IMarca mR, ITipoProducto tpR)
    {
      _afRepo = repository;
      _entradaAfRepo = entradaafrepo;
      _cRepo = cRepo;
      _uRepo = uRepo;
      _pRepo = pRepo;
      _iRepo = iRepo;
      _sRepo = sRepo;
      _prodRepo = prodRepo;
      _osRepo = osRepo;
      _usrManager = um;
      _rolManager = rm;
      _clRepo = clRepo;
      _lnRepo = lnRepo;
      _oafRepo = oafRepo;
      _pqRepo = pqR;
      _piRepo = piR;
      _context = ctx;
      _mRepo = mR;
      _tpRepo = tpR;
      _settings = setRepo.GetByVersion("DAMSA");
    }


    public IActionResult Index()
    {
      return View();
    }

    [HttpGet]
    public JsonResult GetActivoFijoByClientAndOS(int cId, string lnNombre) {
      //SqlConnection myConnection = new SqlConnection("Server=192.168.1.99,1499;Database=suwus_siges_test;user id=sa;password=Ubuntu#Mssql");
      //SqlConnection myConnection = new SqlConnection("Server=192.168.1.102;Database=suwus_damsa;user id=sa;password=Root1234");
      /*SqlConnection myConnection = new SqlConnection("Server=192.168.1.99,1499;Database=siges_damsa_test;user id=sa;password=Ubuntu#Mssql");
      myConnection.Open();
      SqlCommand objcmd = new SqlCommand("select af.Id as afid, os.Id as osid, af.descripcion as activofijo, af.tipo as serie, af.opcional2 as opcional2, os.folio as os, c.razonsocial as cliente, ln.nombre as lineanegocio from ordenactivofijo oa full join activofijo af on oa.activofijoid = af.id full join ordenservicio os on os.id = oa.ordenservicioid full join cliente c on os.clienteid = c.id full join lineanegocio ln on os.lineanegocioid = ln.id where c.id = " + cId + " and af.descripcion is not null and ln.nombre = 'INSTALACIÓN' and af.estatus = '1'", myConnection);
      SqlDataAdapter adapter = new SqlDataAdapter(objcmd);
      DataTable dt = new DataTable();
      adapter.Fill(dt);
      myConnection.Close();
      dt.ToString();
      string output = JsonConvert.SerializeObject(dt);*/

      if(cId != null && lnNombre != null){
        List<OrdenActivoFijo> oafL = _oafRepo.GetAllByClienteAndByLineaNegocio(cId, lnNombre).ToList();
        Dictionary<string,List<OrdenActivoFijo>> json = new Dictionary<string, List<OrdenActivoFijo>>();
        json["ordenActivoFijo"] = oafL;
        JsonSerializerSettings settings = new JsonSerializerSettings();
        settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        string output = JsonConvert.SerializeObject(json, settings);
        return Json(output);
      }
      return Json("");
    }

    [HttpGet]
    public JsonResult GetContactoCliente(int cId){
      if(cId > 0) {
        var cc = _context.ContactoCliente.Where(r => r.Cliente.Id == cId).Include(r => r.Contactos.Where( s=> s.Estatus == true));
        if(cc.Any()){
          var output = cc.Single().Contactos;
          output.Sort((a,b) => a.Nombre.CompareTo(b.Nombre));
          return Json(new{success=true,data=output});
        }
      }
      return Json(new{success=false});
    }

    [HttpGet]
    public JsonResult GetInsumoById(int iId){
      Insumo insumo = _iRepo.GetById(iId);
      return Json(insumo);
    }
    [HttpGet]
    public JsonResult GetMarcasPorProd(string prod){
      List<Producto> p = _prodRepo.GetAllByProdName(prod).ToList();
      p.Sort((a,b) => a.Nombre.CompareTo(b.Nombre));
      string output = JsonConvert.SerializeObject(p);
      return Json(output);
    }

    [HttpGet]
    public JsonResult GetOrdenesServicio()
    {
      List<OrdenServicio> ordenes = _osRepo.GetAll(true).ToList();
      ordenes.Sort((a,b) => a.Folio.CompareTo(b.Folio));
      var response = new Dictionary<string, object>
      {
        { "ordenes", ordenes }
      };
      return Json(response);
    }

    [HttpGet]
    public bool IfPersonExist(string RFC, string CURP){
      Console.WriteLine("Estoy en ifPersonExist: " + RFC + " "+ CURP+"\n");
      if(!String.IsNullOrEmpty(RFC) && !String.IsNullOrEmpty(CURP))
        if(_pRepo.Exist(RFC, CURP))
          return true;
      return false;
    }

    [HttpGet]
    public bool IfDocumentoExist(string nombre, int cliente, int servicio){
      if(!String.IsNullOrEmpty(nombre) && cliente != null && servicio != null){
        List<Contrato> cl = _cRepo.GetAll(true).ToList();
        if(cl.Count > 0)
          foreach(Contrato c in cl)
            if(!String.IsNullOrEmpty(c.Nombre) && c.Nombre.Equals(nombre) && c.Cliente.Id == cliente && c.Servicio.Id == servicio)
              return true;
      }
      return false;
    }

    [HttpGet]
    public bool IfUbicacionExist(string nombre, int cliente){
      if(!String.IsNullOrEmpty(nombre) && cliente > 0)
        return _uRepo.Exist(cliente, nombre);
      return true;
    }

    [HttpGet]
    public bool IfServicioExists(string nombre){
      if(!String.IsNullOrEmpty(nombre)){
        List<Servicio> sl = _sRepo.GetAll(true).ToList();
        if(sl.Count > 0)
          foreach(Servicio s in sl)
            if(!String.IsNullOrEmpty(s.Nombre) && s.Nombre.Equals(nombre))
              return true;
      }
      return false;
    }

    [HttpGet]
    public bool IfLineaNegocioExists(string nombre){
      if(!String.IsNullOrEmpty(nombre)){
        List<LineaNegocio> lnl = _lnRepo.GetAll(true).ToList();
        if(lnl.Count > 0)
          foreach(LineaNegocio ln in lnl)
            if(!String.IsNullOrEmpty(ln.Nombre) && ln.Nombre.Equals(nombre))
              return true;
      }
      return false;
    }

    [HttpGet]
    public bool IfClienteExist(string rfc){
      if(!String.IsNullOrEmpty(rfc))
        return _clRepo.Exist(rfc);
      return true;
    }

    [HttpGet]
    public bool IfInsumoExist(string clave){
      if(!String.IsNullOrEmpty(clave))
        return _iRepo.Exist(clave);
      return true;
    }


    [HttpGet]
    public bool IfActivoFijoExist(string clave){
      if(!String.IsNullOrEmpty(clave))
        return _afRepo.Exist(clave);
      return true;
    }

    [HttpGet]
    public JsonResult GetValidations(){
      return Json(_settings.MinimumDateCriteria);
    }

    [HttpGet]
    public JsonResult GetInsumosByPaquete(int pqId){
      List<PaqueteInsumo> pi = _piRepo.GetAllPIbyPaqueteId(pqId).ToList();
      pi.Sort((a,b) => a.Paquete.Descripcion.CompareTo(b.Paquete.Descripcion));
      return Json(pi);
    }

    [HttpGet]
    public bool OSConfirm(string osFolio) {
      if(!String.IsNullOrEmpty(osFolio))
        if(osFolio.Count() == _settings.FolioPrefix.Count()+_settings.FolioDigitsLong){
          try {
            OrdenServicio os = _osRepo.GetOSbyFolio(osFolio);
            if(os != null)
              return true;
            else
              return false;
          } catch (Exception e) {
            Console.WriteLine("\nERROR\n{0} {1}\nERROR\n", e.Data, e.Message);
            return false;
          }
        }
      return false;
    }

    [HttpGet]
    public JsonResult GetContratosFromCliente(int clienteid)
    {
      List<Contrato> contratos = _cRepo.GetContratoByCliente(clienteid).ToList();
      contratos.Sort((a,b) => a.Nombre.CompareTo(b.Nombre));
      var response = new Dictionary<string, object>();
      response.Add("listContratos", contratos);
      return Json(response);
    }

    [HttpGet]
    public JsonResult GetUbicacionesFromCliente(int clienteid)
    {
      List<Ubicacion> ubicaciones = _uRepo.getUbicacionesByCliente(clienteid).OrderBy(r => r.Nombre).ToList();
      ubicaciones.Sort((a, b) => a.Nombre.CompareTo(b.Nombre));
      var response = new Dictionary<string, object>();
      response.Add("listUbicaciones", ubicaciones);
      return Json(response);
    }

    [HttpGet]
    public JsonResult GetServiciosFromLineaNegocio(int lineaid){
      List<Servicio> servicios = _sRepo.GetServiciosByLineaNegocio(lineaid).ToList();
      servicios.Sort((a,b) => a.Nombre.CompareTo(b.Nombre));
      var response = new Dictionary<string, object>();
      response.Add("listServicios", servicios);
      return Json(response);
    }

    [HttpGet]
    public JsonResult GetActivoFijos()
    {
      List<ActivoFijo> activos = _afRepo.GetAll(true).ToList();
      activos.Sort((a,b) => a.Descripcion.CompareTo(b.Descripcion));
      var response = new Dictionary<string, object>();
      response.Add("listActivos", activos);
      return Json(response);
    }

    [HttpGet]
    public JsonResult GetPersonas() {
      var conn = RelationalDatabaseFacadeExtensions.GetDbConnection(_context.Database);
      List<int> pIdL = null;
      List<Persona> pL = null;
      if(((System.Data.SqlClient.SqlConnection)conn).State == System.Data.ConnectionState.Open)
        ((System.Data.SqlClient.SqlConnection)conn).Close();
      ((System.Data.SqlClient.SqlConnection) conn).Open();
      //SqlCommand cmd = new SqlCommand("select p.id from persona p where not p.id in (select per.id from aspnetuserroles rol left join aspnetusers usr on rol.userid = usr.id left join persona per on usr.perid = per.id left join aspnetroles rls on rol.roleid = rls.id where not rls.name = @Role) and p.estatus = 1", (System.Data.SqlClient.SqlConnection)conn);
      SqlCommand cmd = new SqlCommand("select per.id from aspnetuserroles rol left join aspnetusers usr on rol.userid = usr.id left join persona per on usr.perid = per.id left join aspnetroles rls on rol.roleid = rls.id where rls.name = @Role", (System.Data.SqlClient.SqlConnection)conn);
      cmd.Parameters.Add("@Role", SqlDbType.NVarChar);
      cmd.Parameters["@Role"].Value = "Operador";
      SqlDataReader dataReader = cmd.ExecuteReader();
      if(dataReader.HasRows){
        pIdL = new List<int>();
        pL = new List<Persona>();
        while(dataReader.Read())
          pIdL.Add((int)dataReader[0]);
      }
      if(((System.Data.SqlClient.SqlConnection)conn).State == System.Data.ConnectionState.Open)
        ((System.Data.SqlClient.SqlConnection)conn).Close();
      if(pIdL != null && pIdL.Count > 0)
        foreach(int id in pIdL)
          pL.Add(_pRepo.GetById(id));
      if(pL != null && pL.Count > 0){
        var response = new Dictionary<string, object>();
        pL.Sort();
        response.Add("listPersonas", pL);
        return Json(response);
      }
      return Json("");
    }

    [HttpGet]
    public JsonResult GetAllPaquete(){
      List<Paquete> paquetes = _pqRepo.GetAll(true).ToList();
      paquetes.Sort((a,b) => a.Descripcion.CompareTo(b.Descripcion));
      return Json(paquetes);
    }

    [HttpGet]
    public JsonResult GetInsumos()
    {
      List<Insumo> insumos = _iRepo.GetAll(true).ToList();
      insumos.Sort((a,b) => a.Descripcion.CompareTo(b.Descripcion));
      var response = new Dictionary<string, object>();
      response.Add("listInsumos", insumos);
      return Json(response);
    }
  }
}
