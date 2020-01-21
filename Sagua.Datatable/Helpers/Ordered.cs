using Sagua.Datatable.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sagua.Datatable.Helpers
{
    public class Ordered : IOrdered
    {
        public string OrderBy { get; set; }
        public OrderDirection OrderDirection { get; set; }

        public IColumnOrder CurrentOrderedColum { get; set; }

        public static Ordered Default(IColumnOrder columnOrder)
            => new Ordered
            {
                OrderBy = columnOrder?.Source,
                OrderDirection = OrderDirection.Asc,
                CurrentOrderedColum = columnOrder
            };
    }
}
