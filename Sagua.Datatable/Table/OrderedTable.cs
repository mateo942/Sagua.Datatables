using Microsoft.AspNetCore.Components;
using Sagua.Datatable.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Sagua.Datatable.Table
{
    public class OrderedTable<TModel> : ComponentBase, ITable<TModel>, ITableOrdered
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public IEnumerable<TModel> Items { get; set; }

        protected ISet<IColumn<TModel>> Columns { get; set; }
        protected ISet<IColumnOrder> OrderedColumns { get; set; }

        private IOrdered _ordered;

        public OrderedTable()
        {
            Columns = new HashSet<IColumn<TModel>>();
            OrderedColumns = new HashSet<IColumnOrder>();
        }

        public void AddColumn(IColumn<TModel> column)
        {
            Columns.Add(column);
            Update();
        }

        public void RemoveColumn(IColumn<TModel> column)
        {
            Columns.Remove(column);
            Update();
        }

        public void AddColumOrder(IColumnOrder columnOrder)
        {
            OrderedColumns.Add(columnOrder);
            Update();
        }

        public void RemoveColumOrder(IColumnOrder columnOrder)
        {
            OrderedColumns.Remove(columnOrder);
            Update();
        }

        public IEnumerable<TModel> GetData()
        {
            var query = Items.AsQueryable();

            var ordered = GetOrdered();

            if (!string.IsNullOrEmpty(ordered.OrderBy))
            {
                if (ordered.OrderDirection == OrderDirection.Asc)
                {
                    query = query.OrderBy(x => x.GetType().GetProperty(ordered.OrderBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).GetValue(x));
                } else
                {
                    query = query.OrderByDescending(x => x.GetType().GetProperty(ordered.OrderBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).GetValue(x));
                }
            }

            return query.ToList();
        }

        public IOrdered GetOrdered()
        {
            if(_ordered == null)
            {
                _ordered = Helpers.Ordered.Default(OrderedColumns.FirstOrDefault());
            }

            return _ordered;
        }

        public void SetOrdered(IOrdered ordered)
        {
            _ordered = ordered;

            Update();
        }

        public void UpdateOrder()
        {
            var orderd = GetOrdered();
            foreach (var column in OrderedColumns)
            {
                column.SetActive(orderd);
            }
        }

        private void Update()
        {
            UpdateOrder();
            this.StateHasChanged();
        }

        public Task RenderAsync()
        {
            Update();
            return Task.CompletedTask;
        }

        protected async override Task OnParametersSetAsync()
        {
            await RenderAsync();
        }

        private bool IsSortable(IColumn<TModel> column)
        {
            if(column is IColumnOrder orderedColumn){
                return orderedColumn.IsSortable && OrderedColumns.Contains(orderedColumn);
            }
            return false;
        }

        public string RenderSort(IColumn<TModel> column)
        {
            if (IsSortable(column))
            {
                IColumnOrder columnOrder = column as IColumnOrder;

                if (columnOrder.IsCurrentSort)
                {
                    if(columnOrder.OrderDirection == OrderDirection.Asc)
                    {
                        return "A";
                    }
                    else
                    {
                        return "D";
                    }
                } else
                {
                    return "N";
                }
            }
            else
            {
                return string.Empty;
            }
        }

        public void SetOrder(IColumn<TModel> column)
        {
            if (IsSortable(column))
            {
                IColumnOrder columnOrder = column as IColumnOrder;

                var order = GetOrdered();
                order.OrderBy = columnOrder.Source;
                if(order.OrderDirection == OrderDirection.Asc)
                {
                    order.OrderDirection = OrderDirection.Desc;
                } else
                {
                    order.OrderDirection = OrderDirection.Asc;
                }
                order.CurrentOrderedColum = columnOrder;
                SetOrdered(order);
            }
        }
    }
}
