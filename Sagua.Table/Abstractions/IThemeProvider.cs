using System;
using System.Collections.Generic;
using System.Text;

namespace Sagua.Table.Abstractions
{
    public interface IThemeProvider
    {
        ITableTemplate GetTemplate(string name);
    }
}
