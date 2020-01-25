using Microsoft.AspNetCore.Components;
using Sagua.Table.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sagua.Table.Helpers
{
    public class BootstrapTemplate : ITableTemplate
    {
        public string TableClass
            => "table table-hover table-sm";
        public string TableHeadClass
            => "thead-dark";
        public string TableBodyClass
            => string.Empty;
        public string TableRowClass
            => string.Empty;

        public Func<object, string> DynamicTableRowClass
            => x => string.Empty;
    }
}
