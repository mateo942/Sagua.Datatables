using Microsoft.AspNetCore.Components;
using Sagua.Datatable.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sagua.Datatable.Table
{
    public class SimplePager : ComponentBase, IPager
    {
        [CascadingParameter(Name = "PagingTable")]
        public ITablePaging TablePaging { get; set; }

        public int PageLimit { get; set; }
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages
            => (int)Math.Ceiling((decimal)TotalItems / (decimal)PageLimit);


        protected override void OnInitialized()
        {
            TablePaging.SetPager(this);

            base.OnInitialized();
        }

        public void FirstPage()
        {
            GoToPage(1);
        }

        public void GoToPage(int index)
        {
            if (index < 0)
            {
                index = 0;
            } else if (index > TotalPages)
            {
                index = TotalPages;
            }

            var paging = TablePaging.GetPaging();
            paging.Page = index;
            TablePaging.SetPaging(paging);
        }

        public void LastPage()
        {
            GoToPage(TotalPages);
        }

        public void UpdatePager(IPaging paging)
        {
            PageLimit = paging.Limit ?? 20;
            CurrentPage = paging.Page ?? 0;
            TotalItems = paging.TotalItems;
        }
    }
}
