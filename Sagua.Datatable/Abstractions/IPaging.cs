using System;
using System.Collections.Generic;
using System.Text;

namespace Sagua.Datatable.Abstractions
{
    public enum OrderDirection
    {
        Asc,
        Desc
    }

    public interface IPaging
    {
        int? Limit { get; set; }
        int? Page { get; set; }
        OrderDirection OrderDirection { get; set; }
        string OrderBy { get; set; }
    }
}
