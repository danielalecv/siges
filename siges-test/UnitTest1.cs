using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using siges.Controllers;
using System;
using System.IO;

namespace siges_test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var mock = new Mock<ILogger<HomeController>>();
            ILogger<HomeController> logger = mock.Object;

            var controller = new HomeController(logger);

            string Expected = "Home page";
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                var res = controller.Index();

                var result = sw.ToString().Trim();
                Assert.AreEqual(Expected, result);
            }
        }

        [TestMethod]
        public void TestMethod2()
        {
            var mock = new Mock<ILogger<HomeController>>();
            ILogger<HomeController> logger = mock.Object;

            var controller = new HomeController(logger);
            var ar = controller.Privacy() as ViewResult;
            Assert.AreEqual("Privacy page", ar.ViewData["Message"]);
        }
    }
}
