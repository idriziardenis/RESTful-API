using System;
using System.Collections.Generic;

namespace WebAPI.Models
{
    public partial class Status
    {
        public Status()
        {
            Students = new HashSet<Student>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
