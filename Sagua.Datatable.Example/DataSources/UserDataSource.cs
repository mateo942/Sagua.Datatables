using Sagua.Datatable.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sagua.Datatable.Example.DataSources
{
    public class UserDataSource : IDataSource<Models.User>
    {
        public int Total { get; set; }

        public async Task<IEnumerable<Models.User>> ExecuteAsync(IPaging paging)
        {
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

            Total = users.Count;

            return await Task.FromResult(users);
        }
    }
}
