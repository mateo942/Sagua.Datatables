using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Sagua.Table.Abstractions
{
    public interface ITableColumn : IDisposable
    {
        ITable Table { get; set; }

        string Title { get; set; }
        string Source { get; set; }
        string Format { get; set; }

        void Update();
        string Render(object source);
    }

    public interface ITableColumn<TModel> : ITableColumn
    {
        Expression<Func<TModel, object>> Field { get; set; }
    }

    public interface ITableOrderColumn : ITableColumn
    {
        bool IsSortable { get; set; }

        void Update(IPaging paging);
        void SetSelfOrder();
    }
}
