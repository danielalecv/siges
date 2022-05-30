using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data;
using System.IO;
using System.Globalization;

using siges.Models;
using siges.DTO;
using siges.Repository;
using siges.Utilities;
using siges.Areas.Identity.Data;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

using MimeKit;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using RestSharp;

namespace siges.Controllers {
  [Authorize(Roles = "Operador, SuperUser")]
  public class OperadorController : Controller {
    private readonly IHostingEnvironment _hostingEnvironment;
    private List<Persona> _listOperador;
    private readonly UserManager<RoatechIdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IEmailConfiguration _emailConf;
    private IPersonaRepository _pRepo;
    private IOrdenPersona _opRepo;
    private IOperador _operRepo;
    private IOrdenServicioRepository _osRepo;
    private Persona operadorP;
    private Operador operadorActual;
    private List<string> EstadosOperador;
    private int opId;
    private Settings settings;
    private readonly IRoatechIdentityUserRepo roaIdenUsrRepo;

    public OperadorController(UserManager<RoatechIdentityUser> userManager, RoleManager<IdentityRole> roleManager, IOrdenPersona opRepo, IOrdenServicioRepository osRepo, IPersonaRepository pRepo, IOperador operRepo, IHostingEnvironment hEnv, IEmailConfiguration emailConf, ISettingsRepository set, IRoatechIdentityUserRepo _riuR) {
      _pRepo = pRepo;
      _opRepo = opRepo;
      _userManager = userManager;
      _roleManager = roleManager;
      _operRepo = operRepo;
      _osRepo = osRepo;
      _hostingEnvironment = hEnv;
      _emailConf = emailConf;
      operadorActual = new Operador();
      settings = set.GetByVersion("DAMSA");
      roaIdenUsrRepo = _riuR;
    }

    public async Task<IActionResult> Index() {
      var usr = await _userManager.GetUserAsync(HttpContext.User);
      Console.WriteLine("\n\nuser: "+ usr + "\n");
      opId = _pRepo.GetByEmail((await _userManager.GetUserAsync(HttpContext.User)).ToString()).Single().Id;
      var op =  _opRepo.GetOSbyIdP(opId).ToList();
      List<OrdenPersona> ordPer = new List<OrdenPersona>();
      foreach (var ordenPerso in op) {
        if(ordenPerso.OrdenServicio.Estatus)
          if(ordenPerso.OrdenServicio.FechaInicio != null && ordenPerso.OrdenServicio.FechaFin != null)
            if(ordenPerso.OrdenServicio.EstatusServicio == "programado" || ordenPerso.OrdenServicio.EstatusServicio == "reprogramado" || ordenPerso.OrdenServicio.EstatusServicio == "solicitado") {
              ordPer.Add(ordenPerso);
            }
      }
      foreach(OrdenPersona ordenpersona in ordPer){
        Console.WriteLine("\n\n\t"+ordenpersona.OrdenServicio.Folio+" "+ordenpersona.OrdenServicio.EstatusServicio+" "+ordenpersona.Persona.Nombre+ordenpersona.Persona.Paterno+"\n");
      }
      JsonSerializerSettings jsonSettings = new JsonSerializerSettings();
      jsonSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
      jsonSettings.NullValueHandling = NullValueHandling.Ignore;
      string output = JsonConvert.SerializeObject(ordPer, jsonSettings);
      ViewData["ordenesOperador"] = output;
      ViewData["ordenesPersona"] = ordPer;
      ViewData["OrdenServicioEnTiempo"] = JsonConvert.SerializeObject(getOSenTiempoOperador().Result, jsonSettings);
      RoatechIdentityUser riu = roaIdenUsrRepo.GetAllInfoByEmail(User.Identity.Name).Single();
      ViewData["riu"] = riu;
      return View();
    }

    [HttpPost]
    public IActionResult ReporteActividad(OperadorDTO operador){
      Console.WriteLine("\n\nOperadorDTO {0} {1} {2} {3} {4} {5}\n\n", operador.OrdenServicioId, operador.PersonaId, operador.NuevoEstado, operador.ComentarioNuevoEstado, operador.Hora, operador.ArchivosEvidencia);
      List<Operador> osAtendido = _operRepo.GetAtencionServicio(operador.OrdenServicioId).ToList();
      ViewData["osAtendido"] = osAtendido;
      return View();
    }

    private async Task<int> getOperadorId(){
      return _pRepo.GetByEmail((await _userManager.GetUserAsync(HttpContext.User)).ToString()).Single().Id;
      //return _pRepo.GetByEmail("juan@correo.com").Single().Id;
    }

    private async Task<List<OrdenPersona>> getOSenTiempoOperador(){
      List<OrdenPersona> ordSerOper = new List<OrdenPersona>();
      Console.WriteLine(" _opRepo.GetOSbyIdP(getOperadorId().Result).ToList(): "+ _opRepo.GetOSbyIdP(getOperadorId().Result).ToList().Count+"\n");
      foreach(var ordenPersona in _opRepo.GetOSbyIdP(getOperadorId().Result).ToList()){
        if(ordenPersona.OrdenServicio.Estatus){
          if(ordenPersona.OrdenServicio.FechaInicio != null && ordenPersona.OrdenServicio.FechaFin != null){
            if(ordenPersona.OrdenServicio.EstatusServicio == "programado" || ordenPersona.OrdenServicio.EstatusServicio == "reprogramado" || ordenPersona.OrdenServicio.EstatusServicio == "solicitado"){

                  //ordSerOper.Add(ordenPersona);// deja pasar todas las fechas disponibles

              switch(DateTime.Compare(DateTime.Now, (DateTime)ordenPersona.OrdenServicio.FechaInicio)){
                case -1: //Hoy es antes de FechaInicio
                  ViewData["estados"] = "destiempo";
                  break;
                case 0: //Hoy es el mismo de FechaInicio
                  ordSerOper.Add(ordenPersona);
                  ViewData["estados"] = "entiempo";
                  break;
                case 1: //Hoy es después de FechaInicio
                  switch(DateTime.Compare(DateTime.Now, ((DateTime)ordenPersona.OrdenServicio.FechaFin).AddDays(15))){
                    case -1: //Hoy es antes de FechaFin
                      ordSerOper.Add(ordenPersona);
                      ViewData["estados"] = "entiempo";
                      break;
                    case 0: //Hoy es el mismo de FechaFin
                      ordSerOper.Add(ordenPersona);
                      ViewData["estados"] = "entiempo";
                      break;
                    case 1: //Hoy es después de FechaFin
                      ViewData["estados"] = "destiempo";
                      break;
                  }
                  break;
              }
            }
          }
        }
      }
      return ordSerOper;
    }

    [HttpPost]
    public IActionResult CambiarEstado(OperadorDTO operador) {
      Console.WriteLine("-------------");
      Console.WriteLine(operador.OrdenServicioId);
      Console.WriteLine("-------------");
      
      foreach (var ordenServicio in _osRepo.GetAll(true).OrderBy(r => r.FechaInicio).ToList()) {
        if (ordenServicio.Id == operador.OrdenServicioId) {
          ViewData["ordenServicio"] = ordenServicio;
        }
      }
      List<Operador> listOperador = _operRepo.GetAtencionServicio(operador.OrdenServicioId).ToList();
      if(listOperador.Count == 0){
        ViewData["estados"] = "todos";
      } else {
        foreach(Operador oper in listOperador) {
          foreach(Operador.Estado esta in oper.EstadoN){
            switch(DateTime.Compare(DateTime.Today, oper.Hora.Date)){
              case -1: //Hoy es antes que evidencia
                ViewData["estados"] = "vacio";
                ViewData["diff"] = DateTime.Today.Subtract(oper.Hora.Date);
                break;
              case 0: //Hoy es mismo que evidencia
                if(settings.FaceApiUso){
                  if(esta.NuevoEstado == "asistencia")
                    ViewData["estados"] = "todos";
                }
                if(settings.CustomVisionUso){
                  if(esta.NuevoEstado == "revequiposeguridad")
                    ViewData["estados"] = "sinsitio";
                }
                if(esta.NuevoEstado == "sitio")
                  ViewData["estados"] = "sinsitio";
                if(esta.NuevoEstado == "atendido")
                  ViewData["estados"] = "sinsitioyatendido";
                ViewData["diff"] = DateTime.Today.Subtract(oper.Hora.Date);
                break;
              case 1: //Hoy es después que evidencia
                ViewData["estados"] = "todos";
                ViewData["diff"] = DateTime.Today.Subtract(oper.Hora.Date);
                break;
            }
          }
        }
      }
      ViewData["customvisionuso"] = (bool)settings.CustomVisionUso;
      ViewData["faceapiuso"] = (bool)settings.FaceApiUso;
      ViewData["personaId"] = this.getOperadorId().Result;
      return View();
    }

    private static string getExtension(string fileName){
       string[] words = fileName.Split(new char[]{'.'});
       if(!(words.Length != 2)){
         return words[1];
       }
       Console.WriteLine("\n\nWARNING: Nombre de archivo {0}\n\tSe utilizó la extensión \"ext\"\n\n", fileName);
       return "ext";
    }

    private string createNameOfFile(IFormFile file, OperadorDTO osDTO, int identificador){
      Random ran = new Random();
      byte[] n = new byte[2];
      ran.NextBytes(n);
      OrdenServicio os = _osRepo.GetByIdOS(osDTO.OrdenServicioId);
      string ext = System.IO.Path.GetExtension(file.FileName);
      return String.Format("{0}{3}{1}{5}{6}-{4}{2}", os.Folio, DateTime.Now.ToString("d", CultureInfo.CreateSpecificCulture("es-ES")), ext, Utilities.Tools.OperadorNuevoEstadoStr(osDTO.NuevoEstado), identificador, n[0], n[1]);
    }

    private string tryGetMetadata(string fileName, Dictionary<string,JObject> metadata){
      JObject value = new JObject();
      if(metadata.TryGetValue(fileName, out value)){
        return value.ToString();
      } else {
        return null;
      }
      return null;
    }

    private string tryGetMetadataBi(string fileName, Dictionary<string, string> metadataBi){
      string value = "";
      if(metadataBi.TryGetValue(fileName, out value)){
        return value;
      } else {
        return null;
      }
      return null;
    }

    private EstructuraExifBi tryGetEstructuraExifBi(string fileName, Dictionary<string, string> metadataBi){
      EstructuraExifBi estExifBi = new EstructuraExifBi();
      string json = "";
      if(metadataBi.TryGetValue(fileName, out json)){
        ExifBi ex = JsonConvert.DeserializeObject<ExifBi>(json);
        estExifBi.GPSLongitudeId = typeof(ExifBi.gPSLongitude).IsInstanceOfType(ex.GPSLongitude)?ex.GPSLongitude.id:0;
        estExifBi.GPSLongitudeValue = typeof(ExifBi.gPSLongitude).IsInstanceOfType(ex.GPSLongitude)?ExifBi.ValueToString(ex.GPSLongitude.value):"";
        estExifBi.GPSLongitudeDescription = typeof(ExifBi.gPSLongitude).IsInstanceOfType(ex.GPSLongitude)?ex.GPSLongitude.description:0.0;
        estExifBi.GPSLatitudeId = typeof(ExifBi.gPSLatitude).IsInstanceOfType(ex.GPSLatitude)?ex.GPSLatitude.id:0;
        estExifBi.GPSLatitudeValue = typeof(ExifBi.gPSLatitude).IsInstanceOfType(ex.GPSLatitude)?ExifBi.ValueToString(ex.GPSLatitude.value):"";
        estExifBi.GPSLatitudeDescription = typeof(ExifBi.gPSLatitude).IsInstanceOfType(ex.GPSLatitude)?ex.GPSLatitude.description:"";
        estExifBi.DateTimeOriginalId = typeof(ExifBi.dateTimeOriginal).IsInstanceOfType(ex.DateTimeOriginal)?ex.DateTimeOriginal.id:0;
        if(typeof(ExifBi.dateTimeOriginal).IsInstanceOfType(ex.DateTimeOriginal))
          for(int i = 0; i < ex.DateTimeOriginal.value.Length; i++){
            estExifBi.DateTimeOriginal = DateTime.Parse(ex.DateTimeOriginal.value[i].Split(new char[]{' '})[0].Replace(':', '/') + " " + ex.DateTimeOriginal.value[i].Split(new char[]{' '})[1]);
          }
        estExifBi.GPSAltitudeId = typeof(ExifBi.gPSAltitude).IsInstanceOfType(ex.GPSAltitude)?ex.GPSAltitude.id:0;
        estExifBi.GPSAltitudeValue = typeof(ExifBi.gPSAltitude).IsInstanceOfType(ex.GPSAltitude)?ExifBi.ValueToString(ex.GPSAltitude.value):"";
        estExifBi.GPSAltitudeDescription = typeof(ExifBi.gPSAltitude).IsInstanceOfType(ex.GPSAltitude)?ex.GPSAltitude.description:"";
        estExifBi.GPSLongitudeRefId = typeof(ExifBi.gPSLongitudeRef).IsInstanceOfType(ex.GPSLongitudeRef)?ex.GPSLongitudeRef.id:0;
        estExifBi.GPSLongitudeRefValue = typeof(ExifBi.gPSLongitudeRef).IsInstanceOfType(ex.GPSLongitudeRef)?ExifBi.ValueToString(ex.GPSLongitudeRef.value):"";
        estExifBi.GPSLongitudeRefDescription = typeof(ExifBi.gPSLongitudeRef).IsInstanceOfType(ex.GPSLongitudeRef)?ex.GPSLongitudeRef.description:"";
        estExifBi.GPSLatitudeRefId = typeof(ExifBi.gPSLatitudeRef).IsInstanceOfType(ex.GPSLatitudeRef)?ex.GPSLatitudeRef.id:0;
        estExifBi.GPSLatitudeRefValue = typeof(ExifBi.gPSLatitudeRef).IsInstanceOfType(ex.GPSLatitudeRef)?ExifBi.ValueToString(ex.GPSLatitudeRef.value):"";
        estExifBi.GPSLatitudeRefDescription = typeof(ExifBi.gPSLatitudeRef).IsInstanceOfType(ex.GPSLatitudeRef)?ex.GPSLatitudeRef.description:"";
        estExifBi.GPSAltitudeRefId = typeof(ExifBi.gPSAltitudeRef).IsInstanceOfType(ex.GPSAltitudeRef)?ex.GPSAltitudeRef.id:0;
        estExifBi.GPSAltitudeRefValue = typeof(ExifBi.gPSAltitudeRef).IsInstanceOfType(ex.GPSAltitudeRef)?ex.GPSAltitudeRef.value:0;
        estExifBi.GPSAltitudeRefDescription = typeof(ExifBi.gPSAltitudeRef).IsInstanceOfType(ex.GPSAltitudeRef)?ex.GPSAltitudeRef.description:"";
      }
      return estExifBi;
    }

    [HttpPost]
    public async Task<IActionResult> GuardarEstado(OperadorDTO operador) {
      Multipart multipart = new Multipart("mixed");
      operadorActual = new Operador(){ Estatus = true, OrdenServicio = _osRepo.GetByIdOS(operador.OrdenServicioId), Persona = _pRepo.GetById(operador.PersonaId), Hora = DateTime.ParseExact(operador.Hora, "yyyy MM dd HH:mm:ss", CultureInfo.InvariantCulture), EstadoN = new List<Operador.Estado>(), FechaAdministrativa = DateTime.Now};
      operadorActual.EstadoN.Add(new Operador.Estado(){NuevoEstado = operador.NuevoEstado, ComentarioNuevoEstado = operador.ComentarioNuevoEstado, ArchivosEvidencia = new List<Operador.Estado.Archivo>()});
      Dictionary<string, JObject> metadata = new Dictionary<string, JObject>();
      Dictionary<string, string> metadataBi = new Dictionary<string, string>();
      if(!String.IsNullOrEmpty(operador.Exif)){
        try{
          foreach( var exif in JArray.Parse("[" + operador.Exif + "]")){
            JObject e = JObject.Parse(exif.ToString());
            metadata.Add(e.Property("FileName").Value.ToString(), e);
            ExifBi eb = JsonConvert.DeserializeObject<ExifBi>(exif.ToString());
            metadataBi.Add(e.Property("FileName").Value.ToString(), JsonConvert.SerializeObject(eb) );
            //Console.WriteLine("\n\n\tINFO: " + metadataBi[e.Property("FileName").Value.ToString()] + " <-> " + JsonConvert.SerializeObject(eb) + " <-> " + e.Property("DateTimeOriginal").Value.ToString() + "\n");
          }
        } catch (Exception e) {
          Console.WriteLine("\n\n\tERROR: " + e + "\n\n");
        }
      }
      if(operador.ArchivosEvidencia != null && operador.ArchivosEvidencia.Count > 0){
        int count = 1;
        foreach(var file in operador.ArchivosEvidencia){
          if(file.Length > 0){
            if(file.Length <= 20971520){
              // TODO: get file path from request
              using(var memoryStream = new MemoryStream()){
                await file.CopyToAsync(memoryStream);
                var cvResult = tryGetCustomVisionApi(operador.NuevoEstado, memoryStream.ToArray());
                operadorActual.EstadoN[0].ArchivosEvidencia.Add(new Operador.Estado.Archivo(){
                    Path = "PorElMomentoNoLoTengo", 
                    Type = file.ContentType, 
                    Name = createNameOfFile(file, operador, count), 
                    Exif = tryGetMetadata(file.FileName, metadata), 
                    ExifBi = tryGetMetadataBi(file.FileName, metadataBi), 
                    Size = file.Length, 
                    File=memoryStream.ToArray(), 
                    LastModified = DateTime.Now,
                    EstructuraExif = tryGetEstructuraExifBi(file.FileName, metadataBi),
                    FaceApiFinalResponse = tryGetFaceApi(operador.NuevoEstado, memoryStream.ToArray()),
                    CustomVisionResult = cvResult,
                    EstructuraCustomVisionResult = tryGetEstructuraCustomVision(file.FileName, cvResult)
                });
              }
              multipart.Add(new MimePart(file.ContentType){
                  Content = new MimeContent(file.OpenReadStream(), ContentEncoding.Default),
                  ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                  ContentTransferEncoding = ContentEncoding.Base64,
                  FileName = createNameOfFile(file, operador, count)
                  });
              count++;
            } else {
              //TODO Notificar que no es posible almacenar archivos mayores a 20MB
              Console.WriteLine("\n\nERROR: {0} imposible de almacenar - tamaño {1} MB\n", file.FileName, file.Length/20971520);
            }
          }
        }
        ViewData["operador"] = operadorActual;
      } else {
        ViewData["operador"] = operadorActual;
      }
      _operRepo.Insert(operadorActual);
      Persona persona = _pRepo.GetById(operador.PersonaId);
      OrdenServicio ordenServicio = _osRepo.GetByIdOS(operadorActual.OrdenServicio.Id);
      List<Operador> osAtendido = _operRepo.GetAtencionServicio(ordenServicio.Id).ToList();
      if(osAtendido == null || osAtendido.Count == 0){
        osAtendido = new List<Operador>();
        osAtendido.Add(operadorActual);
      }
      if(operador.NuevoEstado == "noatendido"){
        if(siges.Utilities.Tools.ValidaCamposCompletosOSAtendidos(osAtendido)){
          Notifica notifica = new Utilities.Notifica(_emailConf, _userManager, _roleManager, "Actualización de estado - Reporte actividad", multipart, this.User.Identity.Name.ToString(), settings);
          //notifica.OrdenServicioNotifica("cliente", "actualiza", osAtendido, ordenServicio, null);
          notifica.OrdenServicioNotifica("supervisor", "actualiza", osAtendido, ordenServicio, null);
          notifica.OrdenServicioNotifica("ventas", "actualiza", osAtendido, ordenServicio, null);
        }
      } else if(operador.NuevoEstado == "atendido") {
        if(siges.Utilities.Tools.ValidaCamposCompletosOSAtendidos(osAtendido)){
          Notifica notifica = new Utilities.Notifica(_emailConf, _userManager, _roleManager, "Actualización de estado - Atendido", multipart, this.User.Identity.Name.ToString(), settings);
          //notifica.OrdenServicioNotifica("cliente", "atendido", osAtendido, ordenServicio, null);
          notifica.OrdenServicioNotifica("supervisor", "atendido", osAtendido, ordenServicio, null);
          notifica.OrdenServicioNotifica("ventas", "atendido", osAtendido, ordenServicio, null);
        }
      } else if (operador.NuevoEstado == "sitio"){
        if(siges.Utilities.Tools.ValidaCamposCompletosOSAtendidos(osAtendido)){
          Notifica notifica = new Utilities.Notifica(_emailConf, _userManager, _roleManager, "Actualización de estado - Sitio", multipart, this.User.Identity.Name.ToString(), settings);
          //notifica.OrdenServicioNotifica("cliente", "sitio", osAtendido, ordenServicio, null);
          notifica.OrdenServicioNotifica("supervisor", "sitio", osAtendido, ordenServicio, null);
          notifica.OrdenServicioNotifica("ventas", "actualiza", osAtendido, ordenServicio, null);
        }
      } else if (operador.NuevoEstado == "reporteInterno") {
        if(siges.Utilities.Tools.ValidaCamposCompletosOSAtendidos(osAtendido)){
          Notifica notifica = new Utilities.Notifica(_emailConf, _userManager, _roleManager, "Reporte Interno", multipart, this.User.Identity.Name.ToString(), settings);
          notifica.OrdenServicioNotifica("supervisor", "reporteInterno", osAtendido, ordenServicio, null);
        }
      }
      List<Operador> atencionServicio = _operRepo.GetAtencionServicio(operadorActual.OrdenServicio.Id).ToList();
      ViewData["atencionServicio2"] = atencionServicio;
      ViewData["ordenServicio"] = ordenServicio;
      return View();
    }

    private List<EstructuraCustomVision> tryGetEstructuraCustomVision(string fileName, string cvResult){
      List<EstructuraCustomVision> ecvr = new List<EstructuraCustomVision>();
      if(!String.IsNullOrEmpty(cvResult)){
        CustomVision cv = JsonConvert.DeserializeObject<CustomVision>(cvResult);
        if(cv.predictions.Count > 0){
          foreach(var p in cv.predictions){
            ecvr.Add(new EstructuraCustomVision{
                BoundingBoxHeight = p.boundingBox.height,
                BoundingBoxLeft = p.boundingBox.left,
                BoundingBoxTop = p.boundingBox.top,
                BoundingBoxWidth = p.boundingBox.width,
                Probability = p.probability,
                TagId = p.tagId,
                TagName = p.tagName
                });
          }
          return ecvr;
        }
      }
      return ecvr;
    }

    private string tryGetCustomVisionApi(string estado, byte[] photo){
      if(String.Equals(estado, "revequiposeguridad")){
        var client = new RestClient("https://customvisiges.cognitiveservices.azure.com/customvision/v3.0/Prediction/47768834-6254-4135-89e4-0ddb5f30b25c/detect/iterations/Iteration8/image");
        client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
        var request = new RestRequest(Method.POST);
        request.AddHeader("Content-Type", "application/octet-stream");
        request.AddHeader("Prediction-Key","d7542f74741b41f6a57ee126f9d2cf0f");
        request.AddParameter("application/octet-stream", photo, ParameterType.RequestBody);
        var response = client.Execute(request);
        Console.WriteLine("\n\nResponse 1: {0}\n", response.Content);
        return response.Content;
      }
      return "";
    }
    
    private string tryGetFaceApi(string estado, byte[] photo){
      if(String.Equals(estado, "asistencia") && photo != null){
        var client = new RestClient("https://southcentralus.api.cognitive.microsoft.com/face/v1.0/detect?returnFaceId=true&returnFaceAttributes=age,gender");
        client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
        var request = new RestRequest(Method.POST);
        request.AddHeader("Content-Type","application/octet-stream");
        request.AddHeader("Ocp-Apim-Subscription-Key","116aa6883b0e4cca8e644de51ce91d93");
        request.AddParameter("application/octet-stream", photo, ParameterType.RequestBody);
        var response = client.Execute(request);
        Console.WriteLine("\n\nResponse 1: {0} \t {1}\n", response.Content, response.Content == null ? "response.Content nulo" : response.Content);
        JArray ja = JArray.Parse(response.Content);
        //Console.WriteLine("\n\nresponse 1: {0} {1}\n", response.Content, ja.Count, ja[0].Count);
        JObject data = new JObject();
        for(int i = 0; i < ja.Count; i++)
          data = JObject.Parse(ja[i].ToString());
        var fid = data.GetValue("faceId");
        if(fid != null && fid.Any()){
        fid = fid.ToString();
          String payload = "{\"faceId\":\"" + fid + "\", \"personId\":\"" + _pRepo.GetByEmail(User.Identity.Name).Single().FaceApiId + "\", \"personGroupId\":\"siges\"}";
          var url = "https://southcentralus.api.cognitive.microsoft.com/face/v1.0/verify?"+HttpUtility.ParseQueryString(string.Empty);
          var client2 = new RestClient(url);
          client2.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
          var request2 = new RestRequest(Method.POST);
          request2.AddHeader("Content-Type","application/json");
          request2.AddHeader("Ocp-Apim-Subscription-Key","116aa6883b0e4cca8e644de51ce91d93");
          request2.AddHeader("Ocp-Apim-Subscription-Key","def1074b02a749b98cff01822ec591c5");
          request2.AddParameter(null, payload, ParameterType.RequestBody);
          var response2 = client2.Execute(request2);
          Console.WriteLine("\n\nResponse 2: {0} {1}\n", response2.Content, payload);
          return response2.Content.ToString();
        }
      }
      return "";
    }

    public FileResult MostrarArchivo(int osId, int operadorEstadoId, int operadorEstadoArchivoId) {
      List<Operador> operador = _operRepo.GetAtencionServicio(osId).ToList();
      byte[] archivo = null;
      long TamanoArchivo = 0;
      string NombreArchivo = null;
      foreach(var op in operador) {
        foreach(var es in op.EstadoN) {
          foreach(var ar in es.ArchivosEvidencia){
            if(es.Id == operadorEstadoId && ar.Id == operadorEstadoArchivoId) {
              archivo = ar.File;
              TamanoArchivo = ar.Size;
              NombreArchivo = ar.Name.IsNormalized() ? ar.Name : ar.Name.Normalize();
              //Console.WriteLine(String.Format("es.Id {0} {1} operadorEstadoId, TamanoArchivo {2}, NombreArchivo {3}", es.Id, operadorEstadoId, TamanoArchivo, NombreArchivo));
            }
          }
        }
      }
      Response.Headers.Add("Content-Length", TamanoArchivo.ToString());
      Response.Headers.Add("Content-Disposition", "inline; filename=" + NombreArchivo);
      return File(archivo, "application/octet-stream");
    }
  }
}
