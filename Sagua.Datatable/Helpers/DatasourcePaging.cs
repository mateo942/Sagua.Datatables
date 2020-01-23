using Sagua.Datatable.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sagua.Datatable.Helpers
{
    public class DatasourcePaging : IDataSourcePaging
    {
        public string OrderBy { get; set; }
        public OrderDirection OrderDirection { get; set; }
        public int? Limit { get; set; }
        public int? Page { get; set; }

        public DatasourcePaging(IPaging paging, IOrdered ordered)
        {
            OrderBy = ordered.OrderBy;
            OrderDirection = ordered.OrderDirection;

            Limit = paging.Limit;
            Page = paging.Page;
        }
    }
}
