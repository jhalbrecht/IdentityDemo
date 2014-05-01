using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Demo.Pcl
{
    public class Configuration
    {
        public Configuration()
        {
            _BaseAddress = "http://localhost:8782/";
        }

        private string _BaseAddress;
        public string BaseAddress
        {
            get { return _BaseAddress; }
            set { _BaseAddress = value; }
        }
    }
}
