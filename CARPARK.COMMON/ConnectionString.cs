using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARPARK.COMMON
{
    public class ConnectionString
    {
        public string conString()
        {
            string connec = (@"data source=.;initial catalog=CARPARK;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");
            return connec;
        }
    }
}
