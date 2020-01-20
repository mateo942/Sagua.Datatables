using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sagua.Datatable.Example.Models
{
    public enum Role
    {
        Admin,
        User
    }

    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public DateTime? LastActiveAt { get; set; }
        public Role Role { get; set; }
    }
}
