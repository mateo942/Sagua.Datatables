using Sagua.Datatable.Abstractions;
using Sagua.Datatable.Example.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sagua.Datatable.Example.DataSources
{
    public class TodoDatasource : IDataSource<Models.Todo>
    {
        private readonly HttpClient _httpClient;

        public TodoDatasource(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public int Total { get; set; }

        public async Task<IEnumerable<Todo>> ExecuteAsync(IDataSourcePaging paging)
        {
            var url = "https://jsonplaceholder.typicode.com/todos?";

            if (paging.Limit.HasValue)
            {
                url += $"_limit={paging.Limit.Value}&";
            }

            if (paging.Page.HasValue)
            {
                url += $"_start={paging.Page.Value}&";
            }

            Console.WriteLine("Downloading");
            var httpResponseMessage = _httpClient.GetAsync(url).GetAwaiter().GetResult();
            Console.WriteLine("Downloaded");
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                Console.WriteLine("Reading");
                var content = await httpResponseMessage.Content.ReadAsStringAsync();
                Total = 500;
                Console.WriteLine("Readed");

                Console.WriteLine("Parsing");
                var data = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Todo>>(content);
                Console.WriteLine("Parsed");
                return data;
            }

            Total = 0;
            return Enumerable.Empty<Todo>();
        }
    }
}
