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
        private Mock<IPersonaRepository> _pRepo = new Mock<IPersonaRepository>();
        private Mock<IRoatechIdentityUserRepo> riuRepo = new Mock<IRoatechIdentityUserRepo>();
        private Mock<IMailSupport> _mailSupport = new Mock<IMailSupport>();
        private Mock<IClienteRepository> _cliRepo = new Mock<IClienteRepository>();
        private Mock<IClientContactService> _contactClient = new Mock<IClientContactService>();

        private static string validToken = "succesToken12345";
        private Persona persona = new Persona() { Nombre = "Daniel Alejandro", Paterno = "Camacho", Materno = "Vazquez", Email = "daniel.alecv5@gmail.com", Token = validToken };
        private static Persona personaCreate = new Persona() { Nombre = "Daniel Alejandro", Paterno = "Camacho", Materno = "Vazquez", Email = "daniel.alecv5@gmail.com", Token = validToken };
        private static Cliente cliente = new Cliente() { Id = 1, RazonSocial = "Cliente 1", RFC = "PFI201020Y60", Estatus = true };
        private static Persona newContact = new Persona() { Nombre = "nuevo", Paterno = "nuevo", Materno = "nuevo", Email = "nuevo@gmail.com" };
        private static ContactoCliente contactoCliente = new ContactoCliente() { Cliente = new Cliente() { RazonSocial = "Cliente prueba" } };
        private static List<Persona> emptycontacts = new List<Persona>();
        private static List<Persona> contacts = new List<Persona>() { newContact };

        private static ContactoCliente contactoClienteCreate = new ContactoCliente() { Id= 1, Estatus = true, Cliente = cliente, Contactos = new List<Persona>() { personaCreate } };
        private static ContactoClienteDTO contactoClienteDTO = new ContactoClienteDTO() { clienteId = 1, contactos = contacts, opcional1 = "opcional 1", opcional2 = "opcional 2" };

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

        /*Create
        [TestMethod]
        public void BadId()
        {
            //Moq user identity
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.Name, "daniel.alecv5@gmail.com"),
                                   }, "RoatechIdentityUser"));

            //--------------------Entity Framework mock------------------------------------------------
            //create In Memory Database
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "SSM4_test")
            .Options;

            //// Create mocked Context by seeding Data as per Schema///
            using (var context = new ApplicationDbContext(options))
            {
                context.Persona.Add(persona);
                context.Cliente.Add(cliente);
                context.ContactoCliente.Add(contactoClienteCreate);
                context.SaveChanges();
            }

            // Use a Context instance  with Data to run the test for your Business code 
            using (var context = new ApplicationDbContext(options))
            {
                ContactoClienteController controller = new ContactoClienteController(context, _bRepo.Object, _ccRepo.Object, _pRepo.Object, riuRepo.Object, _mailSupport.Object, _cliRepo.Object, _contactClient.Object);
                //controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

                var ar = controller.Create(new ContactoClienteDTO()) as ViewResult;
                var viewName = ar.ViewName;

                Assert.IsTrue(string.IsNullOrEmpty(viewName) || viewName == "~/Views/ContactoCliente/Index.cshtml");
                //Assert.AreEqual("~/Views/ContactoCliente/Index.cshtml", ar.ViewName);
                //Assert.AreEqual("", ar.ViewData["Hola"]);
            }
        }*/
    }
}
