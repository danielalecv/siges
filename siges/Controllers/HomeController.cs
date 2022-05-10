using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using siges.Data;
using siges.Models;
using siges.Repository;
using siges.Areas.Identity.Data;
using siges.Models.ViewModels;

// using SendGrid's C# Library
// https://github.com/sendgrid/sendgrid-csharp
//using SendGrid;
//using SendGrid.Helpers.Mail;

namespace siges.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRoatechIdentityUserRepo roaIdenUsrRepo;
        private readonly ApplicationDbContext context;
        private readonly IMailSupport _mailSupport;

        public HomeController(ApplicationDbContext _ctx, IRoatechIdentityUserRepo _riuR, IMailSupport mailSupport)
        {
            roaIdenUsrRepo = _riuR;
            context = _ctx;
            _mailSupport = mailSupport;
        }

        public IActionResult SoporteTecnico()
        {
            ViewData["Message"] = "";
            return View();
        }

        [HttpPost]
        public IActionResult SoporteTecnico(SupportViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var name = model.Name;
                    var lastname = model.LastName;
                    var email = model.Email;
                    var priority = model.Priority;
                    var title = model.Title;
                    var body = model.Body;
                    var screenshots = model.Screenshots;

                    Console.WriteLine("nombre: " + name);
                    Console.WriteLine("apellido: " + lastname);
                    Console.WriteLine("email: " + email);
                    Console.WriteLine("prioridad: " + priority);
                    Console.WriteLine("titulo: " + title);
                    Console.WriteLine("body: " + body);

                    if (screenshots != null)
                    {
                        Console.WriteLine("screenshots: " + screenshots.Count);
                        if (_mailSupport.SendMessage(name, lastname, email, priority, title, body, screenshots))
                        {
                            ViewData["Message"] = "Hemos recibido tu problematica, trabajaremos en la solución";
                        }
                        else
                        {
                            ViewData["Message"] = "Hubo un error al enviar tu problema, intenta de nuevo";
                        }
                    }
                    else
                    {
                        if (_mailSupport.SendMessage(name, lastname, email, priority, title, body))
                        {
                            ViewData["Message"] = "Hemos recibido tu problematica, trabajaremos en la solución";
                        }
                        else
                        {
                            ViewData["Message"] = "Hubo un error al enviar tu problema, intenta de nuevo";
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return View();
            }
            return View();
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Operador"))
                    return RedirectToAction("Index", "Operador");
                RoatechIdentityUser riu = roaIdenUsrRepo.GetAllInfoByEmail(User.Identity.Name).Single();
                ViewData["riu"] = riu;
                if (User.IsInRole("Cliente"))
                {
                    string razonSocial = "";
                    var conn = RelationalDatabaseFacadeExtensions.GetDbConnection(context.Database);
                    if (((System.Data.SqlClient.SqlConnection)conn).State == System.Data.ConnectionState.Open)
                        ((System.Data.SqlClient.SqlConnection)conn).Close();
                    ((System.Data.SqlClient.SqlConnection)conn).Open();
                    SqlCommand cmd = new SqlCommand("select cl.razonsocial from clienteidentity ci left join cliente cl on ci.clienteid = cl.id where cuentausuarioid = @RiuId", (System.Data.SqlClient.SqlConnection)conn);
                    cmd.Parameters.Add("@RiuId", SqlDbType.NVarChar);
                    cmd.Parameters["@RiuId"].Value = riu.Id;
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            razonSocial = (string)dataReader[0];
                        }
                    }
                    dataReader.Close();
                    if (((System.Data.SqlClient.SqlConnection)conn).State == System.Data.ConnectionState.Open)
                        ((System.Data.SqlClient.SqlConnection)conn).Close();
                    ViewData["cliente"] = razonSocial;
                }
                return View();
            }
            return NotFound();
        }

        public IActionResult Privacy()
        {
            //Execute().Wait();
            Console.WriteLine("Privacy");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /*static async Task Execute() {
          var apiKey = Environment.GetEnvironmentVariable("SEND_GRID");
          var client = new SendGridClient(apiKey);
          var from = new EmailAddress("erick.agutierrez@roatech.com.mx", "noreply.");
          var subject = "Sending with SendGrid is Fun";
          var to = new EmailAddress("ericksalvadoral@gmail.com", "SendGrid - Example User");
          var plainTextContent = "and easy to do anywhere, even with C#";
          var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
          var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
          var response = await client.SendEmailAsync(msg);
        }*/
    }
}
