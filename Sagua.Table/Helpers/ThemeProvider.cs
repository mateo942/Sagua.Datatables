using Sagua.Table.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sagua.Table.Helpers
{
    public class ThemeProvider : IThemeProvider
    {
        public ITableTemplate GetTemplate(string name)
        {
            return new BootstrapTemplate();
        }
    }
}
