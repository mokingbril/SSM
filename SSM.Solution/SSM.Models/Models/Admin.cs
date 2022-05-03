using System;
using System.Collections.Generic;

namespace SSM.Models
{
    public partial class Admin
    {
        public int Id { get; set; }
        public string LoginName { get; set; }
        public string LoginPwd { get; set; }
    }
}
