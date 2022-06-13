using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using siges.Areas.Identity.Data;
using siges.Controllers;
using siges.Data;
using siges.DTO;
using siges.Models;
using siges.Repository;
using siges.Utilities;

namespace siges_test
{
    [TestClass]
    public class ServicioControllerTest
    {
        private Mock<IConfiguracionServicioRepository> _csRepo = new Mock<IConfiguracionServicioRepository>();
        private Mock<IContratoRepository> _cRepo = new Mock<IContratoRepository>();
        private Mock<IServicioRepository> _sRepo = new Mock<IServicioRepository>();
        private Mock<ILineaNegocioRepository> _lnRepo = new Mock<ILineaNegocioRepository>();
        private Mock<IOrdenServicioRepository> _osRepo = new Mock<IOrdenServicioRepository>();
        private Mock<IUbicacionRepository> _uRepo = new Mock<IUbicacionRepository>();
        private Mock<IPersonaRepository> _pRepo = new Mock<IPersonaRepository>();
        private Mock<IInsumoRepository> _iRepo = new Mock<IInsumoRepository>();
        private Mock<IActivoFijoRepository> _afRepo = new Mock<IActivoFijoRepository>();
        private Mock<IClienteRepository> _clRepo = new Mock<IClienteRepository>();
        private Mock<IBitacoraRepository> _bRepo = new Mock<IBitacoraRepository>();
        private Mock<IOperador> _operRepo = new Mock<IOperador>();
        private Mock<UserManager<RoatechIdentityUser>> _userManager = new Mock<UserManager<RoatechIdentityUser>>(Mock.Of<IUserStore<RoatechIdentityUser>>(), null, null, null, null, null, null, null, null);
        //private Mock<RoleManager<IdentityRole>> _roleManager = new Mock<IConfiguracionServicioRepository>();
        Mock<IRoleStore<IdentityRole>> roleStore = new Mock<IRoleStore<IdentityRole>>();
        private Mock<IEmailConfiguration> _emailConf = new Mock<IEmailConfiguration>();
        private Mock<IComercial> _comRepo = new Mock<IComercial>();
        private Mock<IHostingEnvironment> _hostingEnvironment = new Mock<IHostingEnvironment>();
        private Mock<IBitacoraEstatusRepository> _beRepo = new Mock<IBitacoraEstatusRepository>();
        private Mock<ISettingsRepository> _setRepo = new Mock<ISettingsRepository>();
        private Mock<IOrdenPersona> _opRepo = new Mock<IOrdenPersona>();
        private Settings settings;
        private Mock<IArchivo> _archivoRepo = new Mock<IArchivo>();
        private Mock<IOrdenActivoFijo> _orAfRepo = new Mock<IOrdenActivoFijo>();
        private Mock<IOrdenInsumo> _orInsumoRepo = new Mock<IOrdenInsumo>();
        private Mock<IOsRecurrente> _osRecuRepo = new Mock<IOsRecurrente>();

        private Persona persona = new Persona()
        {
            Nombre = "Daniel Alejandro",
            Paterno = "Camacho",
            Materno = "Vazquez",
            Email = "daniel.alecv5@gmail.com"
        };
        private OrdenServicio osCreateOS = new OrdenServicio() {
            Folio = "OS0000093", Cliente = new Cliente(), Contrato = new Contrato(), Ubicacion = new Ubicacion(),
            LineaNegocio = new LineaNegocio(), Servicio = new Servicio(), Tipo = "solicitado",
            EstatusServicio = "estatusServicio", Observaciones = "observaciones", ContactoNombre = "contactoNombre",
            ContactoAP = "Ap", ContactoAM = "Am", ContactoEmail = "mail", ContactoTelefono = "55448844",
            NombreCompletoCC1 = "completoCC1", NombreCompletoCC2 = "completoCC2", EmailCC1 = "mailcc1", EmailCC2 = "mailcc2",
            Opcional1 = "op1", Opcional2 = "op2", Opcional3 = "op3", Opcional4 = "op4", Estatus = true,
            Insumos = new List<OrdenInsumo>(), Personal = new List<OrdenPersona>(), Activos = new List<OrdenActivoFijo>(),
            PersonaComercial = new Persona(), PersonaValida = new Persona(), FechaAdministrativa = new DateTime()
        };
        private OrdenServicioDTO osDtoCreateOs = new OrdenServicioDTO()
        {
            OSRecurrentePeriodo = "semanal",
            fechasOSRecurrente = "[{\"inicio\":\"2022-06-20\",\"fin\":\"2022-06-22\"},{\"inicio\":\"2022-06-27\",\"fin\":\"2022-06-29\"}]",
        };


        [TestMethod]
        public void SuccessResponseCreateOSR()
        {
            //Arrange interfaces Mocks
            _osRepo.Setup(x => x.GetOSbyFolio("OS0000093")).Returns(new OrdenServicio() { Id = 98 });
            _osRepo.Setup(x => x.GetOSbyFolio("OS0000001")).Returns(new OrdenServicio() { Id = 99 });
            _setRepo.Setup(x => x.GetByVersion("DAMSA")).Returns(new Settings() { FolioPrefix = "OS" });

            // Arrange  --  Mock identity
            _userManager.Setup(x => x.CheckPasswordAsync(It.IsAny<RoatechIdentityUser>(), It.IsAny<string>())).ReturnsAsync(true);
            var _roleManager = new Mock<RoleManager<IdentityRole>>(roleStore.Object, null, null, null, null);

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
                ServicioController controller = new ServicioController(_csRepo.Object, _cRepo.Object, _sRepo.Object, _lnRepo.Object, 
                    _osRepo.Object, _uRepo.Object, _pRepo.Object, _iRepo.Object, _afRepo.Object, _clRepo.Object, _bRepo.Object, 
                    _operRepo.Object, _userManager.Object, _roleManager.Object, _emailConf.Object, _comRepo.Object, 
                    _hostingEnvironment.Object, _beRepo.Object, _setRepo.Object, _opRepo.Object, context, _archivoRepo.Object, 
                    _orAfRepo.Object, _orInsumoRepo.Object, _osRecuRepo.Object);

                var resultOSR = controller.CreateOSR(osCreateOS, osDtoCreateOs);

                Assert.AreEqual(2, resultOSR.Count);
            }
        }
    }
}