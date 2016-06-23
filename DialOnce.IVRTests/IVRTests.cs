using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static DialOnce.IVR;

namespace DialOnce.Tests
{
    [TestClass()]
    public class IVRTests
    {
        private static string called = "+33643487995";
        private static string caller = "+33643487995";
        private static string clientId = "qpvao53b1x10z7u3906wvgzmvexuxwxj";
        private static string clientSecret = "56g5jvhlciv9e0l4izccjqkf54okh21jbn4d4yj7";

        [TestMethod()]
        public void GetTokenAndLogWithApplication()
        {
            Application app = new Application(clientId, clientSecret);
            IVR ivrFlow = new IVR(app, caller, called);

            ivrFlow.Init();
            ValidateToken(app.Token);
            LogType lt = new LogType(LogType.CALL_START);
            bool result = ivrFlow.Log(lt);

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void GetTokenAndisMobileWithApplication()
        {
            Application app = new Application(clientId, clientSecret);
            IVR ivrFlow = new IVR(app, caller, called);

            ivrFlow.Init();
            ValidateToken(app.Token);
            
            bool result = ivrFlow.IsMobile("fr");
            Assert.IsTrue(result);

            //
            bool resultNoCulture = ivrFlow.IsMobile();
            Assert.IsTrue(result);

        }

        [TestMethod()]
        public void GetTokenAndisEligibleWithApplication()
        {
            Application app = new Application(clientId, clientSecret);
            IVR ivrFlow = new IVR(app, caller, called);

            ivrFlow.Init();
            ValidateToken(app.Token);

            bool result = ivrFlow.IsEligible();
            Assert.IsTrue(result);

        }

        [TestMethod()]
        public void GetTokenAndServiceRequestWithApplication()
        {
            Application app = new Application(clientId, clientSecret);
            IVR ivrFlow = new IVR(app, caller, called);

            ivrFlow.Init();
            ValidateToken(app.Token);

            bool result = ivrFlow.ServiceRequest();
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void GetTokenWithInvalidArguments()
        {
            try
            {
                Application app = new Application(null, null);
                IVR ivrFlow = new IVR(app);

                ivrFlow.Init();
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
                IVR ivrFlow = new IVR(app);

                ivrFlow.Init();
                Assert.Fail("Should throw Exception");
            }
            catch (Exception e)
            {
                Assert.IsNotNull(e);
                Assert.AreSame(e.Message, "clientSecret must not be null or empty");
            }

        }

        private static void ValidateToken(TokenDescriptor tokenDescriptor)
        {
            Assert.IsFalse(String.IsNullOrEmpty(tokenDescriptor.Token));
            Assert.IsFalse(String.IsNullOrEmpty(tokenDescriptor.Scheme));
            Assert.IsNotNull(tokenDescriptor.Expires);
            Assert.IsTrue(DateTime.Now < tokenDescriptor.Expires);
        }
    }
}
