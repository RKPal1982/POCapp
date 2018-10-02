using System;
using System.Collections.Generic;
using System.Linq;

namespace Solutions.OnlineSelling.BusinessLogic
{
    public interface IPagedList
    {
        int TotalCount { get; }
        int PageCount { get; }
        int Page { get; }
        int PageSize { get; }
    }
    public class PagedList<T> : List<T>, IPagedList
    {
        public int TotalCount { get; private set; }
        public int PageCount { get; private set; }
        public int Page { get; private set; }
        public int PageSize { get; private set; }

        public PagedList(IQueryable<T> source, int page, int pageSize)
        {
            TotalCount = source.Count();
            PageCount = GetPageCount(pageSize, TotalCount);
            Page = page < 1 ? 0 : page - 1;
            PageSize = pageSize;
            if (pageSize == 0 && Page == 0)
            {
                AddRange(source.ToList());
            }
            else
            {
                AddRange(source.Skip(Page * PageSize).Take(PageSize).ToList());
            }
        }

        private int GetPageCount(int pageSize, int totalCount)
        {
            if (pageSize == 0)
                return 0;

            var remainder = totalCount % pageSize;
            return (totalCount / pageSize) + (remainder == 0 ? 0 : 1);
        }
    }
    public static class PagedListExtensions
    {
        public static PagedList<T> ToPagedList<T>(this IQueryable<T> source, int page, int pageSize)
        {
            return new PagedList<T>(source, page, pageSize);
        }
    }
    public class ProductEntity
    {
        public int GetCounts(bool? activeStatus = null)
        {
            try
            {
                using (var context = new Model.SolutionsOnlineSellingEntities())
                {
                    if (activeStatus.HasValue == true)
                    {
                        return context.TblProduct.Where(s => s.Active == activeStatus.Value).Count();
                    }

                    return context.TblProduct.Count();
                }
            }
            catch
            {
                return 0;
            }
        }

        public List<Model.TblProduct> GetRecords(bool? activeStatus = null)
        {
            try
            {
                using (var context = new Model.SolutionsOnlineSellingEntities())
                {
                    if (activeStatus.HasValue == true)
                    {
                        return context.TblProduct.Where(s => s.Active == activeStatus.Value).ToList();
                    }

                    return context.TblProduct.ToList();
                }
            }
            catch
            {
                return new List<Model.TblProduct>();
            }
        }

        public List<Model.TblProduct> GetRecords(Model.FilterProductLogs filters)
        {
            try
            {
                using (var context = new Model.SolutionsOnlineSellingEntities())
                {
                    return context.TblProduct.ToPagedList(filters.PageNumber, filters.PageSize); ;
                }
            }
            catch
            {
                return new List<Model.TblProduct>();
            }
        }



    }
}
