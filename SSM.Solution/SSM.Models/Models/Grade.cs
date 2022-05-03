using System;
using System.Collections.Generic;

namespace SSM.Models
{
    public partial class Grade
    {
        public Grade()
        {
            this.Students = new List<Student>();
        }

        public int GId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
