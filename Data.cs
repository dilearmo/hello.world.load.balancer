using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hello.world.load.balancer
{
    public class Data
    {
        public string server_who_loaded_page { get; set; }
        public string server_who_saved_submission { get; set; }
        public string color { get; set; }
        public string client { get; set; }
        public DateTime datetime { get; set; }
    }
}
