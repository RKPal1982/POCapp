using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Solutions.OnlineSelling.Web.Models
{
    public class ProductInfoModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string CreatedOn { get; set; }
       
    }
}