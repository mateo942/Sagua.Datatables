using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Sagua.Table.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Sagua.Table.Components
{
    public abstract class TableBase<TModel> : ComponentBase, ITable<TModel>
    {
        [Inject]
        protected ILogger<TableBase<TModel>> Logger { get; set; }

        [Parameter]
        public RenderFragment Columns { get; set; }
        [Parameter]
        public IEnumerable<TModel> Items { get; set; }
        [Parameter]
        public ITableDataSource<TModel> DataSource { get; set; }
        [Parameter]
        public bool UsePager { get; set; }
        [Parameter]
        public int Limit { get; set; }

        public IPager Pager { get; set; }
        public IPaging Paging { get; protected set; }

        public ITablePlaceholder TablePlaceholder { get; protected set; }

        protected ISet<ITableColumn> TableColumns { get; set; }
        protected IEnumerable<TModel> CacheItems { get; set; }

        private IEnumerable<ITableOrderColumn> GetOrderColumns
            => TableColumns.Where(x => typeof(ITableOrderColumn).IsAssignableFrom(x.GetType())).Cast<ITableOrderColumn>();


        public TableBase()
        {
            TableColumns = new HashSet<ITableColumn>();
            Paging = Helpers.Paging.Default;
        }

        protected override void OnParametersSet()
        {
            Paging.Limit = Limit;

            LoadData();
        }

        public async void LoadData()
        {
            TablePlaceholder?.ShowPlaceholder();
            Logger.LogDebug("Clearing old data...");
            CacheItems = Enumerable.Empty<TModel>();
            this.StateHasChanged(); //Update only table
            Logger.LogDebug("Loading data...");
            if (DataSource != null)
            {
                Logger.LogDebug("Loading datasource...");
                CacheItems = await DataSource.ExecuteAsync(Paging);
                Paging.TotalItems = DataSource.TotalItems;
            }
            else if (Items != null)
            {
                Logger.LogDebug("Loading items...");
                CacheItems = Items.ToList();

                if (UsePager)
                {
                    Logger.LogDebug("Paging data...");

                    Paging.TotalItems = CacheItems.Count();

                    if (!string.IsNullOrEmpty(Paging.OrderBy))
                    {
                        if (Paging.OrderDirection == OrderDirection.Asc)
                        {
                            CacheItems = CacheItems.OrderBy(x => x.GetType().GetProperty(Paging.OrderBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).GetValue(x));
                        }
                        else
                        {
                            CacheItems = CacheItems.OrderByDescending(x => x.GetType().GetProperty(Paging.OrderBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).GetValue(x));
                        }
                    }

                    CacheItems = CacheItems.Skip(Paging.Limit * Paging.Page).Take(Paging.Limit);
                }
            }
            else
            {
                Logger.LogDebug("Not found actions to load data");
            }

            Logger.LogDebug("Data has been loaded. Total items: {0}", CacheItems.Count());

            Update();
            TablePlaceholder?.HidePlaceholder();
        }

        public void AddColumn(ITableColumn column)
        {
            Logger.LogDebug("Add columns: {0}", column.Source);
            TableColumns.Add(column);
            
            if(column is ITableOrderColumn tableOrderColumn)
            {
                tableOrderColumn.Update(Paging);
            }

            this.StateHasChanged();
        }

        public void RemoveColumn(ITableColumn column)
        {
            Logger.LogDebug("Remove columns: {0}", column.Source);
            TableColumns.Remove(column);
            this.StateHasChanged();
        }

        public void SetPager(IPager pager)
        {
            Logger.LogDebug("Set pager");

            Pager = pager;
            Pager.Update(Paging);
        }

        public void UpdatePaging(Action<IPaging> action)
        {
            action?.Invoke(Paging);
            Logger.LogDebug("Update paging. Page: {0}, Limit: {1}, OrderBy: {2}, OrderDirection:{3}",
                Paging.Page, Paging.Limit, Paging.OrderBy, Paging.OrderDirection);

            TablePlaceholder?.Configure(TableColumns.Count, Paging.Limit);
            LoadData();
        }

        private void Update()
        {
            Logger.LogDebug("Updating table...");
            this.StateHasChanged();
            Logger.LogDebug("Table has been updated");

            Logger.LogDebug("Updating columns...");
            foreach (var sortableColumn in GetOrderColumns)
            {
                sortableColumn.Update(Paging);
            }
            Logger.LogDebug("Columns has been updated");

            Logger.LogDebug("Updating pager...");
            Pager.Update(Paging);
            Logger.LogDebug("Pager has been updated");
        }

        public void SetPlaceholder(ITablePlaceholder tablePlaceholder)
        {
            Logger.LogDebug("Set placeholder");
            TablePlaceholder = tablePlaceholder;
        }
    }
}
