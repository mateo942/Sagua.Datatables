using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sagua.Table.Abstractions
{
    public interface ITableTemplate
    {
        string TableClass { get; }
        string TableHeadClass { get; }
        string TableBodyClass { get; }
        string TableRowClass { get; }
        Func<object, string> DynamicTableRowClass { get; }
        string TableButtonRowClass { get; }

        string SortIconNone { get; }
        string SortIconAsc { get; }
        string SortIconDsc { get; }
    }
}
