using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

using siges.Areas.Identity.Data;
using siges.DTO;
using siges.Models;
using siges.Repository;
using siges.Utilities;
using siges.Utilities.Templates;
using siges.Data;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace siges.Controllers
{
    [Authorize(Roles = "Cliente, Supervisor, Ventas, Operador, Administración, SuperUser")]
    public class ServicioController : Controller
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
        private readonly IComercial _comRepo;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IBitacoraEstatusRepository _beRepo;
        private readonly ISettingsRepository _setRepo;
        private readonly IOrdenPersona _opRepo;
        private readonly Settings settings;
        private readonly ApplicationDbContext _context;
        private readonly IArchivo _archivoRepo;
        private readonly IOrdenActivoFijo _orAfRepo;
        private readonly IOrdenInsumo _orInsumoRepo;
        private readonly IOsRecurrente _osRecuRepo;
        private String LoggedUser;

        public ServicioController(IConfiguracionServicioRepository csRepo, IContratoRepository cRepo, 
            IServicioRepository sRepo, ILineaNegocioRepository lnRepo, IOrdenServicioRepository osRepo, 
            IUbicacionRepository uRepo, IPersonaRepository pRepo, IInsumoRepository iRepo, IActivoFijoRepository afRepo, 
            IClienteRepository clRepo, IBitacoraRepository bRepo, IOperador operRepo, 
            UserManager<RoatechIdentityUser> um, RoleManager<IdentityRole> rm, IEmailConfiguration emailConf, 
            IComercial comRepo, IHostingEnvironment hostingEnvironment, IBitacoraEstatusRepository beRepo, 
            ISettingsRepository setRepo, IOrdenPersona opRepo, ApplicationDbContext context, IArchivo arR, 
            IOrdenActivoFijo orAfRepo, IOrdenInsumo orInsumoRepo, IOsRecurrente osRecuRepo)
        {
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
            _comRepo = comRepo;
            _hostingEnvironment = hostingEnvironment;
            _beRepo = beRepo;
            _setRepo = setRepo;
            _opRepo = opRepo;
            _context = context;
            _archivoRepo = arR;
            settings = _setRepo.GetByVersion("DAMSA");
            _orAfRepo = orAfRepo;
            _orInsumoRepo = orInsumoRepo;
            _osRecuRepo = osRecuRepo;
        }

        /*
         * Método para recuperar los datos string ExifBi de Archivo a datos EstructuraExifBi de Archivo
         *
         *
         public IActionResult ExifBiToEstructura(){
         var conn = RelationalDatabaseFacadeExtensions.GetDbConnection(_context.Database);
         ((System.Data.SqlClient.SqlConnection) conn).Open();
         SqlCommand cmd = new SqlCommand("select id, exifbi from archivo where exifbi is not null", (System.Data.SqlClient.SqlConnection)conn);
         SqlDataReader dataReader = cmd.ExecuteReader();
         Dictionary<int, EstructuraExifBi> paraGuardarEnArchivo = new Dictionary<int, EstructuraExifBi>();
         if(dataReader.HasRows){
         while(dataReader.Read()){
         EstructuraExifBi estExifBi = new EstructuraExifBi();
         ExifBi ex = JsonConvert.DeserializeObject<ExifBi>(dataReader[1].ToString());
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
         paraGuardarEnArchivo.Add((int)dataReader[0], estExifBi);
         }
         }
         ((System.Data.SqlClient.SqlConnection)conn).Close();
         foreach(int key in paraGuardarEnArchivo.Keys){
         Operador.Estado.Archivo archivo = _archivoRepo.GetById(key);
         archivo.EstructuraExif = paraGuardarEnArchivo[key];
         _archivoRepo.Update(archivo);
         }

         return NotFound();
         }
         */

        [HttpPost]
        public IActionResult BulkUpload(string blt)
        {
            var bodyStr = "";
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8, true, 1024, true))
            {
                bodyStr = reader.ReadToEnd();
            }
            Console.WriteLine("\n\nobject - {0} -  {1}\n", Request.Headers["Referer"], bodyStr);
            return Json(new { succes = true, data = blt });
        }

        public IActionResult CalendarioAllOS()
        {
            List<OrdenServicio> ordSer = _osRepo.GetAll(true).ToList();
            JsonSerializerSettings jsonSettings = new JsonSerializerSettings();
            jsonSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            jsonSettings.NullValueHandling = NullValueHandling.Ignore;
            string output = JsonConvert.SerializeObject(ordSer, jsonSettings);
            ViewData["ordenesServicio"] = output;
            return View("~/Views/Servicio/FullCalendar.cshtml");
        }

        public IActionResult Index()
        {
            return View();
        }

        private string getFechaFromOS(string datetime)
        {
            try
            {
                return datetime.Split(new char[] { ' ' })[0];
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "";
        }

        private string getHoraFromOS(string datetime)
        {
            try
            {
                return datetime.Split(new char[] { ' ' })[1];
            }
            catch (Exception e)
            {
                return e.ToString();
            }
            return "";
        }

        public IActionResult AuditCard(int osId)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (osId != null && osId > 0)
                {
                    FichaAudit fichaAudit = new FichaAudit();
                    try
                    {
                        var conn = RelationalDatabaseFacadeExtensions.GetDbConnection(_context.Database);
                        ((SqlConnection)conn).Open();
                        SqlCommand cmd = new SqlCommand("select ar.exifbi, esex.distancia from archivo ar left join estado es on ar.estadoid = es.id left join operador op on es.operadorid = op.id left join ordenservicio os on op.ordenservicioid = os.id left join estructuraexifbi esex on ar.EstructuraExifId = esex.id where es.nuevoestado = 'sitio' and ar.exifbi is not null and op.ordenservicioid = @Id", (SqlConnection)conn);
                        cmd.Parameters.Add("@Id", SqlDbType.Char);
                        cmd.Parameters["@Id"].Value = osId;
                        SqlDataReader dataReader = cmd.ExecuteReader();
                        if (dataReader.HasRows)
                        {
                            List<DateTime> dtoOfSitios = new List<DateTime>();
                            while (dataReader.Read())
                            {
                                ExifBi ex = JsonConvert.DeserializeObject<ExifBi>(dataReader[0].ToString());
                                //Console.WriteLine("\n\nLínea: "+ ex.GPSLongitude.description+" "+ex.GPSLatitude.description+"\n");
                                fichaAudit.Lugar.Real = new FichaAudit.Coordenadas
                                {
                                    Longitude = new ExifBi.gPSLongitude
                                    {
                                        id = typeof(ExifBi.gPSLongitude).IsInstanceOfType(ex.GPSLongitude) ? ex.GPSLongitude.id : 0,
                                        value = typeof(ExifBi.gPSLongitude).IsInstanceOfType(ex.GPSLongitude) ? ex.GPSLongitude.value : new int[][] { },
                                        description = typeof(ExifBi.gPSLongitude).IsInstanceOfType(ex.GPSLongitude) ? Double.Parse(ex.GPSLongitude.description.ToString().Split('.')[0] + "." + ex.GPSLongitude.description.ToString().Split('.')[1].Substring(0, 3)) : 0.0
                                    },
                                    Latitude = new ExifBi.gPSLatitude
                                    {
                                        id = typeof(ExifBi.gPSLatitude).IsInstanceOfType(ex.GPSLatitude) ? ex.GPSLatitude.id : 0,
                                        value = typeof(ExifBi.gPSLatitude).IsInstanceOfType(ex.GPSLatitude) ? ex.GPSLatitude.value : new int[][] { },
                                        description = typeof(ExifBi.gPSLatitude).IsInstanceOfType(ex.GPSLatitude) ? ex.GPSLatitude.description.Split('.')[0] + "." + ex.GPSLatitude.description.Split('.')[1].Substring(0, 6) : ""
                                    }
                                };
                                if (typeof(ExifBi.dateTimeOriginal).IsInstanceOfType(ex.DateTimeOriginal))
                                    for (int i = 0; i < ex.DateTimeOriginal.value.Length; i++)
                                    {
                                        dtoOfSitios.Add(DateTime.ParseExact(ex.DateTimeOriginal.value[i].Split(new char[] { ' ' })[0].Replace(':', '-') + "T" + ex.DateTimeOriginal.value[i].Split(new char[] { ' ' })[1], "s", CultureInfo.CreateSpecificCulture("es-ES")));
                                    }
                                fichaAudit.EstructuraExifBiDistancia = (float)Double.Parse(dataReader[1].ToString(), NumberStyles.Float);
                            }
                          ((SqlConnection)conn).Close();
                            dtoOfSitios.Sort((a, b) => a.CompareTo(b));
                            fichaAudit.FechaDeProgramacion.InicioReal = dtoOfSitios.Count > 0 ? dtoOfSitios[0].ToString("G", CultureInfo.CreateSpecificCulture("es-ES")).Split(new char[] { ' ' })[0] : "";
                            fichaAudit.HoraDeLlegada.Real = dtoOfSitios.Count > 0 ? dtoOfSitios[0].ToString("G", CultureInfo.CreateSpecificCulture("es-ES")) : "";
                        }
                        if (((SqlConnection)conn).State == System.Data.ConnectionState.Open)
                            ((SqlConnection)conn).Close();

                        ((SqlConnection)conn).Open();
                        cmd = new SqlCommand("select ar.exifbi from archivo ar left join estado es on ar.estadoid = es.id left join operador op on es.operadorid = op.id left join ordenservicio os on op.ordenservicioid = os.id where es.nuevoestado = 'atendido' and ar.exifbi is not null and op.ordenservicioid = @Id", (SqlConnection)conn);
                        cmd.Parameters.Add("@Id", SqlDbType.Char);
                        cmd.Parameters["@Id"].Value = osId;
                        dataReader = cmd.ExecuteReader();
                        if (dataReader.HasRows)
                        {
                            List<DateTime> dtoOfAtendidos = new List<DateTime>();
                            while (dataReader.Read())
                            {
                                ExifBi ex = JsonConvert.DeserializeObject<ExifBi>(dataReader[0].ToString());
                                if (typeof(ExifBi.dateTimeOriginal).IsInstanceOfType(ex.DateTimeOriginal))
                                    for (int i = 0; i < ex.DateTimeOriginal.value.Length; i++)
                                    {
                                        dtoOfAtendidos.Add(DateTime.ParseExact(ex.DateTimeOriginal.value[i].Split(new char[] { ' ' })[0].Replace(':', '-') + "T" + ex.DateTimeOriginal.value[i].Split(new char[] { ' ' })[1], "s", CultureInfo.CreateSpecificCulture("es-ES")));
                                    }
                            }
                          ((SqlConnection)conn).Close();
                            dtoOfAtendidos.Sort((a, b) => b.CompareTo(a));
                            fichaAudit.FechaDeProgramacion.FinalReal = dtoOfAtendidos.Count > 0 ? dtoOfAtendidos[0].ToString().Split(new char[] { ' ' })[0] : "";
                            fichaAudit.HoraDeSalida.Real = dtoOfAtendidos.Count > 0 ? dtoOfAtendidos[0].ToString() : "";
                        }
                        if (((SqlConnection)conn).State == System.Data.ConnectionState.Open)
                            ((SqlConnection)conn).Close();

                        ((SqlConnection)conn).Open();
                        cmd = new SqlCommand("select os.FechaInicio, os.FechaFin, ub.direccion, ub.latitude, ub.longitude, ub.distancia from ordenservicio os left join ubicacion ub on os.ubicacionid = ub.id where os.id = @Id", (SqlConnection)conn);
                        cmd.Parameters.Add("@Id", SqlDbType.Char);
                        cmd.Parameters["@Id"].Value = osId;
                        dataReader = cmd.ExecuteReader();
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                fichaAudit.FechaDeProgramacion.InicioProgramado = getFechaFromOS(dataReader["FechaInicio"].ToString());
                                fichaAudit.FechaDeProgramacion.FinalProgramado = getFechaFromOS(dataReader["FechaFin"].ToString());
                                fichaAudit.HoraDeLlegada.Programado = dataReader["FechaInicio"].ToString();
                                fichaAudit.HoraDeSalida.Programado = dataReader["FechaFin"].ToString();
                                fichaAudit.TiempoLaboradoProgramado.Minutos = (DateTime.Parse(dataReader["FechaFin"].ToString(), CultureInfo.CreateSpecificCulture("es-ES")) - DateTime.Parse(dataReader["FechaInicio"].ToString(), CultureInfo.CreateSpecificCulture("es-ES"))).TotalMinutes.ToString();
                                fichaAudit.Direccion = dataReader["direccion"].ToString();
                                fichaAudit.UbicacionDistancia = (float)Double.Parse(dataReader["distancia"].ToString(), NumberStyles.Float);
                                //Console.WriteLine("\n\nlatitude: " + dataReader["latitude"] + "\n");
                                if (!String.IsNullOrEmpty(dataReader["latitude"].ToString()) && !String.IsNullOrEmpty(dataReader["longitude"].ToString()))
                                {
                                    var lat = "";
                                    var lon = "";
                                    try
                                    {
                                        lat = Double.Parse(dataReader["latitude"].ToString()) < 0 ? (Double.Parse(dataReader["latitude"].ToString()) * -1).ToString() : dataReader["latitude"].ToString();
                                        lon = Double.Parse(dataReader["longitude"].ToString()) < 0 ? (Double.Parse(dataReader["longitude"].ToString()) * -1).ToString() : dataReader["longitude"].ToString();
                                    }
                                    catch (Exception)
                                    {
                                    }
                                    finally
                                    {
                                        fichaAudit.Lugar.Programado = new FichaAudit.CoordenadaSencilla
                                        {
                                            Latitude = lat,
                                            Longitude = lon
                                        };
                                    }
                                }
                            }
                          ((SqlConnection)conn).Close();
                        }
                        if (((SqlConnection)conn).State == System.Data.ConnectionState.Open)
                            ((SqlConnection)conn).Close();

                        //DateTime? horaInicioJornadaTotal = null;
                        //DateTime? horaFinJornadaTotal = null;
                        var cultureInfo = CultureInfo.CreateSpecificCulture("es-ES");

                        try
                        {
                            int yL = Int32.Parse(fichaAudit.HoraDeLlegada.Programado.Split(new char[] { ' ' })[0].Split(new char[] { '/' })[2]);
                            int ML = Int32.Parse(fichaAudit.HoraDeLlegada.Programado.Split(new char[] { ' ' })[0].Split(new char[] { '/' })[1]);
                            int dL = Int32.Parse(fichaAudit.HoraDeLlegada.Programado.Split(new char[] { ' ' })[0].Split(new char[] { '/' })[0]);
                            int hL = Int32.Parse(fichaAudit.HoraDeLlegada.Programado.Split(new char[] { ' ' })[1].Split(new char[] { ':' })[0]);
                            int mL = Int32.Parse(fichaAudit.HoraDeLlegada.Programado.Split(new char[] { ' ' })[1].Split(new char[] { ':' })[1]);
                            int sL = Int32.Parse(fichaAudit.HoraDeLlegada.Programado.Split(new char[] { ' ' })[1].Split(new char[] { ':' })[2]);

                            int yR = Int32.Parse(fichaAudit.HoraDeLlegada.Real.Split(new char[] { ' ' })[0].Split(new char[] { '/' })[2]);
                            int MR = Int32.Parse(fichaAudit.HoraDeLlegada.Real.Split(new char[] { ' ' })[0].Split(new char[] { '/' })[1]);
                            int dR = Int32.Parse(fichaAudit.HoraDeLlegada.Real.Split(new char[] { ' ' })[0].Split(new char[] { '/' })[0]);
                            int hR = Int32.Parse(fichaAudit.HoraDeLlegada.Real.Split(new char[] { ' ' })[1].Split(new char[] { ':' })[0]);
                            int mR = Int32.Parse(fichaAudit.HoraDeLlegada.Real.Split(new char[] { ' ' })[1].Split(new char[] { ':' })[1]);
                            int sR = 00;//Int32.Parse(fichaAudit.HoraDeLlegada.Real.Split(new char[]{' '})[1].Split(new char[]{':'})[2]);

                            //horaInicioJornadaTotal = new DateTime(yR, MR, dR, hR, mR, sR);
                            fichaAudit.HoraDeLlegada.Minutos = ((new DateTime(yR, MR, dR, hR, mR, sR) - (new DateTime(yL, ML, dL, hL, mL, sL)))).TotalMinutes.ToString();
                        }
                        catch (Exception e)
                        {
                            fichaAudit.HoraDeLlegada.Minutos = null;
                        }

                        try
                        {
                            int yS = Int32.Parse(fichaAudit.HoraDeSalida.Programado.Split(new char[] { ' ' })[0].Split(new char[] { '/' })[2]);
                            int MS = Int32.Parse(fichaAudit.HoraDeSalida.Programado.Split(new char[] { ' ' })[0].Split(new char[] { '/' })[1]);
                            int dS = Int32.Parse(fichaAudit.HoraDeSalida.Programado.Split(new char[] { ' ' })[0].Split(new char[] { '/' })[0]);
                            int hS = Int32.Parse(fichaAudit.HoraDeSalida.Programado.Split(new char[] { ' ' })[1].Split(new char[] { ':' })[0]);
                            int mS = Int32.Parse(fichaAudit.HoraDeSalida.Programado.Split(new char[] { ' ' })[1].Split(new char[] { ':' })[1]);
                            int sS = Int32.Parse(fichaAudit.HoraDeSalida.Programado.Split(new char[] { ' ' })[1].Split(new char[] { ':' })[2]);

                            int ySR = Int32.Parse(fichaAudit.HoraDeSalida.Real.Split(new char[] { ' ' })[0].Split(new char[] { '/' })[2]);
                            int MSR = Int32.Parse(fichaAudit.HoraDeSalida.Real.Split(new char[] { ' ' })[0].Split(new char[] { '/' })[1]);
                            int dSR = Int32.Parse(fichaAudit.HoraDeSalida.Real.Split(new char[] { ' ' })[0].Split(new char[] { '/' })[0]);
                            int hSR = Int32.Parse(fichaAudit.HoraDeSalida.Real.Split(new char[] { ' ' })[1].Split(new char[] { ':' })[0]);
                            int mSR = Int32.Parse(fichaAudit.HoraDeSalida.Real.Split(new char[] { ' ' })[1].Split(new char[] { ':' })[1]);
                            int sSR = 00;//Int32.Parse(fichaAudit.HoraDeSalida.Real.Split(new char[]{' '})[1].Split(new char[]{':'})[2]);

                            //horaFinJornadaTotal = new DateTime(ySR, MSR, dSR, hSR, mSR, sSR);
                            fichaAudit.HoraDeSalida.Minutos = ((new DateTime(ySR, MSR, dSR, hSR, mSR, sSR) - (new DateTime(yS, MS, dS, hS, mS, sS)))).TotalMinutes.ToString();
                        }
                        catch (Exception e)
                        {
                            fichaAudit.HoraDeSalida.Minutos = null;
                        }

                        if (!String.IsNullOrEmpty(fichaAudit.HoraDeSalida.Real) && !String.IsNullOrEmpty(fichaAudit.HoraDeLlegada.Real) && !String.IsNullOrEmpty(fichaAudit.HoraDeLlegada.Programado) && !String.IsNullOrEmpty(fichaAudit.HoraDeSalida.Programado))
                            fichaAudit.TiempoLaboradoDiff.Minutos = ((DateTime.Parse(fichaAudit.HoraDeSalida.Real.Split(':')[0] + ":" + fichaAudit.HoraDeSalida.Real.Split(':')[1] + ":00") - DateTime.Parse(fichaAudit.HoraDeLlegada.Real.Split(':')[0] + ":" + fichaAudit.HoraDeLlegada.Real.Split(':')[1] + ":00")) - (DateTime.Parse(fichaAudit.HoraDeSalida.Programado) - DateTime.Parse(fichaAudit.HoraDeLlegada.Programado))).TotalMinutes.ToString();
                        fichaAudit.HoraDeLlegada.Real = !String.IsNullOrEmpty(fichaAudit.HoraDeLlegada.Real) ? fichaAudit.HoraDeLlegada.Real.Split(new char[] { ' ' })[1] : "";
                        Console.WriteLine("\n\nÚltima fichaAudit.HoraDeLlegada.Real: {0}\n", fichaAudit.HoraDeLlegada.Real);
                        fichaAudit.HoraDeSalida.Real = !String.IsNullOrEmpty(fichaAudit.HoraDeSalida.Real) ? fichaAudit.HoraDeSalida.Real.Split(new char[] { ' ' })[1] : "";
                        Console.WriteLine("\n\nÚltima fichaAudit.HoraDeSalida.Real: {0}\n", fichaAudit.HoraDeSalida.Real);
                        if (!String.IsNullOrEmpty(fichaAudit.HoraDeSalida.Real) && !String.IsNullOrEmpty(fichaAudit.HoraDeLlegada.Real))
                        {
                            fichaAudit.TiempoLaboradoReal.Minutos = (DateTime.Parse(fichaAudit.HoraDeSalida.Real.Split(':')[0] + ":" + fichaAudit.HoraDeSalida.Real.Split(':')[1] + ":00") - DateTime.Parse(fichaAudit.HoraDeLlegada.Real.Split(':')[0] + ":" + fichaAudit.HoraDeLlegada.Real.Split(':')[1] + ":00")).TotalMinutes.ToString();
                        }
                        fichaAudit.HoraDeSalida.Programado = getHoraFromOS(fichaAudit.HoraDeSalida.Programado);
                        fichaAudit.HoraDeLlegada.Programado = getHoraFromOS(fichaAudit.HoraDeLlegada.Programado);
                        List<string> fichaAuditL = new List<string>();
                        fichaAuditL.Add(JsonConvert.SerializeObject(fichaAudit));

                        ((SqlConnection)conn).Open();
                        cmd = new SqlCommand("select ar.[file], es.comentarionuevoestado from archivo ar left join estado es on ar.estadoid = es.id left join operador op on es.operadorid = op.id left join ordenservicio os on op.ordenservicioid = os.id where es.nuevoestado = 'sitio' and ar.[file] is not null and op.ordenservicioid = @Id", (SqlConnection)conn);
                        cmd.Parameters.Add("@Id", SqlDbType.Char);
                        cmd.Parameters["@Id"].Value = osId;
                        dataReader = cmd.ExecuteReader();
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                ViewData["sitio"] = dataReader[0];
                                ViewData["comentario"] = dataReader[1];
                            }
                        }
                        if (((SqlConnection)conn).State == System.Data.ConnectionState.Open)
                            ((SqlConnection)conn).Close();
                        ViewData["customvisionuso"] = (bool)settings.CustomVisionUso;
                        if (settings.CustomVisionUso)
                        {
                            ((SqlConnection)conn).Open();
                            cmd = new SqlCommand("select ar.[file], es.comentarionuevoestado, ar.customvisionresult from archivo ar left join estado es on ar.estadoid = es.id left join operador op on es.operadorid = op.id left join ordenservicio os on op.ordenservicioid = os.id where es.nuevoestado = 'revequiposeguridad' and ar.[file] is not null and op.ordenservicioid = @Id order by ar.id desc", (SqlConnection)conn);
                            cmd.Parameters.Add("@Id", SqlDbType.Char);
                            cmd.Parameters["@Id"].Value = osId;
                            dataReader = cmd.ExecuteReader();
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    ViewData["revequiposeguridad"] = dataReader[0];
                                    ViewData["revequiposeguridadcomentario"] = dataReader[1].ToString();
                                    ViewData["revequiposeguridadresult"] = dataReader[2].ToString();
                                }
                            }
                            if (((SqlConnection)conn).State == System.Data.ConnectionState.Open)
                                ((SqlConnection)conn).Close();
                        }
                        ViewData["faceApiUso"] = (bool)settings.FaceApiUso;
                        if (settings.FaceApiUso)
                        {
                            ((SqlConnection)conn).Open();
                            cmd = new SqlCommand("select ar.[file], es.comentarionuevoestado, ar.faceapifinalresponse from archivo ar left join estado es on ar.estadoid = es.id left join operador op on es.operadorid = op.id left join ordenservicio os on op.ordenservicioid = os.id where es.nuevoestado = 'asistencia' and ar.[file] is not null and op.ordenservicioid = @Id order by ar.id desc", (SqlConnection)conn);
                            cmd.Parameters.Add("@Id", SqlDbType.Char);
                            cmd.Parameters["@Id"].Value = osId;
                            dataReader = cmd.ExecuteReader();
                            if (dataReader.HasRows)
                            {
                                while (dataReader.Read())
                                {
                                    ViewData["asistencia"] = dataReader[0];
                                    ViewData["asistenciacomentario"] = dataReader[1].ToString();
                                    ViewData["faceApiCerteza"] = dataReader[2].ToString();
                                }
                            }
                            if (((SqlConnection)conn).State == System.Data.ConnectionState.Open)
                                ((SqlConnection)conn).Close();
                        }
                        return View(fichaAuditL);
                    }
                    catch (Exception e)
                    {
                        return Json(new { success = false, data = e });
                    }
                }
                return NotFound();
            }
            return View();
        }



        /* --------- Configuracion de servicios ------------ */
        /* Lista Configuracion de servicios */
        [Authorize(Roles = "Supervisor, SuperUser")]
        public IActionResult IndexListCS()
        {
            List<ConfiguracionServicio> _all = _csRepo.GetAll(true).ToList();
            ViewData["configuraciones"] = _all;
            return View("~/Views/Servicios/ListConfiguracionServicio.cshtml");
        }


        [Authorize(Roles = "Supervisor, SuperUser")]
        public IActionResult DeleteCS(int csid)
        {
            ConfiguracionServicio nCS = _csRepo.GetById(csid);
            nCS.Estatus = false;
            _csRepo.Update(nCS);
            LoggedUser = this.User.Identity.Name;
            _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Delete", Section = "Configuración de contrato", Description = "Configuración no. " + nCS.Id + " eliminada." });
            return RedirectToAction("IndexListCS", "Servicio");
        }

        /* Nueva Configuracion de servicios */
        [Authorize(Roles = "Supervisor, SuperUser")]
        public IActionResult IndexCreateCS()
        {
            List<Cliente> _allcl = _clRepo.GetAll(true).ToList();
            List<Servicio> _alls = _sRepo.GetAll(true).ToList();
            List<LineaNegocio> _allln = _lnRepo.GetAll(true).ToList();
            ViewData["clientes"] = _allcl;
            ViewData["servicios"] = _alls;
            ViewData["lineas"] = _allln;
            return View("~/Views/Servicios/CreateConfiguracionServicio.cshtml");
        }

        [HttpPost]
        [Authorize(Roles = "Supervisor, SuperUser")]
        public IActionResult SaveCS(ConfiguracionServicioDTO nCS)
        {
            Console.WriteLine("\n\n\n inicio SaveCS");
            Console.WriteLine("\n\n\n" + nCS.Id);
            Console.WriteLine(nCS.Cliente);
            Console.WriteLine(nCS.Contrato);
            Console.WriteLine(nCS.Ubicacion);
            Console.WriteLine(nCS.Estatus);
            Console.WriteLine(nCS.dtServicios + "\n\n\n");
            List<DetalleConfiguracionServicioDTO> detalle = new List<DetalleConfiguracionServicioDTO>();
            foreach (var aS in JArray.Parse(nCS.dtServicios))
            {
                JObject s = JObject.Parse(aS.ToString());
                detalle.Add(new DetalleConfiguracionServicioDTO
                {
                    Id = Int32.Parse(s.Property("idordser").Value.ToString()),
                    Servicio = Int32.Parse(s.Property("idservicio").Value.ToString()),
                    LineaNegocio = Int32.Parse(s.Property("idLinea").Value.ToString()),
                    MaximoServicio = Int32.Parse(s.Property("max").Value.ToString()),
                    MinimoServicio = Int32.Parse(s.Property("min").Value.ToString()),
                    Opcional1 = s.Property("op1").Value.ToString(),
                    Opcional2 = Int32.Parse(s.Property("op2").Value.ToString().Equals("") ? "0" : s.Property("op2").Value.ToString()),
                    CostoServicio = Decimal.Parse(s.Property("costo").Value.ToString()),
                    PrecioServicio = Decimal.Parse(s.Property("precio").Value.ToString())
                });
            }
            ConfiguracionServicio cs = new ConfiguracionServicio();
            cs.Cliente = _clRepo.GetById(nCS.Cliente);
            cs.Contrato = _cRepo.GetById(nCS.Contrato);
            cs.Ubicacion = _uRepo.GetById(nCS.Ubicacion);
            cs.Estatus = nCS.Estatus;
            if (detalle.Count > 0)
            {
                foreach (var i in detalle)
                {
                    cs.Detalle.Add(new DetalleConfiguracionServicio() { LineaNegocio = _lnRepo.GetById(i.LineaNegocio), Servicio = _sRepo.GetById(i.Servicio), CostoServicio = i.CostoServicio, PrecioServicio = i.PrecioServicio, MinimoServicio = i.MinimoServicio, MaximoServicio = i.MaximoServicio, Opcional1 = i.Opcional1, Opcional2 = i.Opcional2, Estatus = true });
                }
            }
            _csRepo.Insert(cs);
            LoggedUser = this.User.Identity.Name;
            _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Create", Section = "Configuración de contrato", Description = "Configuración no. " + nCS.Id + " creada." });
            return RedirectToAction("IndexListCS", "Servicio");
        }

        /* Editar Configuracion de servicios */
        [Authorize(Roles = "Supervisor, SuperUser")]
        public IActionResult IndexEditCS(int configuracion)
        {
            ConfiguracionServicio eCS = _csRepo.GetByIdCS(configuracion);
            List<Cliente> _allcl = _clRepo.GetAll(true).ToList();
            List<Contrato> _allc = _cRepo.GetContratoByCliente(eCS.Cliente.Id).ToList();
            List<Ubicacion> _allu = _uRepo.getUbicacionesByCliente(eCS.Cliente.Id).ToList();
            List<Servicio> _alls = _sRepo.GetAll(true).ToList();
            List<LineaNegocio> _allln = _lnRepo.GetAll(true).ToList();
            ViewData["configuracion"] = eCS;
            ViewData["clientes"] = _allcl;
            ViewData["contratos"] = _allc;
            ViewData["ubicaciones"] = _allu;
            ViewData["servicios"] = _alls;
            ViewData["lineas"] = _allln;
            return View("~/Views/Servicios/EditConfiguracionServicio.cshtml");
        }

        /* Editar Configuracion de servicios */
        [Authorize(Roles = "Supervisor, SuperUser")]
        public IActionResult IndexDetailCS(int configuracion)
        {
            ConfiguracionServicio eCS = _csRepo.GetByIdCS(configuracion);
            List<Cliente> _allcl = _clRepo.GetAll(true).ToList();
            List<Contrato> _allc = _cRepo.GetContratoByCliente(eCS.Cliente.Id).ToList();
            List<Ubicacion> _allu = _uRepo.getUbicacionesByCliente(eCS.Cliente.Id).ToList();
            List<Servicio> _alls = _sRepo.GetAll(true).ToList();
            List<LineaNegocio> _allln = _lnRepo.GetAll(true).ToList();
            ViewData["configuracion"] = eCS;
            ViewData["clientes"] = _allcl;
            ViewData["contratos"] = _allc;
            ViewData["ubicaciones"] = _allu;
            ViewData["servicios"] = _alls;
            ViewData["lineas"] = _allln;
            return View("~/Views/Servicios/DetailConfiguracionServicio.cshtml");
        }

        [HttpPost]
        [Authorize(Roles = "Supervisor, SuperUser")]
        public IActionResult EditCS(ConfiguracionServicioDTO nCS)
        {
            List<DetalleConfiguracionServicioDTO> detalle = new List<DetalleConfiguracionServicioDTO>();
            foreach (var aS in JArray.Parse(nCS.dtServicios))
            {
                JObject s = JObject.Parse(aS.ToString());
                detalle.Add(new DetalleConfiguracionServicioDTO
                {
                    Id = Int32.Parse(s.Property("idordser").Value.ToString()),
                    Servicio = Int32.Parse(s.Property("idservicio").Value.ToString()),
                    LineaNegocio = Int32.Parse(s.Property("idLinea").Value.ToString()),
                    MaximoServicio = Int32.Parse(s.Property("max").Value.ToString()),
                    MinimoServicio = Int32.Parse(s.Property("min").Value.ToString()),
                    Opcional1 = s.Property("op1").Value.ToString(),
                    Opcional2 = Int32.Parse(s.Property("op2").Value.ToString().Equals("") ? "0" : s.Property("op2").Value.ToString()),
                    CostoServicio = Decimal.Parse(s.Property("costo").Value.ToString()),
                    PrecioServicio = Decimal.Parse(s.Property("precio").Value.ToString())
                });
            }
            ConfiguracionServicio cs = _csRepo.GetByIdCS(nCS.Id);
            cs.Cliente = _clRepo.GetById(nCS.Cliente);
            cs.Contrato = _cRepo.GetById(nCS.Contrato);
            cs.Ubicacion = _uRepo.GetById(nCS.Ubicacion);
            if (detalle.Count > 0)
            {
                foreach (var i in cs.Detalle)
                {
                    i.Estatus = false;
                }
                foreach (var i in detalle)
                {
                    if (i.Id > 0)
                    {
                        foreach (var x in cs.Detalle)
                        {
                            if (i.Id == x.Id)
                            {
                                x.Estatus = true;
                            }
                        }
                    }
                    else
                    {
                        cs.Detalle.Add(new DetalleConfiguracionServicio() { LineaNegocio = _lnRepo.GetById(i.LineaNegocio), Servicio = _sRepo.GetById(i.Servicio), CostoServicio = i.CostoServicio, PrecioServicio = i.PrecioServicio, MinimoServicio = i.MinimoServicio, MaximoServicio = i.MaximoServicio, Opcional1 = i.Opcional1, Opcional2 = i.Opcional2, Estatus = true });
                    }
                }
            }
            else if (cs.Detalle.Count > 0)
            {
                foreach (var i in cs.Detalle)
                {
                    i.Estatus = false;
                }
            }
            _csRepo.Update(cs);
            LoggedUser = this.User.Identity.Name;
            _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Update", Section = "Configuración de contrato", Description = "Configuración no. " + nCS.Id + " actualizada." });
            return RedirectToAction("IndexListCS", "Servicio");
        }

        /* --------- Ordenes de servicios ------------ */
        /* Lista Ordenes de servicios */
        public IActionResult IndexListOS()
        {
            Console.WriteLine("\n\n\nInicio IndexListOS\n\n\n");
            List<OrdenServicio> _all = null;
            if (this.User.IsInRole("Operador"))
            {
                int perId = _pRepo.GetByEmail(this.User.Identity.Name).Single().Id;
                _all = new List<OrdenServicio>();
                foreach (OrdenPersona op_ in _opRepo.GetOSbyIdP(perId).ToList())
                    _all.Add(op_.OrdenServicio);
                _all.Sort();
                ViewData["ordenes"] = _all;
                var opId = _pRepo.GetByEmail(this.User.Identity.Name).Single().Id;
                var op = _opRepo.GetOSbyIdP(opId).ToList();
                List<OrdenPersona> ordPer = new List<OrdenPersona>();
                foreach (var ordenPerso in op)
                {
                    if (ordenPerso.OrdenServicio.Estatus)
                        if (ordenPerso.OrdenServicio.FechaInicio != null && ordenPerso.OrdenServicio.FechaFin != null)
                            if (ordenPerso.OrdenServicio.EstatusServicio == "programado" || ordenPerso.OrdenServicio.EstatusServicio == "reprogramado" || ordenPerso.OrdenServicio.EstatusServicio == "solicitado")
                            {
                                ordPer.Add(ordenPerso);
                            }
                }
                JsonSerializerSettings jsonSettings = new JsonSerializerSettings();
                jsonSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                jsonSettings.NullValueHandling = NullValueHandling.Ignore;
                string output = JsonConvert.SerializeObject(ordPer, jsonSettings);
                ordPer.Sort();
                ViewData["ordenesOperador"] = output;
                ViewData["ordenesPersona"] = ordPer;
                ViewData["OrdenServicioEnTiempo"] = JsonConvert.SerializeObject(getOSenTiempoOperador().Result, jsonSettings);
            } /*else if(this.User.IsInRole("Ventas")){
          _all = new List<OrdenServicio>();
          foreach(OrdenServicio os in _osRepo.GetAll(true).OrderByDescending(r => r.Id).ToList())
          if(os.PersonaComercial.Id == _pRepo.GetByEmail(this.User.Identity.Name).Id )
          _all.Add(os);
          ViewData["ordenes"] = _all;
          }*/
            else
            {
                _all = new List<OrdenServicio>();
                var allOS = _osRepo.GetAll(true).OrderByDescending(r => r.Id).ToList();
                int i = 1;
                Console.WriteLine("desde controller: \n");
                foreach (OrdenServicio oss in allOS)
                {
                    Console.WriteLine(i + ".- " + oss.Id + "-folio: " + oss.Folio + "-fechaIni: " + oss.FechaInicio + "-cliente: " + oss.Cliente.RazonSocial
                        + "-contrato: " + oss.Contrato.Nombre + "-ubicacion: " + oss.Ubicacion.Nombre + "-servicio: " + oss.Servicio.Nombre
                        + "-estatus: " + oss.EstatusServicio + "-PersonaComercial: " + oss.PersonaComercial.Nombre);
                    if (oss.EstatusServicio == "solicitado" || oss.EstatusServicio == "programado" || oss.EstatusServicio == "reprogramado" || oss.EstatusServicio == "cancelado")
                        _all.Add(oss);
                    i++;
                }
                ViewData["ordenes"] = _all;
            }
            return View();
        }

        [HttpPost]
        public IActionResult CambiarEstado(OperadorDTO operador)
        {
            foreach (var ordenServicio in _osRepo.GetAll(true).OrderBy(r => r.FechaInicio).ToList())
            {
                if (ordenServicio.Id == operador.OrdenServicioId)
                {
                    ViewData["ordenServicio"] = ordenServicio;
                }
            }
            List<Operador> listOperador = _operRepo.GetAtencionServicio(operador.OrdenServicioId).ToList();
            if (listOperador.Count == 0)
            {
                ViewData["estados"] = "todos";
            }
            else
            {
                foreach (Operador oper in listOperador)
                {
                    foreach (Operador.Estado esta in oper.EstadoN)
                    {
                        switch (DateTime.Compare(DateTime.Today, oper.Hora.Date))
                        {
                            case -1: //Hoy es antes que evidencia
                                ViewData["estados"] = "vacio";
                                ViewData["diff"] = DateTime.Today.Subtract(oper.Hora.Date);
                                break;
                            case 0: //Hoy es mismo que evidencia
                                if (esta.NuevoEstado == "sitio")
                                    ViewData["estados"] = "sinsitio";
                                if (esta.NuevoEstado == "atendido")
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
            ViewData["personaId"] = _pRepo.GetByEmail(this.User.Identity.Name).Single().Id;
            return View();
        }

        private async Task<List<OrdenPersona>> getOSenTiempoOperador()
        {
            List<OrdenPersona> ordSerOper = new List<OrdenPersona>();
            foreach (var ordenPersona in _opRepo.GetOSbyIdP(_pRepo.GetByEmail(User.Identity.Name).Single().Id).ToList())
            {
                if (ordenPersona.OrdenServicio.Estatus)
                {
                    if (ordenPersona.OrdenServicio.FechaInicio != null && ordenPersona.OrdenServicio.FechaFin != null)
                    {
                        if (ordenPersona.OrdenServicio.EstatusServicio == "programado" || ordenPersona.OrdenServicio.EstatusServicio == "reprogramado" || ordenPersona.OrdenServicio.EstatusServicio == "solicitado")
                        {

                            //ordSerOper.Add(ordenPersona);// deja pasar todas las fechas disponibles

                            switch (DateTime.Compare(DateTime.Now, (DateTime)ordenPersona.OrdenServicio.FechaInicio))
                            {
                                case -1: //Hoy es antes de FechaInicio
                                    ViewData["estados"] = "destiempo";
                                    break;
                                case 0: //Hoy es el mismo de FechaInicio
                                    ordSerOper.Add(ordenPersona);
                                    ViewData["estados"] = "entiempo";
                                    break;
                                case 1: //Hoy es después de FechaInicio
                                    switch (DateTime.Compare(DateTime.Now, ((DateTime)ordenPersona.OrdenServicio.FechaFin).AddDays(2)))
                                    {
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

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Supervisor, SuperUser")]
        public IActionResult DeleteOS(int osid)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (osid != null && osid > 0)
                {
                    try
                    {
                        OrdenServicio nOS = _osRepo.GetById(osid);
                        nOS.Estatus = false;
                        _osRepo.Update(nOS);
                        LoggedUser = this.User.Identity.Name;
                        _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Delete", Section = "Orden de servicio", Description = "Orden de servicio no. " + nOS.Id + " eliminada." });
                        return Json(new { success = true });
                    }
                    catch (Exception e)
                    {
                        return Json(new { success = false, data = e });
                    }
                }
                return NotFound();
            }
            return RedirectToAction("IndexListOS", "Servicio");
        }

        /* Lista Ordenes de servicios eliminadas */
        [Authorize(Roles = "Supervisor, SuperUser")]
        public IActionResult IndexListDOS()
        {
            //List<OrdenServicio> _all = _osRepo.GetAll(false).OrderByDescending(r => r.FechaInicio).ToList();
            List<OrdenServicio> _all = _osRepo.GetAllDeleted().ToList();
            Console.WriteLine("\n\nlista de os eliminadas: {0}\n", _all.Count);
            ViewData["ordenes"] = _all;
            return View("~/Views/Servicio/ListDeletedOrdenServicio.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Supervisor, SuperUser")]
        public IActionResult UpdateDOS(int osid)
        {
            try
            {
                OrdenServicio nOS = _osRepo.GetById(osid);
                nOS.Estatus = true;
                _osRepo.Update(nOS);
                LoggedUser = this.User.Identity.Name;
                _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Update", Section = "Orden de servicio eliminada", Description = "Orden de servicio no. " + nOS.Id + " actualizada." });
                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, data = e });
            }
        }

        private string getOsFolio(string typeOS)
        {
            List<OrdenServicio> osList = _osRepo.GetAll().ToList();
            if (!String.IsNullOrEmpty(typeOS) && osList.Count > 0)
                return String.Format("{0}{1:D7}", typeOS, osList[osList.Count - 1].Id + 1).ToUpper();
            else
                return String.Format("{0}{1:D7}", typeOS, 0 + 1).ToUpper();
        }

        /* Nueva Orden de servicios */
        [Authorize(Roles = "Supervisor, SuperUser")]
        public IActionResult CreateOS()
        {
            List<Cliente> _allcl = _clRepo.GetAll(true).ToList();
            List<Servicio> _alls = _sRepo.GetAll(true).ToList();
            List<LineaNegocio> _allln = _lnRepo.GetAll(true).ToList();
            ViewData["clientes"] = _allcl;
            ViewData["servicios"] = _alls;
            ViewData["lineas"] = _allln;
            ViewData["siPaquete"] = (bool)settings.UsoDePaquetes;
            return View();
        }

        [HttpPost]
        public IActionResult SaveOS(OrdenServicioDTO nOS)
        {
            Console.WriteLine("\n\n\nInicio: SaveOS");
            List<OrdenPersonalDTO> personal = new List<OrdenPersonalDTO>();
            List<OrdenInsumoDTO> insumos = new List<OrdenInsumoDTO>();
            List<OrdenActivoFijoDTO> activos = new List<OrdenActivoFijoDTO>();
            Console.WriteLine(JArray.Parse(nOS.dtPersonas));
            foreach (var aP in JArray.Parse(nOS.dtPersonas))
            {
                JObject p = JObject.Parse(aP.ToString());
                personal.Add(new OrdenPersonalDTO { Id = Int32.Parse(p.Property("idordper").Value.ToString()), PersonaId = Int32.Parse(p.Property("idpersona").Value.ToString()) });
            }
            foreach (var aA in JArray.Parse(nOS.dtActivos))
            {
                JObject a = JObject.Parse(aA.ToString());
                activos.Add(new OrdenActivoFijoDTO { Id = Int32.Parse(a.Property("idordact").Value.ToString()), ActivoFijoId = Int32.Parse(a.Property("idactivofijo").Value.ToString()) });
            }
            Console.WriteLine("\n" + JArray.Parse(nOS.dtInsumos));
            if ((bool)settings.UsoDePaquetes && !User.IsInRole("Ventas"))
            {
                foreach (var aI in JArray.Parse(nOS.dtInsumos))
                {
                    JObject i = JObject.Parse(aI.ToString());
                    if (i.Property("idinsumo") != null)
                    {
                        insumos.Add(new OrdenInsumoDTO
                        {
                            Id = Int32.Parse(i.Property("idordins").Value.ToString()),
                            InsumoId = Int32.Parse(i.Property("idinsumo").Value.ToString()),
                            /*LoteId = Int32.Parse(i.Property("idlote").Value.ToString()),*/
                            Lote = i.Property("lote").Value.ToString(),
                            /*Caducidad = i.Property("caducidad").Value.ToString(),*/
                            Cantidad = Int32.Parse(i.Property("cantidad").Value.ToString())
                        });
                    }
                }
            }
            else
            {
                if (!User.IsInRole("Ventas"))
                    foreach (var aI in JArray.Parse(nOS.dtInsumos))
                    {
                        JObject i = JObject.Parse(aI.ToString());
                        insumos.Add(new OrdenInsumoDTO
                        {
                            Id = Int32.Parse(i.Property("idordins").Value.ToString()),
                            InsumoId = Int32.Parse(i.Property("idinsumo").Value.ToString()),
                            Cantidad = Int32.Parse(i.Property("cantidad").Value.ToString())
                        });
                    }
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
            if (settings.ValidateMinimumDate)
            {
                string[] criteria = settings.MinimumDateCriteria.Split(new Char[] { ' ' });
                for (int i = 0; i < criteria.Length / 2; i++)
                {
                    if (criteria[i * 2] == os.LineaNegocio.Nombre)
                    {
                        if (DateTime.Compare(DateTime.Parse(nOS.FechaInicio, cin), DateTime.Today.AddDays(Double.Parse(criteria[(i * 2) + 1]))) < 0 || DateTime.Compare(DateTime.Parse(nOS.FechaFin, cin), DateTime.Today.AddDays(Double.Parse(criteria[(i * 2) + 1]))) < 0)
                        {
                            ViewData["ln"] = os.LineaNegocio.Nombre;
                            ViewData["fi"] = DateTime.Parse(nOS.FechaInicio, cin);
                            ViewData["ff"] = DateTime.Parse(nOS.FechaFin, cin);
                            return View("~/Views/Servicios/InvalidDate.cshtml");
                        }
                        else
                        {
                            os.FechaInicio = String.IsNullOrEmpty(nOS.FechaInicio) ? (DateTime?)null : DateTime.Parse(nOS.FechaInicio, cin);
                            os.FechaFin = String.IsNullOrEmpty(nOS.FechaFin) ? (DateTime?)null : DateTime.Parse(nOS.FechaFin, cin);
                        }
                        break;
                    }
                    Console.WriteLine("\n\n{0} - {1}\n\n", nOS.FechaInicio, nOS.FechaFin);
                    os.FechaInicio = String.IsNullOrEmpty(nOS.FechaInicio) ? (DateTime?)null : DateTime.Parse(nOS.FechaInicio, cin);
                    os.FechaFin = String.IsNullOrEmpty(nOS.FechaFin) ? (DateTime?)null : DateTime.Parse(nOS.FechaFin, cin);
                }
            }
            else
            {
                os.FechaInicio = String.IsNullOrEmpty(nOS.FechaInicio) ? (DateTime?)null : DateTime.Parse(nOS.FechaInicio, cin);
                os.FechaFin = String.IsNullOrEmpty(nOS.FechaFin) ? (DateTime?)null : DateTime.Parse(nOS.FechaFin, cin);
            }
            string EstatusORIGINAL = os.EstatusServicio;
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
            if (!(String.IsNullOrEmpty(nOS.NombreCompletoCC1) && String.IsNullOrEmpty(nOS.EmailCC1)))
            {
                os.NombreCompletoCC1 = nOS.NombreCompletoCC1;
                os.EmailCC1 = nOS.EmailCC1;
            }
            if (!(String.IsNullOrEmpty(nOS.NombreCompletoCC2) && String.IsNullOrEmpty(nOS.EmailCC2)))
            {
                os.NombreCompletoCC2 = nOS.NombreCompletoCC2;
                os.EmailCC2 = nOS.EmailCC2;
            }
            if (personal.Count > 0)
            {
                var ind = 0;
                foreach (var i in personal)
                {
                    os.Personal.Add(new OrdenPersona() { Persona = _pRepo.GetById(i.PersonaId), Estatus = true });
                    Console.WriteLine("\nPersona: {0}", os.Personal[ind++].Persona.FaceApiId);
                }
            }
            if ((bool)settings.UsoDePaquetes)
            {
                if (insumos.Count > 0)
                {
                    foreach (var x in insumos)
                    {
                        os.Insumos.Add(new OrdenInsumo()
                        {
                            Insumo = _iRepo.GetById(x.InsumoId),
                            /*Lote = _lRepo.GetById(x.LoteId),*/
                            Lote = x.Lote,
                            Caducidad = x.Caducidad,
                            Cantidad = x.Cantidad,
                            Estatus = true
                        });
                    }
                }
            }
            else
            {
                if (insumos.Count > 0)
                {
                    foreach (var x in insumos)
                    {
                        os.Insumos.Add(new OrdenInsumo()
                        {
                            Insumo = _iRepo.GetById(x.InsumoId),
                            Cantidad = x.Cantidad,
                            Estatus = true
                        });
                    }
                }
            }
            if (activos.Count > 0)
            {
                foreach (var y in activos)
                {
                    os.Activos.Add(new OrdenActivoFijo() { ActivoFijo = _afRepo.GetById(y.ActivoFijoId), Estatus = true });
                }
            }
            os.FechaAdministrativa = DateTime.Now;
            List<OrdenServicio> osL = _osRepo.GetAll(true).ToList();
            foreach (OrdenServicio osCapturado in osL)
            {
                if (os.Equals(osCapturado))
                {
                    ViewData["osCapturado"] = osCapturado;
                    return View("~/Views/Servicios/OSCapturada.cshtml");
                }
            }
            os.Folio = getOsFolio(settings.FolioPrefix);
            foreach (OrdenPersona p in os.Personal)
                Console.WriteLine("\n\nOS antes de ser insertado en la bd: {0}\n", p.Persona.FaceApiId);
            if (String.IsNullOrEmpty(os.PersonaValida.FaceApiId))
                os.PersonaValida.FaceApiId = "SINFACEAPIID";
            Console.WriteLine("\n\nOS antes de ser insertado en la bd: {0}\n", os.PersonaComercial.FaceApiId);
            Console.WriteLine("\n\nOS antes de ser insertado en la bd: {0}\n", os.PersonaValida.FaceApiId);
            _osRepo.Insert(os);

            //OSREC

            List<OrdenServicio> osR = null;
            if (nOS.OSRecurrente)
            {
                osR = CreateOSR(os, nOS);
            }

            //termina OSRECURRENTE

            int perId = _pRepo.GetByEmail(this.User.Identity.Name).Single().Id;
            int newOSId = 0;
            //List<OrdenServicio> osPersonaList = _osRepo.GetByPersonaComercialId(perId).ToList();
            var osPersonaList = _osRepo.GetByPersonaComercialId(perId);
            /*if (osPersonaList != null && osPersonaList.Count > 0) {
              foreach (OrdenServicio osP in osPersonaList) {
              if (osP.Folio == os.Folio)
              newOSId = os.Id;
              }
              }*/
            var _os_os = from r in osPersonaList select r;
            Console.WriteLine("\n\n\tPersona que crea la orden: " + this.User.Identity.Name + " id:" + perId + " cuántas encontró: " + _osRepo.GetByPersonaComercialId(perId).ToString() + " _os_os: " + _os_os + "\n");
            foreach (OrdenServicio osP in _os_os)
            {
                if (osP.Folio == os.Folio)
                    newOSId = os.Id;
            }
            Comercial com = new Comercial() { Estatus = true, OrdenServicioId = newOSId, PersonaId = perId, FechaAdministrativa = DateTime.Now };
            _comRepo.Insert(com);
            LoggedUser = this.User.Identity.Name;
            if (os.EstatusServicio == "solicitado")
            {
                if (CambiaAProgramado(os))
                {
                    os.EstatusServicio = "programado";
                    if (siges.Utilities.Tools.ValidaCamposCompletosOSyP(os, _pRepo.GetByEmail(this.User.Identity.Name).Single()))
                    {
                        Notifica notifica = new Utilities.Notifica(_emailConf, _userManager, _roleManager, " PROGRAMADA", new MimeKit.Multipart("Mixed"), this.User.Identity.Name.ToString(), settings);
                        notifica.OrdenServicioNotifica("cliente", "programado", null, os, null);
                        notifica.OrdenServicioNotifica("operador", "programado", null, os, null);
                        notifica.OrdenServicioNotifica("ventas", "programado", null, os, null);
                    }
                }
            }
            if (os.EstatusServicio == "solicita")
            {
                if (siges.Utilities.Tools.ValidaCamposCompletosOSyP(os, _pRepo.GetByEmail(this.User.Identity.Name).Single()))
                {
                    Notifica notifica = new Utilities.Notifica(_emailConf, _userManager, _roleManager, " SOLICITADA", new MimeKit.Multipart("Mixed"), this.User.Identity.Name.ToString(), settings);
                    notifica.OrdenServicioNotifica("supervisor", "solicita", null, os, null);
                }
            }
            if (os.EstatusServicio == "reprogramado")
            {
                if (siges.Utilities.Tools.ValidaCamposCompletosOSyP(os, _pRepo.GetByEmail(this.User.Identity.Name).Single()))
                {
                    Notifica notifica = new Utilities.Notifica(_emailConf, _userManager, _roleManager, " RE-PROGRAMADA", new MimeKit.Multipart("Mixed"), this.User.Identity.Name.ToString(), settings);
                    notifica.OrdenServicioNotifica("cliente", "reprogramado", null, os, null);
                    notifica.OrdenServicioNotifica("operador", "reprogramado", null, os, null);
                    notifica.OrdenServicioNotifica("ventas", "reprogramado", null, os, null);
                }
            }
            //if (os.EstatusServicio == "cancelado") {
            //if (siges.Utilities.Tools.ValidaCamposCompletosOSyP(os, _pRepo.GetByEmail(this.User.Identity.Name))) {
            //Notifica notifica = new Utilities.Notifica(_emailConf, _userManager, _roleManager, " CANCELADO", new MimeKit.Multipart("Mixed"), this.User.Identity.Name.ToString(), settings);
            //notifica.OrdenServicioNotifica("cliente", "cancelado", null, os, null);
            //notifica.OrdenServicioNotifica("operador", "cancelado", null, os, null);
            //notifica.OrdenServicioNotifica("ventas", "cancelado", null, os, null);
            //}
            //}
            if (os.EstatusServicio == "finalizado")
            {
                List<Operador> osAtendido = _operRepo.GetAtencionServicio(os.Id).ToList();
                if (osAtendido.Count > 0)
                {
                    os.PersonaValida = _pRepo.GetByEmail(this.User.Identity.Name).Single();
                    if (String.IsNullOrEmpty(os.PersonaValida.FaceApiId))
                        os.PersonaValida.FaceApiId = "SINFACEAPIID";
                    if (siges.Utilities.Tools.ValidaCamposCompletosOSyP(os, _pRepo.GetByEmail(this.User.Identity.Name).Single()))
                    {
                        Notifica notifica = new Utilities.Notifica(_emailConf, _userManager, _roleManager, "Encuesta de satisfacción", new MimeKit.Multipart("Mixed"), this.User.Identity.Name.ToString(), settings);
                        notifica.OrdenServicioNotifica("cliente", "finaliza", osAtendido, osAtendido[0].OrdenServicio, _beRepo.GetFinalizadoDescriptionByOSid(osAtendido[0].OrdenServicio.Id).ToList()[0].Description);
                        notifica.OrdenServicioNotifica("administracion", "finaliza", osAtendido, osAtendido[0].OrdenServicio, _beRepo.GetFinalizadoDescriptionByOSid(osAtendido[0].OrdenServicio.Id).ToList()[0].Description);
                        notifica.OrdenServicioNotifica("ventas", "finaliza", osAtendido, osAtendido[0].OrdenServicio, _beRepo.GetFinalizadoDescriptionByOSid(osAtendido[0].OrdenServicio.Id).ToList()[0].Description);
                    }
                }
                else
                {
                    Console.WriteLine("\n\n No es posible cambiar de estatus ya que no se ha registrado ninguna actividad en el cliente... OS id = {0} folio {1} intento realizado por {2}\n\n", os.Id, os.Folio, this.User.Identity.Name);
                    os.EstatusServicio = EstatusORIGINAL;
                }
            }
            _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Insert", Section = "Orden de servicio", Description = "Orden de servicio no. " + nOS.Id + " agregada." });
            ViewData["ordenServicio"] = os;
            ViewData["osRecurrente"] = osR;
            return View("~/Views/Servicio/ConfirmOrdenServicio.cshtml");
            //return RedirectToAction("IndexListOS", "Servicio");
        }

        /* Functions to Create and Insert OSR */
        public List<OrdenServicio> CreateOSR(OrdenServicio os, OrdenServicioDTO nOS)
        {
            Console.WriteLine("\n\n\n\tInicio Create OS recurrente");
            var osR = new List<OrdenServicio>();
            OsRecurrente children = new OsRecurrente();
            children.OsOrigenId = _osRepo.GetOSbyFolio(os.Folio).Id;
            Console.WriteLine("children folio: " + children.OsOrigenId);
            children.Periodo = nOS.OSRecurrentePeriodo;
            //if(JArray.Parse(nOS.fechasOSRecurrente).Count > 0){
            int count = 0;
            Console.WriteLine("fechasOSRecurrente: " + nOS.fechasOSRecurrente);
            foreach (var f in JArray.Parse(nOS.fechasOSRecurrente))
            {
                Console.WriteLine("FolioPrefix settings: " + settings.FolioPrefix);
                Console.WriteLine("Get folio: " + getOsFolio(settings.FolioPrefix));
                osR.Add(new OrdenServicio
                {
                    Folio = getOsFolio(settings.FolioPrefix),
                    FechaInicio = DateTime.Parse(JObject.Parse(f.ToString()).Property("inicio").Value.ToString(), CultureInfo.InvariantCulture),
                    FechaFin = DateTime.Parse(JObject.Parse(f.ToString()).Property("fin").Value.ToString(), CultureInfo.InvariantCulture),
                    Cliente = os.Cliente,
                    Contrato = os.Contrato,
                    Ubicacion = os.Ubicacion,
                    LineaNegocio = os.LineaNegocio,
                    Servicio = os.Servicio,
                    Tipo = os.Tipo,
                    EstatusServicio = os.EstatusServicio,
                    Observaciones = os.Observaciones,
                    ContactoNombre = os.ContactoNombre,
                    ContactoAP = os.ContactoAP,
                    ContactoAM = os.ContactoAM,
                    ContactoEmail = os.ContactoEmail,
                    ContactoTelefono = os.ContactoTelefono,
                    NombreCompletoCC1 = os.NombreCompletoCC1,
                    EmailCC1 = os.EmailCC1,
                    NombreCompletoCC2 = os.NombreCompletoCC2,
                    EmailCC2 = os.EmailCC2,
                    Opcional1 = os.Opcional1,
                    Opcional2 = os.Opcional2,
                    Opcional3 = os.Opcional3,
                    Opcional4 = os.Opcional4,
                    Estatus = os.Estatus,
                    Insumos = os.Insumos,
                    Personal = os.Personal,
                    Activos = os.Activos,
                    PersonaComercial = os.PersonaComercial,
                    PersonaValida = os.PersonaValida,
                    FechaAdministrativa = os.FechaAdministrativa,
                });
                _osRepo.Insert(osR[count++]);
                children.OsRecurrentesIds.Add(new OsRecurrente.Oses { OsId = _osRepo.GetOSbyFolio(osR[count - 1].Folio).Id });
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("{0} {1} {2}", osR[count - 1].Folio, osR[count - 1].FechaInicio, osR[count - 1].FechaFin);
            }
            //}
            _osRecuRepo.Insert(children);
            return osR;
        }

        /* Editar Orden de servicios */
        [Authorize(Roles = "Supervisor, SuperUser")]
        public IActionResult IndexEditOS(int ordenId)
        {
            OrdenServicio eOS = _osRepo.GetByIdOS(ordenId);
            eOS.Personal = _opRepo.GetOSbyIdOs(ordenId).ToList();
            eOS.Activos = _orAfRepo.GetOSbyIdOs(ordenId).ToList();
            eOS.Insumos = _orInsumoRepo.GetOSbyIdOs(ordenId).ToList();
            List<Cliente> _allcl = _clRepo.GetAll(true).ToList();
            _allcl.Sort((a, b) => a.RazonSocial.CompareTo(b.RazonSocial));
            List<Contrato> _allc = _cRepo.GetContratoByCliente(eOS.Cliente.Id).ToList();
            _allc.Sort((a, b) => a.Nombre.CompareTo(b.Nombre));
            List<Ubicacion> _allu = _uRepo.getUbicacionesByCliente(eOS.Cliente.Id).ToList();
            _allu.Sort((a, b) => a.Nombre.CompareTo(b.Nombre));
            List<Servicio> _alls = _sRepo.GetAll(true).ToList();
            _alls.Sort((a, b) => a.Nombre.CompareTo(b.Nombre));
            List<LineaNegocio> _allln = _lnRepo.GetAll(true).ToList();
            _allln.Sort((a, b) => a.Nombre.CompareTo(b.Nombre));
            List<Paquete> _allpq = new List<Paquete>();
            _allpq.Sort((a, b) => a.Descripcion.CompareTo(b.Descripcion));
            ViewData["esCancelable"] = _osRepo.IsCancelable(ordenId);
            ViewData["paquete"] = _allpq;
            ViewData["orden"] = eOS;
            ViewData["clientes"] = _allcl;
            ViewData["contratos"] = _allc;
            ViewData["servicios"] = _alls;
            ViewData["lineas"] = _allln;
            ViewData["ubicaciones"] = _allu;
            ViewData["siPaquete"] = (bool)settings.UsoDePaquetes;
            return View("~/Views/Servicio/EditOrdenServicio.cshtml");
        }

        /* Detalle Orden de servicio */
        public IActionResult IndexDetailOS(int ordenId)
        {
            OrdenServicio eOS = _osRepo.GetByIdOS(ordenId);
            eOS.Personal = _opRepo.GetOSbyIdOs(ordenId).ToList();
            eOS.Activos = _orAfRepo.GetOSbyIdOs(ordenId).ToList();
            eOS.Insumos = _orInsumoRepo.GetOSbyIdOs(ordenId).ToList();
            List<Cliente> _allcl = _clRepo.GetAll(true).ToList();
            _allcl.Sort((a, b) => a.RazonSocial.CompareTo(b.RazonSocial));
            List<Contrato> _allc = _cRepo.GetContratoByCliente(eOS.Cliente.Id).ToList();
            _allc.Sort((a, b) => a.Nombre.CompareTo(b.Nombre));
            List<Ubicacion> _allu = _uRepo.getUbicacionesByCliente(eOS.Cliente.Id).ToList();
            _allu.Sort((a, b) => a.Nombre.CompareTo(b.Nombre));
            List<Servicio> _alls = _sRepo.GetAll(true).ToList();
            _alls.Sort((a, b) => a.Nombre.CompareTo(b.Nombre));
            List<LineaNegocio> _allln = _lnRepo.GetAll(true).ToList();
            _allln.Sort((a, b) => a.Nombre.CompareTo(b.Nombre));
            List<Operador> _evidencias = _operRepo.GetAtencionServicio(ordenId).ToList();
            List<BitacoraEstatus> _allbe = _beRepo.GetAllByOSId(ordenId).ToList();
            Dictionary<int, Persona> personas = new Dictionary<int, Persona>();
            foreach (Operador oper in _evidencias)
            {
                if (oper.Persona.Id > 0)
                {
                    personas.Add(oper.Id, (Persona)_pRepo.GetById(oper.Persona.Id));
                }
            }
            if (!String.IsNullOrEmpty(eOS.Opcional2))
            {
                //string pattern = @"\w+";
                //Match m = Regex.Match(eOS.Opcional2, pattern);
                //if(m.Success)
                //if(m.Length == settings.FolioPrefix.Count()+settings.FolioDigitsLong){
                OrdenServicio osPrev = new OrdenServicio();
                try
                {
                    osPrev = _osRepo.GetOSbyFolio(eOS.Opcional2);
                }
                catch (SqlException e)
                {
                    Console.WriteLine("\n\neOS.Opcional2 {0} {1}\n\n", eOS.Opcional2, e.Message);
                }
                if (osPrev != null)
                    if (osPrev.Id > -1)
                    {
                        ViewData["osPrevia"] = osPrev.Id;
                        Console.WriteLine("\n\nosPrev {0}\n\n", osPrev.Id);
                    }
                    else
                    {
                        ViewData["osPrevia"] = -1;
                    }
                //}
            }
            ViewData["personas"] = personas;
            ViewData["evidencias"] = _evidencias;
            ViewData["orden"] = eOS;
            ViewData["clientes"] = _allcl;
            ViewData["contratos"] = _allc;
            ViewData["servicios"] = _alls;
            ViewData["lineas"] = _allln;
            ViewData["ubicaciones"] = _allu;
            ViewData["cambiosEstado"] = _allbe;
            ViewData["faceapiuso"] = (bool)settings.FaceApiUso;
            JsonSerializerSettings jsonSettings = new JsonSerializerSettings();
            jsonSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            jsonSettings.NullValueHandling = NullValueHandling.Ignore;
            if (User.IsInRole("Operador"))
            {
                var osTiempo = getOSenTiempoOperador().Result;
                Console.WriteLine("\ngetOSenTiempoOperador {0} {1}\n\n", osTiempo.Count, osTiempo);
                if (osTiempo.Count == 0)
                {
                    ViewData["OrdenServicioEnTiempo"] = new List<OrdenPersona>();
                }
                else
                {
                    ViewData["OrdenServicioEnTiempo"] = JsonConvert.SerializeObject(osTiempo, jsonSettings);
                }
            }
            return View("~/Views/Servicio/DetailOrdenServicio.cshtml");
        }

        public FileResult DescargarZip(int osId)
        {
            List<ZipItem> zipItems = new List<ZipItem>();
            List<Operador> atencionServicioList = _operRepo.GetAtencionServicio(osId).ToList();
            OrdenServicio os = null;
            foreach (Operador osAtendido in atencionServicioList)
            {
                os = osAtendido.OrdenServicio;
                foreach (Operador.Estado osAtendidoEstado in osAtendido.EstadoN)
                {
                    if (osAtendidoEstado.ArchivosEvidencia.Count > 0)
                    {
                        foreach (Operador.Estado.Archivo evidencia in osAtendidoEstado.ArchivosEvidencia)
                        {
                            zipItems.Add(new ZipItem(Utilities.Tools.QuitaSlash(evidencia.Name), new MemoryStream(evidencia.File)));
                        }
                    }
                }
            }
            Response.Headers.Add("Content-Disposition", "inline; filename=" + os.Folio + ".zip");
            return File(Zipper.Zip(zipItems), "application/zip");
        }

        public FileResult MostrarArchivo(int osId, int operadorEstadoId, int operadorEstadoArchivoId)
        {
            List<Operador> operador = _operRepo.GetAtencionServicio(osId).ToList();
            byte[] archivo = null;
            long TamanoArchivo = 0;
            string NombreArchivo = null;
            foreach (var op in operador)
            {
                foreach (var es in op.EstadoN)
                {
                    foreach (var ar in es.ArchivosEvidencia)
                    {
                        if (es.Id == operadorEstadoId && ar.Id == operadorEstadoArchivoId)
                        {
                            archivo = ar.File;
                            TamanoArchivo = ar.Size;
                            NombreArchivo = (new String(ar.Name)).IsNormalized() ? ar.Name : ar.Name.Normalize();
                            //Console.WriteLine(String.Format("es.Id {0} {1} operadorEstadoId, TamanoArchivo {2}, NombreArchivo {3}", es.Id, operadorEstadoId, TamanoArchivo, NombreArchivo));
                        }
                    }
                }
            }
            Response.Headers.Add("Content-Length", TamanoArchivo.ToString());
            Response.Headers.Add("Content-Disposition", "inline; filename=" + NombreArchivo);
            return File(archivo, "application/octet-stream");
        }

        public IActionResult DownloadOSFormat(int ordenId)
        {
            OrdenServicio eOS = _osRepo.GetByIdOS(ordenId);
            Response.Headers.Add("Content-Disposition", "inline; filename=OS-" + eOS.Folio + ".pdf");
            return new FileContentResult(OrdenServicioSigesTemplate.getOrdenServicioFormat(_hostingEnvironment.WebRootPath, eOS), "application/octet-stream")
            {
                FileDownloadName = "OS-" + eOS.Folio + ".pdf"
            };
        }
        public IActionResult DownloadOSFormatEnglish(int ordenId)
        {
            OrdenServicio eOS = _osRepo.GetByIdOS(ordenId);
            Response.Headers.Add("Content-Disposition", "inline; filename=OS-" + eOS.Folio + ".pdf");
            return new FileContentResult(OrdenServicioSigesTemplate.getOrdenServicioFormatEnglish(_hostingEnvironment.WebRootPath, eOS), "application/octet-stream")
            {
                FileDownloadName = "OS-" + eOS.Folio + ".pdf"
            };
        }

        [HttpPost]
        [Authorize(Roles = "Supervisor, SuperUser")]
        public IActionResult EditOS(OrdenServicioDTO nOS)
        {
            List<OrdenPersonalDTO> personal = new List<OrdenPersonalDTO>();
            List<OrdenInsumoDTO> insumos = new List<OrdenInsumoDTO>();
            List<OrdenActivoFijoDTO> activos = new List<OrdenActivoFijoDTO>();

            //TODO: Manejode excepciones NULL
            if (!String.IsNullOrEmpty(nOS.dtPersonas))
                foreach (var aP in JArray.Parse(nOS.dtPersonas))
                {
                    JObject p = JObject.Parse(aP.ToString());
                    personal.Add(new OrdenPersonalDTO { Id = Int32.Parse(p.Property("idordper").Value.ToString()), PersonaId = Int32.Parse(p.Property("idpersona").Value.ToString()) });
                    //Console.WriteLine("ESAG: OrdenServicioDTO nOS {0} nOS.dtPersonas {1}", nOS, nOS.dtPersonas);
                }

            //TODO: Manejode excepciones NULL
            if (!String.IsNullOrEmpty(nOS.dtActivos))
                foreach (var aA in JArray.Parse(nOS.dtActivos))
                {
                    JObject a = JObject.Parse(aA.ToString());
                    activos.Add(new OrdenActivoFijoDTO { Id = Int32.Parse(a.Property("idordact").Value.ToString()), ActivoFijoId = Int32.Parse(a.Property("idactivofijo").Value.ToString()) });
                }

            if (settings.UsoDePaquetes)
            {
                foreach (var aI in JArray.Parse(nOS.dtInsumos))
                {
                    JObject i = JObject.Parse(aI.ToString());
                    Console.WriteLine("\n\n JObject i {0}\n", i);
                    insumos.Add(
                        new OrdenInsumoDTO()
                        {
                            Id = Int32.Parse(i.Property("idordins").Value.ToString()),
                            InsumoId = Int32.Parse(i.Property("idinsumo").Value.ToString()),
                            /*LoteId = Int32.Parse(i.Property("idlote").Value.ToString()),*/
                            Lote = i.Property("lote").Value.ToString(),
                            /*Caducidad = i.Property("caducidad").Value.ToString(),*/
                            Cantidad = Int32.Parse(i.Property("cantidad").Value.ToString())
                        });
                }
            }
            else
            {

                //TODO: Manejode excepciones NULL
                if (!String.IsNullOrEmpty(nOS.dtInsumos))
                    foreach (var aI in JArray.Parse(nOS.dtInsumos))
                    {
                        JObject i = JObject.Parse(aI.ToString());
                        insumos.Add(new OrdenInsumoDTO
                        {
                            Id = Int32.Parse(i.Property("idordins").Value.ToString()),
                            InsumoId = Int32.Parse(i.Property("idinsumo").Value.ToString()),
                            Cantidad = Int32.Parse(i.Property("cantidad").Value.ToString())
                        });
                    }
                foreach (OrdenInsumoDTO oi in insumos)
                {
                    Console.WriteLine("\n\n ---> " + oi.Id + " " + oi.InsumoId + " " + oi.Cantidad);
                }
            }
            OrdenServicio os = _osRepo.GetByIdOS(nOS.Id);
            string EstatusORIGINAL = os.EstatusServicio;
            OrdenServicio osOriginal = os.Copia();
            CultureInfo cin = new CultureInfo("es-MX");
            //os.Folio = nOS.Folio;
            os.FechaInicio = String.IsNullOrEmpty(nOS.FechaInicio) ? (DateTime?)null : DateTime.Parse(nOS.FechaInicio, cin);
            os.FechaFin = String.IsNullOrEmpty(nOS.FechaFin) ? (DateTime?)null : DateTime.Parse(nOS.FechaFin, cin);
            os.Cliente = _clRepo.GetById(nOS.Cliente);
            os.Contrato = _cRepo.GetById(nOS.Contrato);
            os.Ubicacion = _uRepo.GetById(nOS.Ubicacion);
            os.LineaNegocio = _lnRepo.GetById(nOS.LineaNegocio);
            os.Servicio = _sRepo.GetById(nOS.Servicio);
            os.Tipo = nOS.Tipo;
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
            if (!(String.IsNullOrEmpty(nOS.NombreCompletoCC1) && String.IsNullOrEmpty(nOS.EmailCC1)))
            {
                os.NombreCompletoCC1 = nOS.NombreCompletoCC1;
                os.EmailCC1 = nOS.EmailCC1;
            }
            if (!(String.IsNullOrEmpty(nOS.NombreCompletoCC2) && String.IsNullOrEmpty(nOS.EmailCC2)))
            {
                os.NombreCompletoCC2 = nOS.NombreCompletoCC2;
                os.EmailCC2 = nOS.EmailCC2;
            }
            if (personal.Count > 0)
            {
                foreach (var i in os.Personal)
                {
                    i.Estatus = false;
                }
                foreach (var i in personal)
                {
                    if (i.Id > 0)
                    {
                        foreach (var x in os.Personal)
                        {
                            if (i.PersonaId == x.Persona.Id)
                            {
                                x.Estatus = true;
                            }
                        }
                    }
                    else
                    {
                        os.Personal.Add(new OrdenPersona() { Persona = _pRepo.GetById(i.PersonaId), Estatus = true });
                    }
                }
            }
            else if (os.Personal.Count > 0)
            {
                foreach (var i in os.Personal)
                {
                    i.Estatus = false;
                }
            }
            if (insumos.Count > 0)
            {
                foreach (var i in os.Insumos)
                {
                    i.Estatus = false;
                }
                foreach (var i in insumos)
                {
                    if (i.Id > 0)
                    {
                        foreach (var x in os.Insumos)
                        {
                            if (i.InsumoId == x.Insumo.Id)
                            {
                                x.Estatus = true;
                                x.Cantidad = i.Cantidad;
                            }
                        }
                    }
                    else
                    {
                        os.Insumos.Add(new OrdenInsumo()
                        {
                            Insumo = _iRepo.GetById(i.InsumoId),
                            /*Lote = _lRepo.GetById(i.LoteId),*/
                            Lote = i.Lote,
                            Caducidad = i.Caducidad,
                            Cantidad = i.Cantidad,
                            Estatus = true
                        });
                    }
                }
            }
            else if (os.Insumos.Count > 0)
            {
                foreach (var i in os.Insumos)
                {
                    i.Estatus = false;
                }
            }
            if (activos.Count > 0)
            {
                foreach (var i in os.Activos)
                {
                    i.Estatus = false;
                }
                foreach (var i in activos)
                {
                    if (i.Id > 0)
                    {
                        foreach (var x in os.Activos)
                        {
                            if (i.ActivoFijoId == x.ActivoFijo.Id)
                            {
                                x.Estatus = true;
                            }
                        }
                    }
                    else
                    {
                        os.Activos.Add(new OrdenActivoFijo() { ActivoFijo = _afRepo.GetById(i.ActivoFijoId), Estatus = true });
                    }
                }
            }
            else if (os.Activos.Count > 0)
            {
                foreach (var i in os.Activos)
                {
                    i.Estatus = false;
                }
            }
            if (os.EstatusServicio == "programado")/*if (os.EstatusServicio == "solicitado")*/
            {
                if (siges.Utilities.Tools.ValidaCamposCompletosOSyP(os, _pRepo.GetByEmail(this.User.Identity.Name).Single()))
                {
                    Notifica notifica = new Utilities.Notifica(_emailConf, _userManager, _roleManager, " PROGRAMADA", new MimeKit.Multipart("Mixed"), this.User.Identity.Name.ToString(), settings);
                    notifica.OrdenServicioNotifica("cliente", "programado", null, os, null);
                    notifica.OrdenServicioNotifica("operador", "programado", null, os, null);
                    notifica.OrdenServicioNotifica("ventas", "programado", null, os, null);
                }
            }
            if (os.EstatusServicio == "reprogramado")
            {
                if (siges.Utilities.Tools.ValidaCamposCompletosOSyP(os, _pRepo.GetByEmail(this.User.Identity.Name).Single()))
                {
                    Notifica notifica = new Utilities.Notifica(_emailConf, _userManager, _roleManager, "RE-PROGRAMADA", new MimeKit.Multipart("Mixed"), this.User.Identity.Name.ToString(), settings);
                    notifica.OrdenServicioNotifica("cliente", "reprogramado", null, os, null);
                    notifica.OrdenServicioNotifica("operador", "reprogramado", null, os, null);
                    notifica.OrdenServicioNotifica("ventas", "reprogramado", null, os, null);
                }
            }
            if (os.EstatusServicio == "cancelado" && (EstatusORIGINAL == "programado" || EstatusORIGINAL == "reprogramado"))
            {
                if (siges.Utilities.Tools.ValidaCamposCompletosOSyP(os, _pRepo.GetByEmail(this.User.Identity.Name).Single()))
                {
                    Notifica notifica = new Utilities.Notifica(_emailConf, _userManager, _roleManager, "CANCELADO", new MimeKit.Multipart("Mixed"), this.User.Identity.Name.ToString(), settings);
                    notifica.OrdenServicioNotifica("cliente", "cancelado", null, os, null);
                    notifica.OrdenServicioNotifica("operador", "cancelado", null, os, null);
                    notifica.OrdenServicioNotifica("ventas", "cancelado", null, os, null);
                }
            }
            if (os.EstatusServicio == "finalizado")
            {
                List<Operador> osAtendido = _operRepo.GetAtencionServicio(os.Id).ToList();
                if (osAtendido.Count > 0)
                {
                    os.PersonaValida = _pRepo.GetByEmail(this.User.Identity.Name).Single();
                    if (siges.Utilities.Tools.ValidaCamposCompletosOSyP(os, _pRepo.GetByEmail(this.User.Identity.Name).Single()))
                    {
                        if (os.LineaNegocio.Nombre == "INSTALACION" || os.LineaNegocio.Nombre == "MANTENIMIENTO")
                        {
                            Notifica notifica = new Utilities.Notifica(_emailConf, _userManager, _roleManager, "Encuesta de satisfacción", new MimeKit.Multipart("Mixed"), this.User.Identity.Name.ToString(), settings);
                            notifica.OrdenServicioNotifica("cliente", "finaliza", osAtendido, osAtendido[0].OrdenServicio, _beRepo.GetFinalizadoDescriptionByOSid(osAtendido[0].OrdenServicio.Id).ToList()[0].Description);
                            notifica.OrdenServicioNotifica("administracion", "finaliza", osAtendido, osAtendido[0].OrdenServicio, _beRepo.GetFinalizadoDescriptionByOSid(osAtendido[0].OrdenServicio.Id).ToList()[0].Description);
                            notifica.OrdenServicioNotifica("ventas", "finaliza", osAtendido, osAtendido[0].OrdenServicio, _beRepo.GetFinalizadoDescriptionByOSid(osAtendido[0].OrdenServicio.Id).ToList()[0].Description);
                        }
                        else
                        {
                            Notifica notifica = new Utilities.Notifica(_emailConf, _userManager, _roleManager, "Servicio Finalizado", new MimeKit.Multipart("Mixed"), this.User.Identity.Name.ToString(), settings);
                            notifica.OrdenServicioNotifica("cliente", "finaliza", osAtendido, osAtendido[0].OrdenServicio, _beRepo.GetFinalizadoDescriptionByOSid(osAtendido[0].OrdenServicio.Id).ToList()[0].Description);
                            notifica.OrdenServicioNotifica("administracion", "finaliza", osAtendido, osAtendido[0].OrdenServicio, _beRepo.GetFinalizadoDescriptionByOSid(osAtendido[0].OrdenServicio.Id).ToList()[0].Description);
                            notifica.OrdenServicioNotifica("ventas", "finaliza", osAtendido, osAtendido[0].OrdenServicio, _beRepo.GetFinalizadoDescriptionByOSid(osAtendido[0].OrdenServicio.Id).ToList()[0].Description);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("\n\n No es posible cambiar de estatus ya que no se ha registrado ninguna actividad en el cliente... OS id = {0} folio {1} intento realizado por {2}\n\n", os.Id, os.Folio, this.User.Identity.Name);
                    os.EstatusServicio = EstatusORIGINAL;
                    ViewData["sinActividadRegistrada"] = os;
                    return View("~/Views/Servicios/SinActividadRegistrada.cshtml");
                }
            }
            _osRepo.Update(os);
            LoggedUser = this.User.Identity.Name;
            _bRepo.Insert(new Bitacora() { UserId = LoggedUser, EventDate = DateTime.Now, Event = "Update", Section = "Orden de servicio", Description = "Orden de servicio no. " + nOS.Id + " actualizada." });
            // Guarda en BitacoraEstatus
            /*string nuevoEstatus = CambioEstatusOSa(osOriginal, os);
              if (nuevoEstatus != "NoCambio") {
              _beRepo.Insert(new BitacoraEstatus(){Os = os, Folio = os.Folio.ToUpper(), QuienCambia = _pRepo.GetByEmail(this.User.Identity.Name), Email = this.User.Identity.Name.ToUpper(), De = osOriginal.EstatusServicio.ToUpper(), A = nuevoEstatus.ToUpper(), FechaAdministrativa = DateTime.Now, Description = "SUPERVISOR"});
              }*/
            return RedirectToAction("IndexListOS", "Servicio");
        }
        public IActionResult CapturaComentario(BitacoraEstatusDTO be)
        {
            OrdenServicio os = _osRepo.GetByIdOS(be.OrdenServicioId);
            os.EstatusServicio = be.A;
            _osRepo.Update(os);
            _beRepo.Insert(new BitacoraEstatus()
            {
                Os = os,
                Folio = os.Folio.ToUpper(),
                QuienCambia = _pRepo.GetByEmail(this.User.Identity.Name).Single(),
                Email = this.User.Identity.Name.ToUpper(),
                De = be.De.ToUpper(),
                A = be.A.ToUpper(),
                FechaAdministrativa = DateTime.Now,
                Description = be.MotivoCambio
            });
            return RedirectToAction("IndexEditOS", "Servicio", new { ordenId = be.OrdenServicioId });
        }

        /* --------- Resumen ------------ */
        public IActionResult IndexResumen()
        {
            List<Ubicacion> _allu = _uRepo.GetAll(true).ToList();
            ViewData["ubicaciones"] = _allu;
            return View("~/Views/Servicios/Resumen.cshtml");
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
            return "NoCambio";
        }
    }
}