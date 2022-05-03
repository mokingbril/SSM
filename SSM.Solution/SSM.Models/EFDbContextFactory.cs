using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace SSM.Models
{
    public static class EFDbContextFactory
    {
        public static StuScoreManagerContext GetContext()
        {
            StuScoreManagerContext context = CallContext.GetData("StuScoreManagerContext") as StuScoreManagerContext;
            if (context == null)
            {
                context = new StuScoreManagerContext();
                CallContext.SetData("StuScoreManagerContext", context);
            }

            return context;
        }
    }
}
