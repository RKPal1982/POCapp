using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutions.OnlineSelling.Model
{
    public class FilterProductLogs
    {
        public int _ProductId = 0;
        public DateTime? _StartDate = null;
        public DateTime? _EndDate = null;
        private string _OrderBy = "Price";
        private string _OrderDir = "ASC";
        private int _PageNumber = 0;
        private int _PageSize = 0;

        public int ProductId
        {
            get
            {
                return _ProductId;
            }
            set
            {
                _ProductId = value;
            }
        }
        public DateTime? StartDate
        {
            get
            {
                return _StartDate;
            }
            set
            {
                _StartDate = value;
            }
        }
        public DateTime? EndDate
        {
            get
            {
                return _EndDate;
            }
            set
            {
                _EndDate = value;
            }
        }

        public string OrderBy
        {
            get
            {
                return _OrderBy;
            }
            set
            {
                _OrderBy = value;
            }
        }

        public string OrderDir
        {
            get
            {
                return _OrderDir;
            }
            set
            {
                _OrderDir = value;
            }
        }

        public int PageNumber
        {
            get
            {
                return _PageNumber;
            }
            set
            {
                _PageNumber = value;
            }
        }

        public int PageSize
        {
            get
            {
                return _PageSize;
            }
            set
            {
                _PageSize = value;
            }
        }
    }

}
