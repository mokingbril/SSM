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
    public class TeacherManager
    {
        private DbSession session = new DbSession();

        //增删改查；
        public void Add(Teacher teacher)
        {
            ITeacherDAO dao = session.CreateDAO<ITeacherDAO>();
            dao.Add(teacher);
            session.SaveChanges();
        }

        public void Update(Teacher teacher)
        {
            ITeacherDAO dao = session.CreateDAO<ITeacherDAO>();
            dao.Update(teacher);
            session.SaveChanges();
        }

        public void Delete(Teacher teacher)
        {
            ITeacherDAO dao = session.CreateDAO<ITeacherDAO>();
            dao.Delete(teacher);
            session.SaveChanges();
        }

        //获取总数；
        public int GetCount()
        {
            ITeacherDAO dao = session.CreateDAO<ITeacherDAO>();
            return dao.Query(null).Count;
        }

        //登录；
        public Teacher Login(Teacher stu) 
        {
            ITeacherDAO dao = session.CreateDAO<ITeacherDAO>();
            List<Teacher> Stus = dao.Query(st => st.TeaNo == stu.TeaNo && st.LoginPwd == stu.LoginPwd);
            if (Stus.Count>0)
            {
                return Stus[0];
            }
            return null;
        }

        //根据编号获取；
        public Teacher GetTeacher(string LoginName)
        {
            ITeacherDAO dao = session.CreateDAO<ITeacherDAO>();
            List<Teacher> Admins = dao.Query(adm => adm.TeaNo == LoginName);
            if (Admins.Count > 0)
            {
                return Admins[0];
            }
            return null;
        }

        //获取所有；
        public List<Teacher> GetTeachers() 
        {
            ITeacherDAO dao = session.CreateDAO<ITeacherDAO>();
            return dao.Query(null);
        }

        //分页查询；
        public List<Teacher> GetTeachers(int PageIndex,int PageSize,out int Pages) 
        {
            ITeacherDAO dao = session.CreateDAO<ITeacherDAO>();
            return dao.PagingQuery<string>(PageIndex, PageSize, true, s=>true, s => s.TeaNo,out Pages);
        }

    }
}
