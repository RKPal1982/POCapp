using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Solutions.OnlineSelling.Web.Models
{
    public class PropertyModel
    {
        public int AttributeId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Order { get; set; }

        public bool Selected { get; set; }
    }
}