using System;
using System.Collections.Generic;
using System.Linq;
using Solutions.OnlineSelling.Model;
using System.Data.Entity;

namespace Solutions.OnlineSelling.BusinessLogic
{
    public class CalendarEntity
    {
        public int GetCounts(bool? activeStatus = null)
        {
            try
            {
                using (var context = new Model.SolutionsOnlineSellingEntities())
                {
                    if (activeStatus.HasValue == true)
                    {
                        return context.TblCalendar.Where(s => s.IsActive == activeStatus.Value).Count();
                    }

                    return context.TblCalendar.Count();
                }
            }
            catch (Exception ex)
            {
                Helpers.Logger.LogException(ex, false);
                return 0;
            }
        }

        public List<Model.TblCalendar> GetCalendar(bool? activeStatus = null)
        {
            try
            {
                using (var context = new Model.SolutionsOnlineSellingEntities())
                {
                    if (activeStatus.HasValue == true)
                    {
                        return context.TblCalendar.Where(s => s.IsActive == activeStatus.Value).ToList();
                    }

                    return context.TblCalendar.ToList();
                }
            }
            catch (Exception ex)
            {
                Helpers.Logger.LogException(ex, false);
                return null;
            }
        }



        public Model.TblCalendar GetCalendar(int calendarId)
        {
            try
            {
                using (var context = new Model.SolutionsOnlineSellingEntities())
                {
                    return context.TblCalendar.Where(s => s.EventId == calendarId).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Helpers.Logger.LogException(ex, false);
                return null;
            }
        }

        public bool ManageCalendar(TblCalendar model)
        {
            try
            {
                using (var context = new Model.SolutionsOnlineSellingEntities())
                {
                    context.Entry(model).State = model.EventId == 0 ? EntityState.Added : EntityState.Modified;
                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Helpers.Logger.LogException(ex, true);
                return false;
            }
        }
        public bool RemoveCalendar(TblCalendar model)
        {
            try
            {
                using (var context = new SolutionsOnlineSellingEntities())
                {
                    context.Entry(model).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Helpers.Logger.LogException(ex, true);
                return false;
            }
        }

    }
}
