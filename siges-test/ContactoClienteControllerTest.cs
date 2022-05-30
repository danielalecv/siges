using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using siges.Controllers;
using siges.Data;
using siges.DTO;
using siges.Models;
using siges.Repository;
using siges.Services;
using System.Collections.Generic;
using System.Security.Claims;

namespace siges_test
{
    [TestClass]
    public class ContactoClienteControllerTest
    {
        private Mock<IBitacoraRepository> _bRepo = new Mock<IBitacoraRepository>();
        private Mock<IContactoCliente> _ccRepo = new Mock<IContactoCliente>();
        private Mock<IClienteRepository> _cliRepo = new Mock<IClienteRepository>();
        private Mock<IPersonaRepository> _pRepo = new Mock<IPersonaRepository>();
        private Mock<IRoatechIdentityUserRepo> riuRepo = new Mock<IRoatechIdentityUserRepo>();
        private Mock<IMailSupport> _mailSupport = new Mock<IMailSupport>();
        private Mock<IClientContactService> _contactClient = new Mock<IClientContactService>();

        private static string validToken = "succesToken12345";
        private Persona persona = new Persona()
        {
            Nombre = "Daniel Alejandro",
            Paterno = "Camacho",
            Materno = "Vazquez",
            Email = "daniel.alecv5@gmail.com",
            Token = validToken
        };
        private Persona personaUser = new Persona()
        {
            Id = 1,
            Nombre = "Daniel Alejandro",
            Paterno = "Camacho",
            Materno = "Vazquez",
            Email = "daniel.alecv5@gmail.com",
            Token = validToken
        };
        private ContactoCliente contactoCliente = new ContactoCliente()
        {
            Cliente = new Cliente() { RazonSocial="Cliente prueba"}
        };
        private static List<Persona> badcontactos = new List<Persona>();
        private ContactoClienteDTO badContactoClienteDTO = new ContactoClienteDTO() { clienteId = 0, contactos = badcontactos };

        //ConfirmContact
        [TestMethod]
        public void BadToken()
        {
            _pRepo.Setup(p => p.GetByToken(validToken)).Returns(persona);
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
                ContactoClienteController controller = new ContactoClienteController(context, _bRepo.Object, _ccRepo.Object, _pRepo.Object, riuRepo.Object, _mailSupport.Object, _cliRepo.Object, _contactClient.Object);

                var ar = controller.ConfirmContact("badToken12345") as ViewResult;
                Assert.AreEqual(siges.Utilities.Constants.ContactoCliente.ConfirmFail, ar.ViewData["Message"]);
            }
        }

        [TestMethod]
        public void SuccessConfirm()
        {
            _pRepo.Setup(p => p.GetByToken(validToken)).Returns(persona);
            _ccRepo.Setup(c => c.GetByClienteToken(persona)).Returns(contactoCliente);
            _contactClient.Setup(cc => cc.UpdatePerson(persona)).Returns(true);
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
                ContactoClienteController controller = new ContactoClienteController(context, _bRepo.Object, _ccRepo.Object, _pRepo.Object, riuRepo.Object, _mailSupport.Object, _cliRepo.Object, _contactClient.Object);

                var ar = controller.ConfirmContact("succesToken12345") as ViewResult;
                Assert.AreEqual(siges.Utilities.Constants.ContactoCliente.ConfirmSuccess + contactoCliente.Cliente.RazonSocial, ar.ViewData["Message"]);
            }
        }

        //Create
        [TestMethod]
        public void BadId()
        {
            //Moq user identity
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier, "SomeValueHere"),
                                        new Claim(ClaimTypes.Name, "daniel.alecv5@gmail.com")
                                        // other required and custom claims
                                   }, "TestAuthentication"));

            //--------------------Entity Framework mock------------------------------------------------
            //create In Memory Database
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "SSM4_test")
            .Options;

            //// Create mocked Context by seeding Data as per Schema///
            using (var context = new ApplicationDbContext(options))
            {
                context.Persona.Add(personaUser);
                context.SaveChanges();
            }

            // Use a Context instance  with Data to run the test for your Business code 
            using (var context = new ApplicationDbContext(options))
            {
                ContactoClienteController controller = new ContactoClienteController(context, _bRepo.Object, _ccRepo.Object, _pRepo.Object, riuRepo.Object, _mailSupport.Object, _cliRepo.Object, _contactClient.Object);
                controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

                var ar = controller.Create(badContactoClienteDTO) as ViewResult;
                var viewName = ar.ViewName;

                Assert.IsTrue(string.IsNullOrEmpty(viewName) || viewName == "~/Views/ContactoCliente/Index.cshtml");
                //Assert.AreEqual("~/Views/ContactoCliente/Index.cshtml", ar.ViewName);
            }
        }
    }
}
