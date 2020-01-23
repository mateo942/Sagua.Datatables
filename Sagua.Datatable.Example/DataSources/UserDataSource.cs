using Sagua.Datatable.Abstractions;
using Sagua.Datatable.Example.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Sagua.Datatable.Example.DataSources
{
    public class UserDataSource : IDataSource<Models.User>
    {
        public int Total { get; set; }

        public async Task<IEnumerable<User>> ExecuteAsync(IDataSourcePaging paging)
        {
            Console.WriteLine("GetData: {0}", System.Text.Json.JsonSerializer.Serialize(paging));

            var users = new List<Models.User>();

            users.Add(new Models.User
            {
                Id = Guid.NewGuid(),
                Login = "admin",
                Email = "admin@sagua.com",
                LastActiveAt = DateTime.Now,
                Role = Models.Role.Admin
            });

            users.Add(new Models.User
            {
                Id = Guid.NewGuid(),
                Login = "user",
                Email = "user@sagua.com",
                LastActiveAt = DateTime.Now.AddMinutes(-233),
                Role = Models.Role.User
            });

            for (int i = 0; i < 500; i++)
            {
                users.Add(new Models.User
                {
                    Id = Guid.NewGuid(),
                    Login = $"user {i}",
                    Email = "user@sagua.com",
                    LastActiveAt = DateTime.Now.AddMinutes(-123),
                    Role = Models.Role.User
                });
            }

            Total = users.Count;

            var query = users.AsQueryable();
            if (!string.IsNullOrEmpty(paging.OrderBy))
            {
                if (paging.OrderDirection == OrderDirection.Asc)
                {
                    query = query.OrderBy(x => x.GetType().GetProperty(paging.OrderBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).GetValue(x));
                }
                else
                {
                    query = query.OrderByDescending(x => x.GetType().GetProperty(paging.OrderBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).GetValue(x));
                }
            }

            query = query.Skip(paging.Limit.Value * paging.Page.Value).Take(paging.Limit.Value);

            

            return await Task.FromResult(query.ToList());
        }
    }
}
