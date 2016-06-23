using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DialOnce.IVR.Tests
{
    [TestClass()]
    public class ApplicationTests
    {
        private static void ValidateToken(TokenDescriptor tokenDescriptor) {
            Assert.IsFalse(String.IsNullOrEmpty(tokenDescriptor.Token));
            Assert.IsFalse(String.IsNullOrEmpty(tokenDescriptor.Scheme));
            Assert.IsNotNull(tokenDescriptor.Expires);
            Assert.IsTrue(DateTime.Now < tokenDescriptor.Expires);
        }

        [TestMethod()]
        public void GetTokenWithApplication()
        {
            Application app = new Application("qpvao53b1x10z7u3906wvgzmvexuxwxj", "56g5jvhlciv9e0l4izccjqkf54okh21jbn4d4yj7");
            TokenDescriptor firstToken = app.Token;
            ValidateToken(firstToken);

            // token should not change
            TokenDescriptor secondToken = new Application(app.Token).Token;
            ValidateToken(secondToken);
            Assert.AreSame(firstToken, secondToken);
        }
        

        [TestMethod()]
        public void GetTokenWithRandomParams()
        {
            try
            {
                Application app = new Application("test", "test");
                Assert.Fail("Should throw Exception");
            }
            catch(Exception e) {
                Assert.IsNotNull(e);
            }
         
        }

        [TestMethod()]
        public void GetTokenWithInvalidArguments()
        {
           
            try
            {
                Application app = new Application(null, null);
                Assert.Fail("Should throw Exception");
            }
            catch (Exception e)
            {
                Assert.IsNotNull(e);
                Assert.AreSame(e.Message, "clientId must not be null or empty");
            }

        }

        [TestMethod()]
        public void GetTokenWithInvalidClientSecret()
        {

            try
            {
                Application app = new Application("test", null);
                Assert.Fail("Should throw Exception");
            }
            catch (Exception e)
            {
                Assert.IsNotNull(e);
                Assert.AreSame(e.Message, "clientSecret must not be null or empty");
            }

        }

    }
}