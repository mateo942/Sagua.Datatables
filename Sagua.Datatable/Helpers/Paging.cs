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

        public static Paging Default
            => new Paging
            {
                Limit = 10,
                Page = 0
            };

        public int TotalItems { get; set; }
    }
}
