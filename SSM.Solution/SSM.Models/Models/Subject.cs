using System;
using System.Collections.Generic;

namespace SSM.Models
{
    public partial class Subject
    {
        public Subject()
        {
            this.Records = new List<Record>();
        }

        public int SubId { get; set; }
        public string Name { get; set; }
        public int Period { get; set; }
        public double Score { get; set; }
        public virtual ICollection<Record> Records { get; set; }
    }
}
