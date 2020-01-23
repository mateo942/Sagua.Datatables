﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Sagua.Datatable.Abstractions
{
    public interface ITablePaging
    {
        IPaging GetPaging();
        void SetPaging(IPaging paging, bool reload);

        void SetPager(IPager pager);
        void UpdatePager();
    }
}
