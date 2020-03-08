using System;
using System.Collections.Generic;
using System.Text;

namespace Sagua.Table.Abstractions
{
    public interface ITablePlaceholder
    {
        void Configure(int columns, int rows);
        void ConfigureRows(int rows);

        void ShowPlaceholder();
        void HidePlaceholder();
    }
}
