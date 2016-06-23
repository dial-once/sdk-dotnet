using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DialOnce.Tests
{
    [TestClass()]
    public class ApplicationTests
    {   

        [TestMethod()]
        public void GetApplicationWithRandomParams()
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

    }
}