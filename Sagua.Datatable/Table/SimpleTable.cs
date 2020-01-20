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
    public class SimpleTable<TModel> : ComponentBase, ITable<TModel>
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public IEnumerable<TModel> Items { get; set; }

        protected ISet<IColumn<TModel>> Columns { get; set; }


        public SimpleTable()
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

        protected async override Task OnParametersSetAsync()
        {
            await RenderAsync();
        }

        private void Update()
        {
            this.StateHasChanged();
        }

        public Task RenderAsync()
        {
            Update();

            return Task.CompletedTask;
        }


        public IEnumerable<TModel> GetData()
        {
            return Items.ToList();
        }
    }
}
