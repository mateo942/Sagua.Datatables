using Microsoft.AspNetCore.Components;
using Sagua.Datatable.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sagua.Datatable.Table
{
    public class SimplePager<TModel> : ComponentBase, IPager
    {
        [CascadingParameter(Name = "PagingTable")]
        public  ITablePaging TablePaging { get; set; }

        public int PageLimit { get; set; }
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }



        protected override void OnParametersSet()
        {
            TablePaging.SetPager(this);

            base.OnParametersSet();
        }

        public void FirstPage()
        {
            GoToPage(1);
        }

        public void GoToPage(int index)
        {
            if (index < 1)
            {
                index = 1;
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
    }
}
