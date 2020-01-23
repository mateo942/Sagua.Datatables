using System;
using System.Collections.Generic;
using System.Text;

namespace Sagua.Datatable.Abstractions
{
    public interface IDataSourcePaging
    {
        string OrderBy { get; set; }
        OrderDirection OrderDirection { get; set; }

        int? Limit { get; set; }
        int? Page { get; set; }
    }
}
