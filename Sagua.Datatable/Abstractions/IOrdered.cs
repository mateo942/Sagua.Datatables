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

    public interface IOrdered
    {
        string OrderBy { get; set; }
        OrderDirection OrderDirection { get; set; }

        IColumnOrder CurrentOrderedColum { get; set; }
    }
}
