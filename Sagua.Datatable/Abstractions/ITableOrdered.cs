using System;
using System.Collections.Generic;
using System.Text;

namespace Sagua.Datatable.Abstractions
{
    public interface ITableOrdered
    {
        IOrdered GetOrdered();
        void SetOrdered(IOrdered ordered);

        void AddColumOrder(IColumnOrder columnOrder);
        void RemoveColumOrder(IColumnOrder columnOrder);

        void UpdateOrder();
    }
}
