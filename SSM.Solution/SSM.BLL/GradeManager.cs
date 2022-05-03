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
    public class GradeManager
    {
        private DbSession session = new DbSession();

        //增删改查；
        public void Add(Grade gd)
        {
            IGradeDAO dao = session.CreateDAO<IGradeDAO>();
            dao.Add(gd);
            session.SaveChanges();
        }

        public void Update(Grade gd)
        {
            IGradeDAO dao = session.CreateDAO<IGradeDAO>();
            dao.Update(gd);
            session.SaveChanges();
        }

        public void Delete(Grade gd)
        {
            IGradeDAO dao = session.CreateDAO<IGradeDAO>();
            dao.Delete(gd);
            session.SaveChanges();
        }

        //ID获取；
        public Grade GetGrade(int GId)
        {
            IGradeDAO dao = session.CreateDAO<IGradeDAO>();
            List<Grade> Grades = dao.Query(gd => gd.GId == GId);
            if (Grades.Count>0)
            {
                return Grades[0];
            }
            return null;
        }

        public Grade GetGrade(string Name)
        {
            IGradeDAO dao = session.CreateDAO<IGradeDAO>();
            List<Grade> Grades = dao.Query(gd => gd.Name==Name);
            if (Grades.Count > 0)
            {
                return Grades[0];
            }
            return null;
        }

        //获取所有；
        public List<Grade> GetGrades()
        {
            IGradeDAO dao = session.CreateDAO<IGradeDAO>();
            return dao.Query(null);
        }

        //分页查询；
        public List<Grade> GetGrades(int PageIndex, int PageSize, out int Pages)
        {
            IGradeDAO dao = session.CreateDAO<IGradeDAO>();
            return dao.PagingQuery<int>(PageIndex, PageSize, true, s => true, s => s.GId, out Pages);
        }

    }
}
