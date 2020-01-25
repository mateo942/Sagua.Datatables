using Sagua.Table.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sagua.Table.Helpers
{
    public class Paging : IPaging
    {
        public string OrderBy { get; set; }
        public OrderDirection OrderDirection { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
        public int TotalItems { get; set; }

        public static Paging Default =>
            new Paging
            {
                Page = 0,
                Limit = 20,
                OrderDirection = OrderDirection.Asc
            };

        
    }
}
