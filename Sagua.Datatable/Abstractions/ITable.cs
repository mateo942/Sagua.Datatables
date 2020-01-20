using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sagua.Datatable.Abstractions
{
    public interface ITable<TModel>
    {
        IEnumerable<TModel> Items { get; set; }
        IEnumerable<TModel> GetData();

        void AddColumn(IColumn<TModel> column);
        void RemoveColumn(IColumn<TModel> column);

        Task RenderAsync();
    }
}
