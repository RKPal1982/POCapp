using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace Solutions.OnlineSelling.BusinessLogic
{
    public class EntitySQL<T> where T: class
    {
        public  string ErrorMessage { get; set; }

        public List<T> SelectQuery(string columnArray, string tableName, string whereClause = null, string orderClause = null)
        {
            try
            {
                using (var context = new Model.SolutionsOnlineSellingEntities())
                {
                    if (string.IsNullOrEmpty(columnArray) == true) columnArray = "*";

                    //Querying with Object Services and Entity SQL
                    string sqlString = string.Format("SELECT {0} FROM SolutionsOnlineSellingEntities.{1} ", columnArray, tableName);                                        

                    if(string.IsNullOrEmpty(whereClause) == false)  sqlString = string.Format("{0} WHERE {1}",  sqlString, whereClause);

                    if (string.IsNullOrEmpty(orderClause) == false)  sqlString = string.Format("{0} ORDER BY {1}",  sqlString, orderClause);
                   


                    var objctx = (context as IObjectContextAdapter).ObjectContext;

                    ObjectQuery<T> queryList = objctx.CreateQuery<T>(sqlString);
                    var requiredList = queryList.ToList<T>();

                    return requiredList;
                }
            }
            catch(Exception ex)
            {
                ErrorMessage = ex.Message;
                return new List<T>();
            }
        }

        public List<T> SelectQuery(string query)
        {
            try
            {
                using (var context = new Model.SolutionsOnlineSellingEntities())
                {
                    var requiredList = context.Database.SqlQuery<T>(query).ToList<T>();
                    return requiredList;
                }
            }
            catch(Exception ex)
            {
                ErrorMessage = ex.Message;
                return new List<T>();
            }
        }
    }
}
