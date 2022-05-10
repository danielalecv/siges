using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;

using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

using siges.Areas.Identity.Data;
using siges.Repository;
using siges.Models;
using siges.DTO;
using siges.Data;
using siges.Utilities;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Linq;

using RestSharp;
using RestSharp.Authenticators;

namespace siges.Controllers {
  [Authorize(Roles = "Admin, Supervisor")]
  public class AdminController : Controller {

    private readonly UserManager<RoatechIdentityUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly IRoatechIdentityUserRepo roatechIdentityUserRepo;
    private readonly IPersonaRepository perRepo;
    private readonly ISemaphoreParamsRepo semRepo;
    private readonly IBitacoraRepository _bRepo;
    private readonly IClienteRepository cliRepo;
    private readonly IClienteIdentity clIdRepo;
    private readonly ApplicationDbContext context;
    private readonly IEmailConfiguration _emailConf;
    private readonly ISettingsRepository _setRepo;
    private readonly Settings settings;
    private readonly IBulkUploadTemplate _bltRepo;
    private string LoggedUser;

    public string ReturnUrl {get; set;}

    public AdminController(ApplicationDbContext _ctx, UserManager<RoatechIdentityUser> um, RoleManager<IdentityRole> rm, IRoatechIdentityUserRepo _riuR, IPersonaRepository _perrepo, ISemaphoreParamsRepo _seR, IBitacoraRepository bR, IClienteRepository _clR, IClienteIdentity _ciR, IEmailConfiguration emailConf, ISettingsRepository _set, IBulkUploadTemplate bltR){
      userManager = um;
      roleManager = rm;
      roatechIdentityUserRepo = _riuR;
      cliRepo = _clR;
      perRepo = _perrepo;
      semRepo = _seR;
      clIdRepo = _ciR;
      _bRepo = bR;
      _emailConf = emailConf;
      settings = _set.GetByVersion("DAMSA");
      _setRepo = _set;
      context = _ctx;
      _bltRepo = bltR;
    }

    public IActionResult BulkLoaderTemplate(){
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> SaveBulkLoaderTemplate(BulkLoaderTemplateDTO blt){
      Console.WriteLine("\n\nbltDTO => {0} \t {1} \t {2} \t {3} \t settings.Templates es nulo¿? {4}\n", blt.Tipo, blt.Version, blt.Archivo.FileName, blt.Archivo.Length, settings.Templates == null);
      if(User.Identity.IsAuthenticated){
        if(blt != null && !String.IsNullOrEmpty(blt.Tipo) && blt.Archivo.Length > 0){
          using(var memoryStream = new MemoryStream()){
            await blt.Archivo.CopyToAsync(memoryStream);
            //if(settings.Templates == null)
              //settings.Templates = new List<BulkUploadTemplate>();
            settings.Templates.Add(new BulkUploadTemplate{
                Tipo = blt.Tipo,
                Version = blt.Version,
                Archivo = memoryStream.ToArray(),
                FechaAdministrativa = DateTime.Now,
                TamanoArchivo = blt.Archivo.Length,
                CreadoPor = roatechIdentityUserRepo.GetAllInfoByEmail(this.User.Identity.Name).Single()
                });
          }
          try{
            _setRepo.Update(settings);
            LoggedUser = this.User.Identity.Name;
            _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Update", Section = "Settings - Templates", Description = "Tipo " + blt.Tipo + " archivo actualizado." });
            return Json(new{succes=true});
          } catch (Exception e) {
            return Json(new{success=false, data=e});
          }
        }
        return NoContent();
      }
      return NotFound();
    }

    [HttpGet]
    public IActionResult GetBulkLoaderTemplate(string tipo){
      if(User.Identity.IsAuthenticated){
        try{
          var bl = _bltRepo.GetByTipoAndVersion("1.0", tipo);
          if(!bl.Any())
            return NotFound();
          Response.Headers.Add("Content-Length", bl.Single().TamanoArchivo.ToString());
          Response.Headers.Add("Content-Disposition", "inline; filename=" + bl.Single().Tipo);
          return File(bl.Single().Archivo, "application/octet-stream", tipo+".xlsx");
        }
        catch(Exception e){
          return NotFound(e);
        }
      }
      return NotFound();
    }

    public async Task<IActionResult> OnPostAsync(string returnUrl = null) {
      returnUrl = returnUrl ?? Url.Content("~/");
      Console.WriteLine("\n\nEstoy en OnPost\n");
      return View();
    }

    public IActionResult PlatformSettings(){
      if(User.Identity.IsAuthenticated){
        try{
          return View(settings);
        }catch(Exception e){
          return View(e);
        }
      }
      return View();
    }

    [HttpPost]
    public IActionResult PSsave(Settings s){
      if(User.Identity.IsAuthenticated){
        if(s != null){
          settings.EmailHost = s.EmailHost;
          settings.EmailPort = s.EmailPort;
          settings.EmailUser = s.EmailUser;
          settings.EmailPass = s.EmailPass;
          settings.EmailEnableSSL = s.EmailEnableSSL;
          settings.UsoDePaquetes = s.UsoDePaquetes;
          settings.FaceApiUso = s.FaceApiUso;
          settings.FaceApiMinCantEntrenamiento = s.FaceApiMinCantEntrenamiento;
          settings.CustomVisionUso = s.CustomVisionUso;
          _setRepo.Update(settings);
        }
      }
      return RedirectToAction("PlatformSettings","Admin");
    }

    public IActionResult TrainModelFaceApi(){
      if(User.Identity.IsAuthenticated){
        ViewData["FaceApi"] = (bool)settings.FaceApiUso;
        ViewData["NumMin"] = (int)settings.FaceApiMinCantEntrenamiento;
        List<Persona> pL = new List<Persona>();
        foreach(RoatechIdentityUser riu in userManager.GetUsersInRoleAsync("Operador").Result){
          pL.Add(roatechIdentityUserRepo.GetAllInfoById(riu.Id).Single().per);
        }
        pL.Sort((a,b) => (a.Nombre+a.Paterno+a.Materno).CompareTo(b.Nombre+b.Paterno+b.Materno));
        return View(pL);
      }
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddFaceApi(TrainModelDTO newPerson){
      if(User.Identity.IsAuthenticated){
        var payload = "{'name':'"+newPerson.Nombre+"'}";
        var client = new RestClient("https://southcentralus.api.cognitive.microsoft.com/face/v1.0/persongroups/siges/persons");
        client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
        var request = new RestRequest(Method.POST);
        request.AddHeader("ContentType","application/json");
        request.AddHeader("Ocp-Apim-Subscription-Key","116aa6883b0e4cca8e644de51ce91d93");
        request.AddParameter(null, payload, ParameterType.RequestBody);
        var response = client.Execute(request);
        JObject data = JObject.Parse(response.Content);
        Persona p = perRepo.GetById(newPerson.PersonaId);
        p.FaceApiId = data.GetValue("personId").ToString();
        perRepo.Update(p);
        LoggedUser = this.User.Identity.Name;
        _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Update", Section = "Catálogo de Persona", Description = "Persona no. " + p.Id + " FaceApi actualizada." });
        return Json(new{success=true});
      }
      return Json(new{success=false});
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> TrainModel(TrainModelDTO t){
      if(User.Identity.IsAuthenticated){
        try{
          var url = "https://southcentralus.api.cognitive.microsoft.com/face/v1.0/persongroups/siges/persons/"+t.FaceId+"/persistedFaces";
          byte[] payload = null;
          using(var memoryStream = new MemoryStream()){
            await t.Photo.CopyToAsync(memoryStream);
            payload =memoryStream.ToArray();
          }
          var client = new RestClient(url);
          client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
          var request = new RestRequest(Method.POST);
          request.AddHeader("ContentType","application/octet-stream");
          request.AddHeader("Ocp-Apim-Subscription-Key","116aa6883b0e4cca8e644de51ce91d93");
          request.AddParameter("application/octet-stream", payload, ParameterType.RequestBody);
          var response = client.Execute(request);
          JObject data = JObject.Parse(response.Content);
          if(data != null){
            Persona p = perRepo.GetById(t.PersonaId);
            p.FaceApiCount += 1;
            perRepo.Update(p);
            LoggedUser = this.User.Identity.Name;
            _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Update", Section = "Catálogo de Persona", Description = "Persona no. " +t. PersonaId + " FaceApi actualizada." });
          }
          return Json(new{success=true, data=data});
        } catch (Exception e) {
          return Json(new{success=false, data=e});
        }
      }
      return RedirectToAction("AdminUsers", "Admin");
    }

    [HttpGet]
    public IActionResult ClienteIdentity(){
      if(User.Identity.IsAuthenticated){
        try{
          List<RoatechIdentityUser> cuentas = new List<RoatechIdentityUser>();
          cuentas.Sort((a,b) => (a.per.Nombre+a.per.Paterno+a.per.Materno).CompareTo(b.per.Nombre+b.per.Paterno+b.per.Materno));
          Dictionary<RoatechIdentityUser, Cliente> clientesIdentities = new Dictionary<RoatechIdentityUser, Cliente>();
          foreach(RoatechIdentityUser u in userManager.GetUsersInRoleAsync("Cliente").Result){
            RoatechIdentityUser riu = roatechIdentityUserRepo.GetAllInfoByEmail(u.Email).Single();
            cuentas.Add(riu);
            ClienteIdentity clId = clIdRepo.GetByRiuId(riu.Id);
            if(clId != null)
              clientesIdentities[riu] = cliRepo.GetById(clId.Cliente.Id);
          }
          ViewData["clientesIdentities"] = clientesIdentities;
          List<Cliente> clienteL = cliRepo.GetAll(true).OrderBy(r => r.RazonSocial).ToList();
          clienteL.Sort((a,b) => a.RazonSocial.CompareTo(b.RazonSocial));
          foreach(Cliente p in clienteL)
            Console.WriteLine("\nOrden de cliente: {0}", p.RazonSocial);
          Console.WriteLine();
          ViewData["clientes"] = clienteL;
          return View(cuentas);
        } catch (Exception e) {
          return Json(new{success=false, data=e});
        }
      }
      return RedirectToAction("AdminUsers", "Admin");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CISave(ClienteIdentityDTO cid){
      if(User.Identity.IsAuthenticated){
        if(cid != null){
          try{
            RoatechIdentityUser cuenta = roatechIdentityUserRepo.GetAllInfoByEmail(cid.email).Single();
            if(userManager.IsInRoleAsync(cuenta, "Cliente").Result){
              if(!context.Cliente.Any(r => r.Id == cid.clienteId))
                return Json(new{succes=false, data="No existe."});
              Cliente cliente = cliRepo.GetById(cid.clienteId);
              ClienteIdentity clienteIdentity = new ClienteIdentity{
                Cliente = cliente,
                        CuentaUsuario = cuenta,
                        Estatus = true,
                        FechaAdministrativa = System.DateTime.Now
              };
              ClienteIdentity clId = clIdRepo.GetByRiuId(cuenta.Id);
              int existe = -1;
              var cc = new ContactoCliente();
              cc.Cliente = cliente;
              cc.Contactos.Add(context.Persona.Single(r => r.Id == cuenta.per.Id));
              cc.Estatus = true;
              cc.FechaCreacion = DateTime.Now;
              cc.FechaModificacion = DateTime.Now;
              cc.CreadorPor = roatechIdentityUserRepo.GetAllInfoByEmail(this.User.Identity.Name).Single();
              cc.ModificadoPor = roatechIdentityUserRepo.GetAllInfoByEmail(this.User.Identity.Name).Single();
              context.ContactoCliente.Add(cc);
              context.SaveChanges();
              if(clId != null)
                clIdRepo.Delete(clId);
              clIdRepo.Insert(clienteIdentity);
              return Json(new{success=true, data="se guardó correctamente ClienteIdentity"});
            } else {
              return Json(new{success=false, data="No fue posible asociar la cuenta al cliente. Es necesario tener un role adecuado."});
            }
          } catch (Exception e) {
            return Json(new{success=false, data=e});
          }
        }
      }
      return View();
    }

    [HttpGet]
    public IActionResult SemaphoreParams(){
      if(User.Identity.IsAuthenticated){
        try{
          return View(semRepo.GetAll().ToList());
        } catch (Exception e) {
          return Json(new{success=false, data=e});
        }
      }
      return View();
    }

    [HttpPost, ActionName("SemaphoreParams")]
    [ValidateAntiForgeryToken]
    public IActionResult SemaphoreConf(String ID, String key, String value){
       if(User.Identity.IsAuthenticated){
        if(!String.IsNullOrEmpty(ID) && !String.IsNullOrEmpty(key) && !String.IsNullOrEmpty(value)){
          try{
            //JObject jo = JObject.Parse(sp);
            //Console.WriteLine("\n\n\t"+jo.Property("key").Value.ToString()+" "+jo.Property("value").Value.ToString()+"\n");
            //if(1 == Int32.Parse(jo.Property("id").Value.ToString())){
            if(1 == Int32.Parse(ID)){
              //SemaphoreParams sempar = semRepo.GetById(Int32.Parse(jo.Property("id").Value.ToString()));
              SemaphoreParams sempar = semRepo.GetById(Int32.Parse(ID));
              //switch(jo.Property("key").Value.ToString()) {
              switch(key) {
                case "llegadaVerde":
                  //sempar.LlegadaVerde = Int32.Parse(jo.Property("value").Value.ToString());
                  sempar.LlegadaVerde = Int32.Parse(value);
                  break;
                case "llegadaAmarillo":
                  sempar.LlegadaAmarillo = Int32.Parse(value);
                  break;
                case "llegadaRojo":
                  sempar.LlegadaRojo = Int32.Parse(value);
                  break;
                case "salidaVerde":
                  sempar.SalidaVerde = Int32.Parse(value);
                  break;
                case "salidaAmarillo":
                  sempar.SalidaAmarillo = Int32.Parse(value);
                  break;
                case "salidaRojo":
                  sempar.SalidaRojo = Int32.Parse(value);
                  break;
              }
              semRepo.Update(sempar);
              LoggedUser = this.User.Identity.Name;
              //_bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Update", Section = "Configuración de Semaforo", Description = "Actualización de " + jo.Property("key").Value.ToString() + " a " + jo.Property("value").Value.ToString() + "."});
              _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Update", Section = "Configuración de Semaforo", Description = "Actualización de " + key + " a " + value + "."});
              return Json(new{success=true});
            }
          } catch (Exception e) {
            return Json(new{success=false, data=e});
          }
        }
        return NotFound();
      }
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole(string role) {
      if (User.Identity.IsAuthenticated) {
        if (!String.IsNullOrEmpty(role)) {
          try {
            await roleManager.CreateAsync(new IdentityRole(role));
            return new JsonResult(value: true);
          } catch(Exception e ){
            return new JsonResult(value: e);
          }
        }
      }
      return View();
    }

    [HttpGet]
    public IActionResult GetRoles() {
      if (User.Identity.IsAuthenticated) {
        List<IdentityRole> roles = new List<IdentityRole>();
        var ro = from r in roleManager.Roles select r;
        foreach(IdentityRole i in ro){
          roles.Add(i);
        }
        ViewData["roles"] = roles;
      }
      return View();
    }

    [HttpPost]
    public IActionResult DeleteUser(string id){
      //roatechIdentityUserRepo.
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> AsignRoleToUser(string role, string user) {
      if(User.Identity.IsAuthenticated) {
        if (!String.IsNullOrEmpty(role) && !String.IsNullOrEmpty(user)) {
          try {
            var riuser = await userManager.FindByEmailAsync(user);
            if(userManager.IsInRoleAsync(riuser, "Default").Result){
              await userManager.RemoveFromRoleAsync(riuser, "Default");
              await userManager.AddToRoleAsync(riuser, role);
            }
            if(userManager.GetRolesAsync(riuser).Result[0] != role)
              await userManager.RemoveFromRoleAsync(riuser, userManager.GetRolesAsync(riuser).Result[0]);
              await userManager.AddToRoleAsync(riuser, role);
          } catch (Exception e) {
            return new JsonResult(value: e);
          }
          return new JsonResult(value: true);
        }
      }
      return View();
    }

    public IActionResult AdminRoles() {
      if(User.Identity.IsAuthenticated) {
        List<string> riuIdsList = new List<string>();
        List<RoatechIdentityUser> riuList = new List<RoatechIdentityUser>();
        Dictionary<RoatechIdentityUser, List<string>> usuarioRoles = new Dictionary<RoatechIdentityUser, List<string>>();
        var idsList = from unn in userManager.Users where unn.Email != "" select unn;
        foreach ( RoatechIdentityUser riuId in idsList) {
          riuIdsList.Add(riuId.Id);
        }
        riuIdsList.ForEach(delegate(string id){
            var roaIdeUsrL = from r in roatechIdentityUserRepo.GetAllInfoById(id) select r;
            foreach ( RoatechIdentityUser x in roaIdeUsrL){
            riuList.Add(x);
            }
            });
        riuList.ForEach(delegate(RoatechIdentityUser riu){
            usuarioRoles.Add(riu, ((List<string>) userManager.GetRolesAsync(riu).Result));
            });
        riuList.Sort();
        ViewData["riuList"] = riuList;
        ViewData["riuDicc"] = usuarioRoles;
        ArrayList roles = new ArrayList();
        foreach(IdentityRole ro in roleManager.Roles.OrderBy(r => r.Name).ToList()){
          roles.Add(ro.Name.ToString());
        }
        ViewData["roles"] = roles.ToArray(typeof( string ));
      }
      return View();
    }

    public IActionResult CatalogueRoles() {
      ViewData["roles"] = roleManager.Roles.OrderBy(r => r.Name).ToList();
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> DeleteRole(string name){
      if(User.Identity.IsAuthenticated){
        if(!String.IsNullOrEmpty(name)){
          try {
            var role = await roleManager.FindByNameAsync(name);
            await roleManager.DeleteAsync(role);
            return new JsonResult(value: true);
          } catch (Exception e ) {
            return new JsonResult(value: e);
          }
        }
      }
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> EditRole(string oldName, string newName) {
      if(User.Identity.IsAuthenticated){
        if(!String.IsNullOrEmpty(oldName) && ! String.IsNullOrEmpty(newName)){
          try {
            var role = await roleManager.FindByNameAsync(oldName);
            role.Name = newName;
            await roleManager.UpdateAsync(role);
            return new JsonResult(value: true);
          } catch (Exception e) {
            return new JsonResult(value: e);
          }
        }
      }
      return View();
    }

    [HttpPost]
    public IActionResult SaveRIU(RoatechIdentityUser riu){
      if(User.Identity.IsAuthenticated){
        try{
          RoatechIdentityUser user = roatechIdentityUserRepo.GetAllInfoByEmail(riu.per.Email).Single();
          user.per.Nombre = riu.per.Nombre;
          user.per.Paterno = riu.per.Paterno;
          user.per.Materno = riu.per.Materno;
          user.per.CURP = riu.per.CURP;
          user.per.RFC = riu.per.RFC;
          user.per.Telefono = riu.per.Telefono;
          //user.per.TelefonoContacto = riu.per.TelefonoContacto;
          roatechIdentityUserRepo.Update(user);
          return RedirectToAction("AdminUsers", "Admin");
        }catch(Exception e){
          return Json(new{success=false, data=e.ToString()});
        }
      }
      return NotFound();
    }

    [HttpGet]
    public IActionResult EditRIU(string email){
      if(User.Identity.IsAuthenticated){
        if(!String.IsNullOrEmpty(email)){
          try{
            RoatechIdentityUser user = roatechIdentityUserRepo.GetAllInfoByEmail(email).Single();
            ViewData["usuario"] = user;
          }catch(Exception e){
            Console.WriteLine("\n\n\t *** ERRORRR " + email + "\n");
            return new JsonResult(value: e);
          }
        }
      }
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> EmailConfirmation(string email, string name){
      if(User.Identity.IsAuthenticated){
        if(!String.IsNullOrEmpty(email)){
          try{
            string EmailConfirmationUrl = "";
            var user = await userManager.FindByEmailAsync(email);
            if(!await userManager.IsEmailConfirmedAsync(user)){
              var userId = await userManager.GetUserIdAsync(user);
              var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
              code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
              EmailConfirmationUrl = Url.Page(
                  "/Account/ConfirmEmail",
                  pageHandler: null,
                  values: new { area = "Identity", userId = userId, code = code },
                  protocol: Request.Scheme);
              Notifica notifica = new Utilities.Notifica(_emailConf, "Confirmación de correo electrónico", new MimeKit.Multipart("Mixed"), settings);
              notifica.NuevaCuentaNotifica(EmailConfirmationUrl, "********", email, name);

              return Json(new{success=true, data=new{mensaje="Se reenvió correo de confirmación a " + email, nombre=name}});
            }
            return Json(new{success=true, data=new{mensaje="No es necesario reenviar correo de confirmación", nombre=name}});
          }catch (Exception e){
            return Json(new{success=false, data=e});
          }
        }
      }
      return RedirectToAction("AdminUsers", "Admin");
    }

    public IActionResult SetLockout(string email){
      if(User.Identity.IsAuthenticated){
        if(!String.IsNullOrEmpty(email)){
          Console.WriteLine("\n\nbloqueado: " + userManager.IsLockedOutAsync(userManager.FindByEmailAsync(email).Result).Result);
          if(userManager.IsLockedOutAsync(userManager.FindByEmailAsync(email).Result).Result){
            var conn = RelationalDatabaseFacadeExtensions.GetDbConnection(context.Database);
            if(((System.Data.SqlClient.SqlConnection)conn).State == System.Data.ConnectionState.Open)
              ((System.Data.SqlClient.SqlConnection)conn).Close();
            ((System.Data.SqlClient.SqlConnection) conn).Open();
            SqlCommand cmd = new SqlCommand("update aspnetusers set lockoutend = current_timestamp where email = @Email", (System.Data.SqlClient.SqlConnection) conn);
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar);
            cmd.Parameters["@Email"].Value = email;
            cmd.ExecuteNonQuery();
          }
          return RedirectToAction("AdminUsers", "Admin");
        }
      }
      return RedirectToAction("AdminUsers", "Admin");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteRIU(string email){
      if(User.Identity.IsAuthenticated){
        if(!String.IsNullOrEmpty(email)){
          try{
            string userId = "";
            var conn = RelationalDatabaseFacadeExtensions.GetDbConnection(context.Database);
            if(((System.Data.SqlClient.SqlConnection)conn).State == System.Data.ConnectionState.Open)
              ((System.Data.SqlClient.SqlConnection)conn).Close();
            ((System.Data.SqlClient.SqlConnection) conn).Open();
            SqlCommand cmd = new SqlCommand("select id from aspnetusers where email = @Email", (System.Data.SqlClient.SqlConnection) conn);
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar);
            cmd.Parameters["@Email"].Value = email;
            SqlDataReader dataReader = cmd.ExecuteReader();
            if(dataReader.HasRows){
              while(dataReader.Read()){
                userId = (string)dataReader[0];
              }
            }
            if(((System.Data.SqlClient.SqlConnection)conn).State == System.Data.ConnectionState.Open)
              ((System.Data.SqlClient.SqlConnection)conn).Close();
            if(!String.IsNullOrEmpty(userId)){
              ((System.Data.SqlClient.SqlConnection) conn).Open();
              cmd = new SqlCommand("delete from clienteidentity where cuentausuarioid = @Id", (System.Data.SqlClient.SqlConnection) conn);
              cmd.Parameters.Add("@Id", SqlDbType.NVarChar);
              cmd.Parameters["@Id"].Value = userId;
              int rows = cmd.ExecuteNonQuery();
              Console.WriteLine("\n\nLineas afectadas? "+rows+"\n");
              if(((System.Data.SqlClient.SqlConnection)conn).State == System.Data.ConnectionState.Open)
                ((System.Data.SqlClient.SqlConnection)conn).Close();
            }
            var user = await userManager.FindByEmailAsync(email);
            var result = await userManager.DeleteAsync(user);
            Persona p = perRepo.GetByEmail(email).Single();
            p.Estatus = false;
            p.Email = " ";
            perRepo.Update(p);
            return new JsonResult(value: true);
          }catch (Exception e ) {
            return new JsonResult(value: e);
          }
        }
      }
      return View();
    }

    public IActionResult AdminUsers() {
      if(User.Identity.IsAuthenticated){
        List<string> ids = new List<string>();
        List<RoatechIdentityUser> riuList = new List<RoatechIdentityUser>();
        Dictionary<RoatechIdentityUser, List<string>> usuarioRoles = new Dictionary<RoatechIdentityUser, List<string>>();
        var usrQueryable = from u in userManager.Users where u.Email != "" select u;
        foreach( RoatechIdentityUser id in usrQueryable){
          ids.Add(id.Id);
        }
        ids.ForEach(delegate(string i){
            var riuQueryable = from r in roatechIdentityUserRepo.GetAllInfoById(i) select r;
            foreach( RoatechIdentityUser x in riuQueryable){
            riuList.Add(x);
            }
            });
        riuList.ForEach(delegate(RoatechIdentityUser riu){
            usuarioRoles.Add(riu, ((List<string>) userManager.GetRolesAsync(riu).Result));
            });
        riuList.Sort();
        ViewData["riuList"] = riuList;
        ViewData["riuDicc"] = usuarioRoles;
      }
      return View();
    }
  }
}
