using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Solutions.OnlineSelling.Web.Models
{
    public class ProductImageModel
    {
        public int ProductId { get; set; }
        public int ImageId { get; set; }
        public int DisplayOrder { get; set; }
        public string Description { get; set; }
        public string Thumbnail { get; set; }
        public string FullSize { get; set; }
        public string Extension { get; set; }
        public bool Selected { get; set; }
    }
}