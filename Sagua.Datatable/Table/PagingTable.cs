using Microsoft.AspNetCore.Components;
using Sagua.Datatable.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagua.Datatable.Table
{
    public class PagingTable<TModel> : ComponentBase, ITable<TModel>, ITablePaging
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public IEnumerable<TModel> Items { get; set; }

        protected ISet<IColumn<TModel>> Columns { get; set; }

        private IPaging _paging;
        private IPager _pager;

        public PagingTable()
        {
            Columns = new HashSet<IColumn<TModel>>();
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

        public IEnumerable<TModel> GetData()
        {
            var query = Items.AsQueryable();

            var paging = GetPaging();
            paging.TotalItems = query.Count();

            if (paging.Limit.HasValue == false)
                paging.Limit = 20;

            if (paging.Page.HasValue == false)
                paging.Page = 0;

            query = query.Skip(paging.Limit.Value * paging.Page.Value).Take(paging.Limit.Value);

            return query.ToList();
        }

        public IPaging GetPaging()
        {
            if(_paging == null)
            {
                _paging = Helpers.Paging.Default;
            }

            return _paging;
        }

        public void SetPaging(IPaging paging, bool reload)
        {
            _paging = paging;

            Update();
        }

        public void SetPager(IPager pager)
        {
            _pager = pager;

            Update();
        }

        public void UpdatePager()
        {
            if(_pager != null)
            {
                var paging = GetPaging();

                _pager.UpdatePager(paging);
            }
        }


        private void Update()
        {
            UpdatePager();
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
    }
}
