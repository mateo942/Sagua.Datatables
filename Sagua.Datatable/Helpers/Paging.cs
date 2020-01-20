using Sagua.Datatable.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sagua.Datatable.Helpers
{
    public class Paging : IPaging
    {
        public int? Limit { get; set; }
        public int? Page { get; set; }
        public OrderDirection OrderDirection { get; set; }
        public string OrderBy { get; set; }

        public static Paging Default
            => new Paging
            {
                Limit = 20,
                Page = 1,
                OrderDirection = OrderDirection.Asc,
                OrderBy = null
            };
    }
}
