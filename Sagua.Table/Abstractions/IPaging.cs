using System;
using System.Collections.Generic;
using System.Text;

namespace Sagua.Table.Abstractions
{
    public enum OrderDirection
    {
        Asc,
        Desc
    }

    public interface IPaging
    {
        string OrderBy { get; set; }
        OrderDirection OrderDirection { get; set; }

        int Page { get; set; }
        int Limit { get; set; }

        int TotalItems { get; set; }
    }
}
