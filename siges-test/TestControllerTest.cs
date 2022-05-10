using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using siges.Controllers;

namespace sigesTests
{
    [TestClass]
    public class TestControllerTest
    {
        private TestController controller = new TestController();

        [TestMethod]
        public void pruebaEjemplo()
        {
            string Expected = "Prueba 1";
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                var res = controller.Index();

                var result = sw.ToString().Trim();
                Assert.AreEqual(Expected, result);
            }
        }
        [TestMethod]
        public void pruebaEjemplo3()
        {
            var res = controller.Index3(5, 5);
            Assert.AreEqual(10, res);
        }

        [TestMethod]
        public void pruebaEjemplo2()
        {
            var ar = controller.Index2() as ViewResult;
            Assert.AreEqual("Prueba 2", ar.ViewData["Message"]);
        }
    }
}
