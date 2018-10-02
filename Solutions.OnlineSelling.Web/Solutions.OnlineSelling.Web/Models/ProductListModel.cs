using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Solutions.OnlineSelling.Web.Models
{
    public class ProductListModel
    {
        public List<AttributeModel> AttributeList { get; set; }
        public List<ProductInfoModel> ProductList { get; set; }
        public List<ProductImageModel> ImageList { get; set; }
    }
}