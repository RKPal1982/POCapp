using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutions.OnlineSelling.Helpers
{
    public class JSONManager
    {
        public static string GetJson(object result)
        {
            JsonSerializerSettings jsSettings = new JsonSerializerSettings();
            jsSettings.DateFormatString = "dd/MM/yyy";
            jsSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            string json = JsonConvert.SerializeObject(result, jsSettings);

            return json;
        }
    }
}
