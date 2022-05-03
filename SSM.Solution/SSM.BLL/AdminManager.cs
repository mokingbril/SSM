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
    public class AdminManager
    {
        private DbSession session = new DbSession();

        //检查是否可修改；
        public bool CheckUpdate(Admin admin)
        {
            IAdminDAO dao = session.CreateDAO<IAdminDAO>();
            Admin r = GetAdmin(admin.Id);
            if (r != null)
            {
                if (r.LoginName == admin.LoginName)
                {
                    return true;
                }
                else
                {
                    if (dao.Query(ad=>ad.LoginName==admin.LoginName).Count<=0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //增删改查；
        public void Add(Admin admin) 
        {
            IAdminDAO dao = session.CreateDAO<IAdminDAO>();
            dao.Add(admin);
            session.SaveChanges();
        }

        public void Update(Admin admin)
        {
            IAdminDAO dao = session.CreateDAO<IAdminDAO>();
            dao.Update(admin);
            session.SaveChanges();
        }

        public void Delete(Admin admin)
        {
            IAdminDAO dao = session.CreateDAO<IAdminDAO>();
            dao.Delete(admin);
            session.SaveChanges();
        }

        //登录；
        public Admin Login(Admin stu) 
        {
            IAdminDAO dao = session.CreateDAO<IAdminDAO>();
            List<Admin> Stus = dao.Query(st => st.LoginName == stu.LoginName && st.LoginPwd == stu.LoginPwd);
            if (Stus.Count>0)
            {
                return Stus[0];
            }
            return null;
        }

        //根据条件获取；
        public Admin GetAdmin(int Id)
        {
            IAdminDAO dao = session.CreateDAO<IAdminDAO>();
            List<Admin> Admins = dao.Query(adm => adm.Id == Id);
            if (Admins.Count > 0)
            {
                return Admins[0];
            }
            return null;
        } 

        public Admin GetAdmin(string LoginName) 
        {
            IAdminDAO dao = session.CreateDAO<IAdminDAO>();
            List<Admin> Admins= dao.Query(adm=>adm.LoginName==LoginName);
            if (Admins.Count>0)
            {
                return Admins[0];
            }
            return null;
        }

        //获取所有集合；
        public List<Admin> GetAdmins() 
        {
            IAdminDAO dao = session.CreateDAO<IAdminDAO>();
            return dao.Query(null);
        }

        //分页查询；
        public List<Admin> GetAdmins(int PageIndex, int PageSize, out int Pages)
        {
            IAdminDAO dao = session.CreateDAO<IAdminDAO>();
            return dao.PagingQuery<int>(PageIndex, PageSize, true, s => true, s => s.Id, out Pages);
        }

    }
}
