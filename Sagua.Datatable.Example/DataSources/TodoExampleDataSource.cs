using Sagua.Datatable.Example.Models;
using Sagua.Table.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Sagua.Datatable.Example.DataSources
{
    public class TodoExampleDataSource : ITableDataSource<Models.Todo>
    {
        public int TotalItems { get; private set; }

        public async Task<IEnumerable<Todo>> ExecuteAsync(IPaging paging)
        {
            var data = Enumerable.Range(0, 1000).Select(x => new Models.Todo
            {
                Completed = x % 2 == 0,
                Id = x,
                UserId = x,
                Title = $"Task: {x}"
            });

            await Task.Delay(500);
            var tempData = data.ToList().AsQueryable();

            TotalItems = tempData.Count();

            if (!string.IsNullOrEmpty(paging.OrderBy))
            {
                if (paging.OrderDirection == OrderDirection.Asc)
                {
                    tempData = tempData.OrderBy(x => x.GetType().GetProperty(paging.OrderBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).GetValue(x));
                }
                else
                {
                    tempData = tempData.OrderByDescending(x => x.GetType().GetProperty(paging.OrderBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).GetValue(x));
                }
            }

            tempData = tempData.Skip(paging.Limit * paging.Page).Take(paging.Limit);

            return tempData;
        }
    }
}
