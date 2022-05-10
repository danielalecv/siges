using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using siges.Controllers;
using siges.Data;
using siges.Repository;
using siges.Models;
using siges.Models.ViewModels;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace siges_test
{
    [TestClass]
    public class HomeControllerTest
    {
        private string success = "Hemos recibido tu problematica, trabajaremos en la solución";
        private string fail = "Hubo un error al enviar tu problema, intenta de nuevo";
        private string name = "Daniel";
        private string lastname = "Camacho";
        private string email = "dani@mail.com";
        private string priority = "alta";
        private string title = "Error seccion 1";
        private string body = "Se genero un error en la seccion 1, se desconoce la problematica, pero no abre la pagina";
        private Persona persona = new Persona()
        {
            Nombre = "Daniel Alejandro",
            Paterno = "Camacho",
            Materno = "Vazquez",
            Email = "daniel.alecv5@gmail.com"
        };

        private Mock<IRoatechIdentityUserRepo> riuR = new Mock<IRoatechIdentityUserRepo>();
        private Mock<IMailSupport> mailSupport = new Mock<IMailSupport>();
        private Mock<IList<IFormFile>> screenshots = new Mock<IList<IFormFile>>();

        [TestMethod]
        public void GetViewSupport()
        {
            //--------------------Entity Framework mock------------------------------------------------
            //create In Memory Database
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "SSM4_test")
            .Options;

            //// Create mocked Context by seeding Data as per Schema///
            using (var context = new ApplicationDbContext(options))
            {
                context.Persona.Add(persona);
                context.SaveChanges();
            }

            // Use a Context instance  with Data to run the test for your Business code 
            using (var context = new ApplicationDbContext(options))
            {
                HomeController controller = new HomeController(context, riuR.Object, mailSupport.Object);

                var ar = controller.SoporteTecnico() as ViewResult;
                Assert.AreEqual("", ar.ViewData["Message"]);
            }
        }

        [TestMethod]
        public void PostViewSupportNoFile()
        {
            mailSupport.Setup(m => m.SendMessage(name, lastname, email, priority, title, body)).Returns(true);
            //--------------------Entity Framework mock------------------------------------------------
            //create In Memory Database
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "SSM4_test")
            .Options;

            //// Create mocked Context by seeding Data as per Schema///
            using (var context = new ApplicationDbContext(options))
            {
                context.Persona.Add(persona);
                context.SaveChanges();
            }

            // Use a Context instance  with Data to run the test for your Business code 
            using (var context = new ApplicationDbContext(options))
            {
                HomeController controller = new HomeController(context, riuR.Object, mailSupport.Object);

                var ar = controller.SoporteTecnico(new SupportViewModel()
                {
                    Name = name,
                    LastName = lastname,
                    Email = email,
                    Priority = priority,
                    Title = title,
                    Body = body
                }) as ViewResult;

                Assert.AreEqual(success, ar.ViewData["Message"]);
            }
        }

        [TestMethod]
        public void FailPostViewSupportNoFile()
        {
            mailSupport.Setup(m => m.SendMessage(name, lastname, email, priority, title, body)).Returns(true);
            //--------------------Entity Framework mock------------------------------------------------
            //create In Memory Database
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "SSM4_test")
            .Options;

            //// Create mocked Context by seeding Data as per Schema///
            using (var context = new ApplicationDbContext(options))
            {
                context.Persona.Add(persona);
                context.SaveChanges();
            }

            // Use a Context instance  with Data to run the test for your Business code 
            using (var context = new ApplicationDbContext(options))
            {
                HomeController controller = new HomeController(context, riuR.Object, mailSupport.Object);

                var ar = controller.SoporteTecnico(new SupportViewModel()
                {
                    Name = "Alejandro",
                    LastName = lastname,
                    Email = email,
                    Priority = priority,
                    Title = title,
                    Body = body
                }) as ViewResult;

                Assert.AreEqual(fail, ar.ViewData["Message"]);
            }
        }

        [TestMethod]
        public void PostViewSupportWithFile()
        {

            mailSupport.Setup(m => m.SendMessage(name, lastname, email, priority, title, body, screenshots.Object)).Returns(true);
            //--------------------Entity Framework mock------------------------------------------------
            //create In Memory Database
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "SSM4_test")
            .Options;

            //// Create mocked Context by seeding Data as per Schema///
            using (var context = new ApplicationDbContext(options))
            {
                context.Persona.Add(persona);
                context.SaveChanges();
            }

            // Use a Context instance  with Data to run the test for your Business code 
            using (var context = new ApplicationDbContext(options))
            {
                HomeController controller = new HomeController(context, riuR.Object, mailSupport.Object);

                var ar = controller.SoporteTecnico(new SupportViewModel()
                {
                    Name = name,
                    LastName = lastname,
                    Email = email,
                    Priority = priority,
                    Title = title,
                    Body = body,
                    Screenshots = screenshots.Object
                }) as ViewResult;

                Assert.AreEqual(success, ar.ViewData["Message"]);
            }
        }

        [TestMethod]
        public void FailPostViewSupportWithFile()
        {

            mailSupport.Setup(m => m.SendMessage(name, lastname, email, priority, title, body, screenshots.Object)).Returns(true);
            //--------------------Entity Framework mock------------------------------------------------
            //create In Memory Database
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "SSM4_test")
            .Options;

            //// Create mocked Context by seeding Data as per Schema///
            using (var context = new ApplicationDbContext(options))
            {
                context.Persona.Add(persona);
                context.SaveChanges();
            }

            // Use a Context instance  with Data to run the test for your Business code 
            using (var context = new ApplicationDbContext(options))
            {
                HomeController controller = new HomeController(context, riuR.Object, mailSupport.Object);

                var ar = controller.SoporteTecnico(new SupportViewModel()
                {
                    Name = "Alejandro",
                    LastName = lastname,
                    Email = email,
                    Priority = priority,
                    Title = title,
                    Body = body,
                    Screenshots = screenshots.Object
                }) as ViewResult;

                Assert.AreEqual(fail, ar.ViewData["Message"]);
            }
        }
    }
}
