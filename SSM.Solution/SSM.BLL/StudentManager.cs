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
    public class StudentManager
    {
        private DbSession session = new DbSession();

        //获取总数；
        public int GetCount()
        {
            IStudentDAO dao = session.CreateDAO<IStudentDAO>();
            return dao.Query(null).Count;
        }

        //增删改查；
        public void Add(Student student)
        {
            IStudentDAO dao = session.CreateDAO<IStudentDAO>();
            dao.Add(student);
            session.SaveChanges();
        }

        public void Update(Student student)
        {
            IStudentDAO dao = session.CreateDAO<IStudentDAO>();
            dao.Update(student);
            session.SaveChanges();
        }

        public void Delete(Student student)
        {
            IStudentDAO dao = session.CreateDAO<IStudentDAO>();
            dao.Delete(student);
            session.SaveChanges();
        }

        public void AllDelete(List<Student> students)
        {
            IStudentDAO dao = session.CreateDAO<IStudentDAO>();
            foreach (var item in students)
            {
                RecordManager Scorem=new RecordManager();
                List<Record> Scores = Scorem.GetRecords(item.StuNo);
                Scorem.AllDelete(Scores);
                dao.Delete(item);
            }
            session.SaveChanges();
        }

        //登录；
        public Student Login(Student stu) 
        {
            IStudentDAO dao = session.CreateDAO<IStudentDAO>();
            List<Student> Stus = dao.Query(st=>st.StuNo==stu.StuNo&&st.LoginPwd==stu.LoginPwd);
            if (Stus.Count>0)
            {
                return Stus[0];
            }
            return null;
        }

        //根据学号获取；
        public Student GetStudent(string LoginName)
        {
            IStudentDAO dao = session.CreateDAO<IStudentDAO>();
            List<Student> Admins = dao.Query(adm => adm.StuNo == LoginName);
            if (Admins.Count > 0)
            {
                return Admins[0];
            }
            return null;
        }

        //获取所有集合；
        public List<Student> GetStudets() 
        {
            IStudentDAO dao = session.CreateDAO<IStudentDAO>();
            return dao.Query(null);
        }

        //根据条件查找集合；
        public List<Student> GetStudets(int GID)
        {
            IStudentDAO dao = session.CreateDAO<IStudentDAO>();
            return dao.Query(st=>st.GId==GID);
        }

        public List<Student> GetStudetsByD(int DID)
        {
            IStudentDAO dao = session.CreateDAO<IStudentDAO>();
            return dao.Query(st => st.DId == DID);
        }

        //分页查询；
        public List<Student> GetStudets(int PageIndex, int PageSize, out int Pages)
        {
            IStudentDAO dao = session.CreateDAO<IStudentDAO>();
            return dao.PagingQuery<string>(PageIndex, PageSize, true, s => true, s => s.StuNo, out Pages);
        }

        public List<Student> PageStudents(int pageIndex, int pageSize, out int pages, string stuNo)
        {
            IStudentDAO dao = session.CreateDAO<IStudentDAO>();
            if (stuNo == null)
            {
                return dao.PagingQuery<string>(pageIndex, pageSize, true, s => true, s => s.StuNo, out pages);
            }
            return dao.PagingQuery<string>(pageIndex, pageSize, true, s => s.StuNo == stuNo, s => s.StuNo, out pages);
        }

    }
}
