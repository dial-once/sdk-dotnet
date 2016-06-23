using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DialOnce
{
    class IVR
    {
        private Application app { get; set; }
        private string caller { get; set; }
        private string called { get; set; }

        public IVR(Application app, string caller, string called) {
            this.app = app;
            this.caller = caller;
            this.called = called;

        }
    }
}
