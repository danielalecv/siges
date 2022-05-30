using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using siges.Areas.Identity.Data;
using siges.Data;
using siges.Models;
using siges.Repository;

namespace siges.Controllers {
  [Authorize(Roles = "Cliente, SuperUser")]
  public class ClientController : Controller {
    private readonly IPersonaRepository peRepo;
    private readonly IOrdenServicioRepository osRepo;
    private readonly IOrdenPersona opRepo;
    private readonly ApplicationDbContext context;
    private readonly UserManager<RoatechIdentityUser> userManager;

    public ClientController(IPersonaRepository _peR, IOrdenServicioRepository _osR, IOrdenPersona _opR, ApplicationDbContext _cntx, UserManager<RoatechIdentityUser> _um){
      peRepo = _peR;
      osRepo = _osR;
      opRepo = _opR;
      context = _cntx;
      userManager = _um;
    }

    /*public IActionResult Index() {
      SqlConnection myConnection = new SqlConnection("Server=192.168.1.221,1499;Database=siges_covid19_new;user id=sa;password=D3v1nst#");
      myConnection.Open();
      SqlCommand objcmd = new SqlCommand("SELECT * FROM AspNetUsers", myConnection);
      //objcmd.ExecuteNonQuery();      
      SqlDataAdapter adp = new SqlDataAdapter(objcmd);
      DataTable dt = new DataTable();
      adp.Fill(dt);
      dt.ToString();

      //DataTable dt = Datos.Buscar(textBox1.Text);//envio dato a buscar
      //si encuentra el dato guardo los datos en las variables
      if (dt.Rows.Count > 0) {
        DataRow row = dt.Rows[0];
        //guardo datos en variables
        string apellido = Convert.ToString(row["UserName"]);
        string edad = Convert.ToString(row["UserName"]);
        ViewData["apellido"] = apellido;
        ViewData["edad"] = edad;
      }
      return View(dt);
    }*/

    // GET: Classes/Details/5
    public IActionResult Details(int? id) {
      return View();
    }

    public IActionResult Staff() {
      if(User.Identity.IsAuthenticated){
        try{
          var conn = RelationalDatabaseFacadeExtensions.GetDbConnection(context.Database);
          string riuId = userManager.GetUserIdAsync(userManager.FindByEmailAsync(User.Identity.Name).Result).Result;
          List<int> pIdL = null;
          List<Persona> pList = null;

          if(((SqlConnection)conn).State == System.Data.ConnectionState.Open)
            ((SqlConnection)conn).Close();
          ((SqlConnection) conn).Open();
          SqlCommand cmd = new SqlCommand("select p.id from clienteidentity clid left join cliente cl on clid.clienteid = cl.id left join ordenservicio os on cl.id = os.clienteid left join ordenpersona op on os.id = op.ordenservicioid left join persona p on op.personaid = p.id left join aspnetusers usr on clid.cuentausuarioid = usr.id where p.estatus = 1 and usr.Id = @Id group by p.id", (SqlConnection) conn);
          cmd.Parameters.Add("@Id", SqlDbType.NVarChar);
          cmd.Parameters["@Id"].Value = riuId;
          SqlDataReader dataReader = cmd.ExecuteReader();
          if(dataReader.HasRows){
            pIdL = new List<int>();
            pList = new List<Persona>();
            while(dataReader.Read()){
              pIdL.Add((int)dataReader[0]);
            }
          }
          if(((SqlConnection)conn).State == System.Data.ConnectionState.Open)
            ((SqlConnection)conn).Close();
          if(pIdL != null && pIdL.Count > 0)
            foreach(int id in pIdL)
              pList.Add(peRepo.GetById(id));
          if(pList != null && pList.Count > 0)
            return View(pList);
          else
            return View();
        } catch (Exception e) {
          return Json(new{success=false, data=e});
        }
      }
      return View();
    }

    [HttpGet]
    public IActionResult StaffDetail(int? id){
      if(User.Identity.IsAuthenticated){
        if(id != null && id > 0){
          try{
            Persona persona = peRepo.GetById((int)id);
            if(persona != null)
              return Json(new{success=true, data=persona});
            else
              return NotFound();
          } catch (Exception e){
            return Json(new{success=false, data=e});
          }
        }
        return NotFound();
      }
      return View();
    }

    public IActionResult Services() {
      if(User.Identity.IsAuthenticated){
        try{
          var conn = RelationalDatabaseFacadeExtensions.GetDbConnection(context.Database);
          string riuId = userManager.GetUserIdAsync(userManager.FindByEmailAsync(User.Identity.Name).Result).Result;
          List<int> osIdL = null;
          List<OrdenServicio> osList = null;

          if(((SqlConnection)conn).State == System.Data.ConnectionState.Open)
            ((SqlConnection)conn).Close();
          ((SqlConnection) conn).Open();
          SqlCommand cmd = new SqlCommand("select os.id from clienteidentity clid left join cliente cl on clid.clienteid = cl.id left join ordenservicio os on cl.id = os.clienteid left join aspnetusers usr on clid.cuentausuarioid = usr.id where os.estatus = 1 and usr.Id = @Id group by os.id", (SqlConnection) conn);
          cmd.Parameters.Add("@Id", SqlDbType.NVarChar);
          cmd.Parameters["@Id"].Value = riuId;
          SqlDataReader dataReader = cmd.ExecuteReader();
          if(dataReader.HasRows){
            osIdL = new List<int>();
            osList = new List<OrdenServicio>();
            while(dataReader.Read()){
              osIdL.Add((int)dataReader[0]);
            }
          }
          if(((SqlConnection)conn).State == System.Data.ConnectionState.Open)
            ((SqlConnection)conn).Close();
          if(osIdL != null && osIdL.Count > 0)
            foreach(int id in osIdL)
              osList.Add(osRepo.GetByIdOS(id));
          if(osList != null && osList.Count > 0)
            return View(osList);
          else
            return View();
        } catch (Exception e) {
          return Json(new{success=false, data=e});
        }
      }
      return View();
    }

    [HttpGet]
    public IActionResult ServicesDetail(int? id) {
      if(User.Identity.IsAuthenticated){
        if(id != null && id > 0){
          try{
            List<OrdenPersona> servicioDetail = opRepo.GetOrdenServicioDetail((int) id).ToList();
            if(servicioDetail.Count > 0)
              return Json(new{success=true, data=servicioDetail});
            else
              return NotFound();
          } catch (Exception e){
            return Json(new{success=false, data=e});
          }
        }
        return NotFound();
      }
      return View();
    }

    public IActionResult Summary() {
          var conn = RelationalDatabaseFacadeExtensions.GetDbConnection(context.Database);
          string riuId = userManager.GetUserIdAsync(userManager.FindByEmailAsync(User.Identity.Name).Result).Result;
          List<int> osIdL = null;
          List<OrdenServicio> osList = null;

          if(((SqlConnection)conn).State == System.Data.ConnectionState.Open)
            ((SqlConnection)conn).Close();
          ((SqlConnection) conn).Open();
          SqlCommand cmd = new SqlCommand("SELECT ROW_NUMBER() OVER (ORDER BY se.Id) as Id, se.Nombre AS Servicio, count(*) as NumServicio FROM OrdenServicio os INNER JOIN Servicio se ON os.ServicioId = se.Id left join cliente cl on os.clienteid = cl.id left join clienteidentity clid on cl.id = clid.clienteid left join aspnetusers usr on clid.cuentausuarioid = usr.id where usr.id = @Id and os.estatus = 1 GROUP BY se.Id, se.Nombre", (SqlConnection) conn);
          cmd.Parameters.Add("@Id", SqlDbType.NVarChar);
          cmd.Parameters["@Id"].Value = riuId;
          SqlDataAdapter adp = new SqlDataAdapter(cmd);
          DataTable dt = new DataTable();
          adp.Fill(dt);
          dt.ToString();



          //SqlDataReader dataReader = cmd.ExecuteReader();
          //if(dataReader.HasRows){
            //osIdL = new List<int>();
            //osList = new List<OrdenServicio>();
            //while(dataReader.Read()){


      //SqlConnection myConnection = new SqlConnection("Server=192.168.1.221,1499;Database=siges_covid19_new;user id=sa;password=D3v1nst#");
      //myConnection.Open();
      //SqlCommand objcmd = new SqlCommand("SELECT ROW_NUMBER() OVER (ORDER BY se.Id) as Id, se.Nombre AS Servicio, count(*) as NumServicio FROM OrdenServicio os INNER JOIN Servicio se ON os.ServicioId = se.Id GROUP BY se.Id, se.Nombre", myConnection);
      //SqlDataAdapter adp = new SqlDataAdapter(objcmd);
      //DataTable dt = new DataTable();
      //adp.Fill(dt);
      //dt.ToString();

      if (dt.Rows.Count > 0) {
        return View(dt);
      } else {
        return View();
      }
    }
  }
}
