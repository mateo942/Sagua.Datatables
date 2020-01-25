using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sagua.Datatable.Example.Models
{
    public class Todo
    {
        public long UserId { get; set; }
        public long Id { get; set; }
        public string Title { get; set; }
        public bool Completed { get; set; }
    }
}
