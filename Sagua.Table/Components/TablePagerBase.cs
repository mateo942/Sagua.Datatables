using Microsoft.AspNetCore.Components;
using Sagua.Table.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sagua.Table.Components
{
    public class TablePagerBase : ComponentBase, IPager
    {
        [CascadingParameter(Name = "Table")]
        public ITable Table { get; set; }

        public int Limit { get; set; }
        public int Page { get; set; }
        public int TotalItems { get; set; }

        protected int TotalPages
            => Limit <= 0 ? 1 : (TotalItems + Limit - 1) / Limit;

        protected override void OnInitialized()
        {
            Table.SetPager(this);
        }

        public void GoToPage(int index)
        {
            if (index < 0)
            {
                index = 0;
            }
            else if (index > TotalPages)
            {
                index = TotalPages;
            }

            Page = index;
            UpdateTable();
        }

        private void UpdateTable()
        {
            Table.UpdatePaging(x =>
            {
                x.Page = Page;
                x.Limit = Limit;
            });
        }

        public void Update(IPaging paging)
        {
            Limit = paging.Limit;
            Page = paging.Page;
            TotalItems = paging.TotalItems;

            this.StateHasChanged();
        }

        public void SetLimit(int limit)
        {
            Limit = limit;
            UpdateTable();
        }
    }
}
