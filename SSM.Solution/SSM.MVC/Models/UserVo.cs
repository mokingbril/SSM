using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSM.MVC.Models
{
    public class UserVo
    {
        public string LoginName { get;private set; }
        public int RId { get; private set; }

        public UserVo(string name, int RoleId) 
        {
            LoginName = name;
            RId = RoleId;
        }

    }
}