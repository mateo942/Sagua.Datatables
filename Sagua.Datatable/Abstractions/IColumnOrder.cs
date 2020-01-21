using System;
using System.Collections.Generic;
using System.Text;

namespace Sagua.Datatable.Abstractions
{
    public interface IColumnOrder
    {
        string Source { get; set; }
        bool IsSortable { get; set; }

        bool IsCurrentSort { get; }
        OrderDirection OrderDirection { get; }

        void SetActive(IOrdered ordered);
    }
}
