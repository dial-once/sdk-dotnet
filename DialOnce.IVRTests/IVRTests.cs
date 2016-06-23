using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static DialOnce.IVR;

namespace DialOnce.Tests
{
    [TestClass()]
    public class IVRTests
    {

        [TestMethod()]
        public void GetTokenAndLogWithApplication()
        {
            string called  = "+33643487995";
            string caller = "+33643487995";

            Application app = new Application("qpvao53b1x10z7u3906wvgzmvexuxwxj", "56g5jvhlciv9e0l4izccjqkf54okh21jbn4d4yj7");
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
            string called = "+33643487995";
            string caller = "+33643487995";

            Application app = new Application("qpvao53b1x10z7u3906wvgzmvexuxwxj", "56g5jvhlciv9e0l4izccjqkf54okh21jbn4d4yj7");
            IVR ivrFlow = new IVR(app, caller, called);

            ivrFlow.Init();
            ValidateToken(app.Token);
            
            bool result = ivrFlow.isMobile("fr");
            Assert.IsTrue(result);

        }

        [TestMethod()]
        public void GetTokenAndisEligibleWithApplication()
        {
            string called = "+33643487995";
            string caller = "+33643487995";

            Application app = new Application("qpvao53b1x10z7u3906wvgzmvexuxwxj", "56g5jvhlciv9e0l4izccjqkf54okh21jbn4d4yj7");
            IVR ivrFlow = new IVR(app, caller, called);

            ivrFlow.Init();
            ValidateToken(app.Token);

            bool result = ivrFlow.isEligible();
            Assert.IsTrue(result);

        }

        [TestMethod()]
        public void GetTokenAndServiceRequestWithApplication()
        {
            string called = "+33643487995";
            string caller = "+33643487995";

            Application app = new Application("qpvao53b1x10z7u3906wvgzmvexuxwxj", "56g5jvhlciv9e0l4izccjqkf54okh21jbn4d4yj7");
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
