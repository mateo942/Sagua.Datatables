using System;
using System.Collections.Generic;
using System.Text;

namespace Sagua.Datatable.Abstractions
{
    public interface IDataSourceTable<TModel>
    {
        void SetDataSource(IDataSource<TModel> dataSource);
        IDataSource<TModel> GetDataSource();
    }
}
