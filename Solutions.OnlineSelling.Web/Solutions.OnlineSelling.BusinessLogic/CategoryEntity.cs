using System;
using System.Collections.Generic;
using System.Linq;

namespace Solutions.OnlineSelling.BusinessLogic
{
    public class CategoryEntity
    {
        public int GetCounts(bool? activeStatus = null)
        {
            try
            {
                using (var context = new Model.SolutionsOnlineSellingEntities())
                {
                    if (activeStatus.HasValue == true)
                    {
                        return context.TblCategory.Where(s => s.Active == activeStatus.Value).Count();
                    }

                    return context.TblCategory.Count();
                }
            }
            catch (Exception ex)
            {
                Helpers.Logger.LogException(ex, false);
                return 0;
            }
        }
    }
}
