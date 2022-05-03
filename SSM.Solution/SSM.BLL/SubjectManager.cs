using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SSM.IDAL;
using SSM.Factory;
using SSM.Models;

namespace SSM.BLL
{
    public class SubjectManager
    {
        private DbSession session = new DbSession();

        //检查唯一性；
        public bool CheckOne(Subject sub)
        {
            ISubjectDAO dao = session.CreateDAO<ISubjectDAO>();
            if (dao.Query(r => r.Name == sub.Name).Count > 0)
            {
                return true;
            }
            return false;
        }

        //检查是否可修改；
        public bool CheckUpdate(Subject sub)
        {
            ISubjectDAO dao = session.CreateDAO<ISubjectDAO>();
            List<Subject> sb = dao.Query(r => r.SubId == sub.SubId);
            if (sb.Count>0)
            {
                if (sb[0].Name == sub.Name)
                {
                    return true;
                }
                else
                {
                    if (!CheckOne(sub))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //增删改查；
        public void Add(Subject teacher)
        {
            ISubjectDAO dao = session.CreateDAO<ISubjectDAO>();
            dao.Add(teacher);
            session.SaveChanges();
        }

        public void Update(Subject teacher)
        {
            ISubjectDAO dao = session.CreateDAO<ISubjectDAO>();
            dao.Update(teacher);
            session.SaveChanges();
        }

        public void Delete(Subject teacher)
        {
            ISubjectDAO dao = session.CreateDAO<ISubjectDAO>();
            dao.Delete(teacher);
            session.SaveChanges();
        }

        //根据ID获取；
        public Subject GetSubject(int SubjectId)
        {
            ISubjectDAO dao = session.CreateDAO<ISubjectDAO>();
            List<Subject> Admins = dao.Query(adm => adm.SubId == SubjectId);
            if (Admins.Count > 0)
            {
                return Admins[0];
            }
            return null;
        }

        //获取所有；
        public List<Subject> GetSubjects()
        {
            ISubjectDAO dao = session.CreateDAO<ISubjectDAO>();
            return dao.Query(null);
        }

        //分页查找；
        public List<Subject> GetSubjects(int PageIndex, int PageSize, out int Pages)
        {
            ISubjectDAO dao = session.CreateDAO<ISubjectDAO>();
            return dao.PagingQuery<int>(PageIndex, PageSize, true, s => true, s => s.SubId, out Pages);
        }
    }
}
