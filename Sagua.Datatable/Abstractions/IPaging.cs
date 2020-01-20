using System;
using System.Collections.Generic;
using System.Text;

namespace Sagua.Datatable.Abstractions
{
    public interface IPaging
    {
        int TotalItems { get; set; }

        int? Limit { get; set; }
        int? Page { get; set; }
    }
}
