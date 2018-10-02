using Solutions.OnlineSelling.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Solutions.OnlineSelling.Web.Controllers
{
    public class PagingEntity
    {
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int CurrentPage { get; set; }
    }


    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Write you code logic that executes before action
        }
    }

    public class ProductsController : BaseController
    {
        // GET: Products
        public ActionResult Index()
        {
            var model = GetProducts(null);
            return View(model);
        }

        public ActionResult GetPaging(int CurrentPage, int PageSize, int TotalRecords)
        {
            PagingEntity model = new PagingEntity();
            int TotalPages = 0;
            if (PageSize != 0)
            {
                var remainder = TotalRecords % PageSize;
                TotalPages = (TotalRecords / PageSize) + (remainder == 0 ? 0 : 1);
            }
            model.TotalPages = TotalPages;
            model.PageSize = PageSize;
            model.TotalRecords = TotalRecords;
            model.CurrentPage = CurrentPage;
            return PartialView("_PagingPartial", model);
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Index(FormCollection collection = null)
        {
            var model = GetProducts(collection);
            return View(model);
        }

        private List<Model.TblProduct> GetProducts(FormCollection collection = null)
        {
            int PageNumber = 1;
            if (collection != null && collection["PageNumber"] != null)
            {
                int.TryParse(collection["PageNumber"].ToString(), out PageNumber);                
            }

            var _Filter = new Model.FilterProductLogs();
            _Filter.PageNumber = PageNumber;
            _Filter.PageSize = 2;
            var model = new BusinessLogic.ProductEntity().GetRecords(_Filter);
            @ViewBag.CurrentPageNumber = _Filter.PageNumber;
            @ViewBag.TotalRecords = model.Count();
            @ViewBag.PageSize = _Filter.PageSize;
            return model;
        }
    }
}