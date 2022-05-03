using System;
using System.Collections.Generic;

namespace SSM.Models
{
    public partial class Department
    {
        public Department()
        {
            this.Students = new List<Student>();
        }

        public int DId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
