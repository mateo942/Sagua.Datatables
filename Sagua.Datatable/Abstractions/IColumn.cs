using System;
using System.Collections.Generic;
using System.Text;

namespace Sagua.Datatable.Abstractions
{
    public interface IColumn<TModel>
    {
        string Title { get; set; }
        string Source { get; set; }

        bool Sortable { get; set; }

        Func<TModel, object> Field { get; set; }
        string Format { get; set; }

        string Render(TModel item);
    }
}
