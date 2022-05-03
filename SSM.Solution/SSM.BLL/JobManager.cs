using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SSM.Factory;
using SSM.IDAL;
using SSM.Models;
namespace SSM.BLL
{
    public class JobManager
    {
        DbSession session = new DbSession();

        //根据ID获取；
        public Job GetJob(int JId)
        {
            IJobDAO dao = session.CreateDAO<IJobDAO>();
            List<Job> Jobs= dao.Query(j=>j.JId==JId);
            if (Jobs.Count>0)
            {
                return Jobs[0];
            }
            return null;
        }

        //获取所有；
        public List<Job> GetJobs()
        {
            IJobDAO dao = session.CreateDAO<IJobDAO>();
            return dao.Query(null);
        }
    }
}
