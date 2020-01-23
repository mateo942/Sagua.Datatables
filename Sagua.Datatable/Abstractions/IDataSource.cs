using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sagua.Datatable.Abstractions
{
    public interface IDataSource<TModel>
    {
        int Total { get; set; }
        Task<IEnumerable<TModel>> ExecuteAsync(IDataSourcePaging paging);
    }
}
