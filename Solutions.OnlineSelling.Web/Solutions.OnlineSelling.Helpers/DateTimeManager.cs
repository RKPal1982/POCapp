using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutions.OnlineSelling.Helpers
{
    public class DateTimeManager
    {
        public static string GetString(DateTime createdOn, string pattern = "dd MM, yyyy")
        {
            return createdOn.ToString(pattern);
        }

        public static string GetString(DateTime createdOn)
        {
            string pattern = "dd MM, yyyy HH:mm";
            return createdOn.ToString(pattern);
        }
    }
}
