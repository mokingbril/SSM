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
    public class DepartmentManager
    {
        private DbSession session = new DbSession();

        //增删改查；
        public void Add(Department dt)
        {
            IDepartmentDAO dao = session.CreateDAO<IDepartmentDAO>();
            dao.Add(dt);
            session.SaveChanges();
        }

        public void Update(Department dt)
        {
            IDepartmentDAO dao = session.CreateDAO<IDepartmentDAO>();
            dao.Update(dt);
            session.SaveChanges();
        }

        public void Delete(Department dt)
        {
            IDepartmentDAO dao = session.CreateDAO<IDepartmentDAO>();
            dao.Delete(dt);
            session.SaveChanges();
        }

        //ID获取；
        public Department GetDepartment(int DId)
        {
           IDepartmentDAO dao = session.CreateDAO<IDepartmentDAO>();
           List<Department> Departments = dao.Query(dt => dt.DId == DId);
           if (Departments.Count > 0)
            {
                return Departments[0];
            }
            return null;
        }

        public Department GetDepartment(string Name)
        {
            IDepartmentDAO dao = session.CreateDAO<IDepartmentDAO>();
            List<Department> Departments = dao.Query(dt => dt.Name==Name);
            if (Departments.Count > 0)
            {
                return Departments[0];
            }
            return null;
        }

        //获取所有；
        public List<Department> GetDepartments()
        {
            IDepartmentDAO dao = session.CreateDAO<IDepartmentDAO>();
            return dao.Query(null);
        }

        //分页查询；
        public List<Department> GetDepartments(int PageIndex, int PageSize, out int Pages)
        {
            IDepartmentDAO dao = session.CreateDAO<IDepartmentDAO>();
            return dao.PagingQuery<int>(PageIndex, PageSize, true, s => true, s => s.DId, out Pages);
        }

    }
}
