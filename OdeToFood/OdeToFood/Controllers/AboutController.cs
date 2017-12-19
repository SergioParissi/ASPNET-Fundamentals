using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.Controllers
{
    public class AboutController
    {
        public DateTime Phone()
        {
            DateTime time = DateTime.Now.AddDays(-90);

            return time;
        }

        public string Address()
        {
            return "México";
        }
    }
}
