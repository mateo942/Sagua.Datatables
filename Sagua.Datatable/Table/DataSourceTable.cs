using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Sagua.Datatable.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagua.Datatable.Table
{
    public class DataSourceTable<TModel> : ComponentBase, ITable<TModel>, ITableOrdered, ITablePaging, IDataSourceTable<TModel>
    {
        [Inject]
        protected ILogger<DataSourceTable<TModel>> _logger { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public IDataSource<TModel> DataSource { get; set; }

        public IEnumerable<TModel> Items { get; set; } = Enumerable.Empty<TModel>();

        protected ISet<IColumn<TModel>> Columns { get; set; }
        protected ISet<IColumnOrder> OrderedColumns { get; set; }

        private IOrdered _ordered;
        private IPaging _paging;
        private IPager _pager;

        public DataSourceTable()
        {
            Columns = new HashSet<IColumn<TModel>>();
            OrderedColumns = new HashSet<IColumnOrder>();
        }

        #region Columns
        public void AddColumn(IColumn<TModel> column)
        {
            _logger.LogDebug("Add column: {0}", column.Title);
            Columns.Add(column);
            this.StateHasChanged();
        }

        public void AddColumOrder(IColumnOrder columnOrder)
        {
            _logger.LogDebug("Add order column: {0}", columnOrder.Source);
            OrderedColumns.Add(columnOrder);
        }

        public void RemoveColumn(IColumn<TModel> column)
        {
            _logger.LogDebug("Remove column: {0}", column.Title);
            Columns.Remove(column);
            this.StateHasChanged();
        }

        public void RemoveColumOrder(IColumnOrder columnOrder)
        {
            _logger.LogDebug("Remove order column: {0}", columnOrder.Source);
            OrderedColumns.Remove(columnOrder);
        }
        #endregion

        #region Data
        public IEnumerable<TModel> GetData()
        {
            _logger.LogDebug("Downloading data...");
            var dataSource = GetDataSource();
            var paging = GetPaging();
            var ordered = GetOrdered();

            if (dataSource == null)
            {
                paging.TotalItems = 0;
                //TODO: Loading placeholder
                Items = Enumerable.Empty<TModel>();
            } else
            {
                var items = dataSource.ExecuteAsync(new Helpers.DatasourcePaging(paging, ordered)).GetAwaiter().GetResult();
                paging.TotalItems = dataSource.Total;
                Items = items;

                SetPaging(paging, false);
                UpdatePager();

                _logger.LogDebug("Download completed");
            }
            
            return Items;
        }

        public IDataSource<TModel> GetDataSource()
        {
            return DataSource;
        }

        public void SetDataSource(IDataSource<TModel> dataSource)
        {
            _logger.LogDebug("Set source: {0}", dataSource.GetType().Name);
            DataSource = dataSource;
        }
        #endregion

        #region Sort
        public IOrdered GetOrdered()
        {
            if (_ordered == null)
            {
                _ordered = Helpers.Ordered.Default(OrderedColumns.FirstOrDefault());
            }

            return _ordered;
        }

        public void SetOrdered(IOrdered ordered)
        {
            _logger.LogDebug("Set ordered");

            _ordered = ordered;
            Refresh();
        }

        public void UpdateOrder()
        {
            _logger.LogDebug("Update order");
            var orderd = GetOrdered();
            foreach (var column in OrderedColumns)
            {
                column.SetActive(orderd);
            }
        }
        #endregion

        #region Paging
        public IPaging GetPaging()
        {
            if (_paging == null)
            {
                _paging = Helpers.Paging.Default;
            }

            return _paging;
        }

        public void SetPaging(IPaging paging, bool reload)
        {
            _logger.LogDebug("Set paging");
            _paging = paging;

            if (reload)
                Refresh();
        }

        public void SetPager(IPager pager)
        {
            _logger.LogDebug("Set pager");

            _pager = pager;

            UpdatePager();
        }

        public void UpdatePager()
        {
            if (_pager != null)
            {
                _logger.LogDebug("Update pager");
                var paging = GetPaging();

                _pager.UpdatePager(paging);
            }
        }
        #endregion

        public Task RenderAsync()
        {
            return Task.CompletedTask;
        }

        private bool IsSortable(IColumn<TModel> column)
        {
            if (column is IColumnOrder orderedColumn)
            {
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
                    if (columnOrder.OrderDirection == OrderDirection.Asc)
                    {
                        return "A";
                    }
                    else
                    {
                        return "D";
                    }
                }
                else
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
                if (order.OrderDirection == OrderDirection.Asc)
                {
                    order.OrderDirection = OrderDirection.Desc;
                }
                else
                {
                    order.OrderDirection = OrderDirection.Asc;
                }
                order.CurrentOrderedColum = columnOrder;
                SetOrdered(order);
            }
        }

        protected override Task OnInitializedAsync()
        {
            _logger.LogDebug("Init async");
            return base.OnInitializedAsync();
        }

        protected async override Task OnParametersSetAsync()
        {
            _logger.LogDebug("Parameters set async");

            await Task.Delay(500);
            Refresh();

            await base.OnParametersSetAsync();
        }

        public void Refresh()
        {
            _logger.LogDebug("Refresh");
            GetData();
            this.StateHasChanged();
        }
    }
}
