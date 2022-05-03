using System;
using System.Collections.Generic;

namespace SSM.Models
{
    public partial class Teacher
    {
        public string TeaNo { get; set; }
        public string Name { get; set; }
        public string LoginPwd { get; set; }
        public bool Sex { get; set; }
        public int JId { get; set; }
        public virtual Job Job { get; set; }
    }
}
