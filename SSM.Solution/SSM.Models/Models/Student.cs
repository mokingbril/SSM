using System;
using System.Collections.Generic;

namespace SSM.Models
{
    public partial class Student
    {
        public Student()
        {
            this.Records = new List<Record>();
        }

        public string StuNo { get; set; }
        public string LoginPwd { get; set; }
        public string Name { get; set; }
        public bool Sex { get; set; }
        public int DId { get; set; }
        public int GId { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public virtual Department Department { get; set; }
        public virtual Grade Grade { get; set; }
        public virtual ICollection<Record> Records { get; set; }
    }
}
