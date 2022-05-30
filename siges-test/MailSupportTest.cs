using Microsoft.VisualStudio.TestTools.UnitTesting;
using MimeKit;
using Moq;
using siges.Repository;
using siges.Utilities;

namespace siges_test
{
    [TestClass]
    public class MailSupportTest
    {
        private Mock<IEmailConfiguration> emailConf = new Mock<IEmailConfiguration>();

        [TestMethod]
        public void CreateContent()
        {
            var controller = new MailSupportRepository(emailConf.Object);
            var result = controller.CreateContent("prioridad", "name", "lastname", "email", "body");
            string expect = "<h2>Soporte Técnico</h2><br><p>Prioridad: prioridad</p><p>Nombre: name lastname</p><p>Usuario: email</p><br><p>Descripción del problema: body</p>";

            Assert.AreEqual(expect, result);
        }

        [TestMethod]
        public void CreateHeader()
        {
            emailConf.Setup(e => e.UserName).Returns("correosDotNet117@gmail.com");
            var controller = new MailSupportRepository(emailConf.Object);
            MimeMessage result = controller.CreateHeader("title");

            Assert.AreEqual("title", result.Subject);
        }
    }
}
