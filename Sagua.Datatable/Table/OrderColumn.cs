using Microsoft.AspNetCore.Components;
using Sagua.Datatable.Abstractions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Sagua.Datatable.Table
{
    public class OrderColumn<TModel> : ComponentBase, IColumn<TModel>, IColumnOrder, IDisposable
    {
        [CascadingParameter(Name = "Table")]
        public ITable<TModel> Table { get; set; }

        [CascadingParameter(Name = "TableOrdered")]
        public ITableOrdered TableOrdered { get; set; }

        [Parameter]
        public string Title { get; set; }
        [Parameter]
        public string Format { get; set; }

        [Parameter]
        public string Source { get; set; }

        [Parameter]
        public bool IsSortable { get; set; }

        [Parameter]
        public Func<TModel, object> Field { get; set; }

        protected bool _isCurrentSortable;
        protected OrderDirection _orderDirection;

        public bool IsCurrentSort 
            => _isCurrentSortable;

        public OrderDirection OrderDirection 
            => _orderDirection;

        protected override void OnInitialized()
        {
            Table.AddColumn(this);
            TableOrdered.AddColumOrder(this);

            base.OnInitialized();
        }

        public void Dispose()
        {
            Table.RemoveColumn(this);
            TableOrdered.RemoveColumOrder(this);
        }

        public string Render(TModel item)
        {
            if (Field == null)
                return string.Empty;

            if (item == null)
                return string.Empty;

            var value = Field?.Invoke(item);
            if (value == null)
                return string.Empty;

            if (string.IsNullOrEmpty(Format))
                return value.ToString();

            return string.Format(CultureInfo.CurrentCulture, $"{{0:{Format}}}", value);
        }

        public void SetActive(IOrdered ordered)
        {
            if(ordered.CurrentOrderedColum == this)
            {
                _isCurrentSortable = true;
            } else
            {
                _isCurrentSortable = false;
            }

            _orderDirection = ordered.OrderDirection;
        }
    }
}
