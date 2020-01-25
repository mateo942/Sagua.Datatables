using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sagua.Table.Abstractions
{
    public interface ITableDataSource<TModel>
    {
        Task<IEnumerable<TModel>> ExecuteAsync(IPaging paging);
        int TotalItems { get; }
    }
}
