using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Sagua.Datatable.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sagua.Datatable.Table
{
    public class SimplePager : ComponentBase, IPager
    {
        [Inject]
        protected ILogger<SimplePager> _logger { get; set; }

        [CascadingParameter(Name = "PagingTable")]
        public ITablePaging TablePaging { get; set; }

        public int PageLimit { get; set; }
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }


        protected override void OnInitialized()
        {
            _logger.LogDebug("Inicialize pager");

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

            _logger.LogDebug("Go to page: {0}", index);

            var paging = TablePaging.GetPaging();
            paging.Page = index;
            TablePaging.SetPaging(paging, true);
        }

        public void LastPage()
        {
            GoToPage(TotalPages);
        }

        public void UpdatePager(IPaging paging)
        {
            _logger.LogDebug("Update pager, {0}", System.Text.Json.JsonSerializer.Serialize(paging));

            PageLimit = paging.Limit ?? 20;
            CurrentPage = paging.Page ?? 0;
            TotalItems = paging.TotalItems;

            TotalPages = (int)Math.Ceiling((decimal)TotalItems / (decimal)PageLimit);

            this.StateHasChanged();
        }

        public void Refresh()
        {
            _logger.LogDebug("Refresh. Page: {0}, Limit: {1}", CurrentPage, PageLimit);
            this.StateHasChanged();
        }
    }
}
