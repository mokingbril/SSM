using System;
using System.Collections.Generic;

namespace SSM.Models
{
    public partial class Job
    {
        public Job()
        {
            this.Teachers = new List<Teacher>();
        }

        public int JId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
