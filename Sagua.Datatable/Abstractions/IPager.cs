using System;
using System.Collections.Generic;
using System.Text;

namespace Sagua.Datatable.Abstractions
{
    public interface IPager
    {
        void FirstPage();
        void LastPage();
        void GoToPage(int index);

        int PageLimit { get; set; }
        int TotalItems { get; set; }

        int CurrentPage { get; set; }
        int TotalPages { get; set; }
    }
}
