using System;
using System.Collections.Generic;
using System.Text;

namespace Sagua.Table.Abstractions
{
    public interface ITable
    {
        string Name { get; set; }

        void AddColumn(ITableColumn column);
        void RemoveColumn(ITableColumn column);

        IPager Pager { get; }
        void SetPager(IPager pager);
        bool UsePager { get; set; }
        int Limit { get; set; }

        IPaging Paging { get; }
        void UpdatePaging(Action<IPaging> func);

        void SetPlaceholder(ITablePlaceholder tablePlaceholder);

        void LoadData();
    }

    public interface ITable<TModel> : ITable
    {
        IEnumerable<TModel> Items { get; set; }
    }
}
