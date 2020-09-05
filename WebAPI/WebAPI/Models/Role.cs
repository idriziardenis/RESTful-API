using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public int RoleName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
