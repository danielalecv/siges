using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using siges.Utilities;

namespace siges_test
{
    [TestClass]
    public class ToolsTest
    {
        // Function CountUntilFindSlash
        [TestMethod]
        public void WithRegularUrl()
        {
            int expect = 15;
            string UrlTest = "https://www.google.com";
            var result = Tools.CountUntilFindSlash(UrlTest);

            Assert.AreEqual(expect, result);
        }

        [TestMethod]
        public void IfNotHaveDiagonal()
        {
            string UrlTest = "https:www.google.com";
            var result = Tools.CountUntilFindSlash(UrlTest);

            Assert.AreEqual(null, result);
        }

        //Function GenerateToken
        [TestMethod]
        public void TokenLength()
        {
            int expect = 64;
            var result = Tools.GenerateToken().Length;
            Assert.AreEqual(expect, result);
        }
        [TestMethod]
        public void TokenContainsOnlyLowercase()
        {
            var token = Tools.GenerateToken();
            string expect = token.ToLower();
            Assert.AreEqual(expect, token);
        }
    }
}