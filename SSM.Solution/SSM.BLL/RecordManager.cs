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
    public class RecordManager
    {
        private DbSession session = new DbSession();

        //检查唯一性；
        public bool CheckOne(Record rd)
        {
            IRecordDAO dao = session.CreateDAO<IRecordDAO>();
            if (dao.Query(r => r.StuNo == rd.StuNo && r.SubId == rd.SubId).Count > 0)
            {
                return true;
            }
            return false;
        }

        //检查是否可修改；
        public bool CheckUpdate(Record rd)
        {
            IRecordDAO dao = session.CreateDAO<IRecordDAO>();
            Record r = GetRecord(rd.Id);
            if (r!=null)
            {
                if (r.StuNo==rd.StuNo&& r.SubId==rd.SubId)
                {
                    return true;
                }
                else
                {
                    if (!CheckOne(rd))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //增删改查；
        public void Add(Record rd)
        {
            IRecordDAO dao = session.CreateDAO<IRecordDAO>();
            dao.Add(rd);
            session.SaveChanges();
        }

        public void Update(Record rd)
        {
            IRecordDAO dao = session.CreateDAO<IRecordDAO>();
            dao.Update(rd);
            session.SaveChanges();
        }

        public void Delete(Record rd)
        {
            IRecordDAO dao = session.CreateDAO<IRecordDAO>();
            dao.Delete(rd);
            session.SaveChanges();
        }

        public void AllDelete(List<Record> Records)
        {
            IRecordDAO dao = session.CreateDAO<IRecordDAO>();
            foreach (var rd in Records)
            {
                dao.Delete(rd);
            }
            session.SaveChanges();
        }

        //根据ID获取；
        public Record GetRecord(int Id)
        {
            IRecordDAO dao = session.CreateDAO<IRecordDAO>();
            List<Record> Admins = dao.Query(adm => adm.Id == Id);
            if (Admins.Count > 0)
            {
                return Admins[0];
            }
            return null;
        }

        //根据学号和课程名称获取；
        public Record GetRecord(string StuNo,string SubName)
        {
            IRecordDAO dao = session.CreateDAO<IRecordDAO>();
            List<Record> Records = dao.Query(rd=>rd.StuNo==StuNo&&rd.Subject.Name==SubName);
            if (Records.Count > 0)
            {
                return Records[0];
            }
            return null;
        }

        //获取集合；
        public List<Record> GetRecords()
        {
            IRecordDAO dao = session.CreateDAO<IRecordDAO>();
            return dao.Query(null);
        }

        public List<Record> GetRecords(int SubId)
        {
            IRecordDAO dao = session.CreateDAO<IRecordDAO>();
            return dao.Query(rd=>rd.SubId ==SubId);
        }

        public List<Record> GetRecords(string StuNo)
        {
            IRecordDAO dao = session.CreateDAO<IRecordDAO>();
            return dao.Query(rd => rd.StuNo == StuNo);
        }

        //分页查询；
        public List<Record> GetRecords(int PageIndex, int PageSize, out int Pages)
        {
            IRecordDAO dao = session.CreateDAO<IRecordDAO>();
            return dao.PagingQuery<string>(PageIndex, PageSize, true, s => s.Id>0, s => s.StuNo, out Pages);
        }

        //根据条件【Option操作数】分页查询；
        public List<Record> GetRecords(int PageIndex, int PageSize, string StuNo, int Option, out int Pages)
        {
            IRecordDAO dao = session.CreateDAO<IRecordDAO>();
            if (Option==3)
            {
                return dao.PagingQuery<string>(PageIndex, PageSize, true, s => s.Student.Department.Name == StuNo, s => s.StuNo, out Pages);
            }
            else if (Option==2)
            {
                return dao.PagingQuery<string>(PageIndex, PageSize, true, s => s.Subject.Name == StuNo, s => s.StuNo, out Pages);
            }
            else if (Option==4)
            {
                 return dao.PagingQuery<string>(PageIndex, PageSize, true, s => s.Student.Grade.Name == StuNo, s => s.StuNo, out Pages);
            }
            else if (Option == 5)
            {
                double PassLine = 60;
                double.TryParse(StuNo, out PassLine);
                return dao.PagingQuery<string>(PageIndex, PageSize, true, s =>(s.Score!=null && s.Score>=PassLine), s => s.StuNo, out Pages);
            }
            else if (Option == 6)
            {
                return dao.PagingQuery<string>(PageIndex, PageSize, true, s => s.Score==null, s => s.StuNo, out Pages);
            }
            else if (Option == 7)
            {
                return dao.PagingQuery<string>(PageIndex, PageSize, true, s => true, s => s.StuNo, out Pages);
            }
            else if (Option == 8)
            {
                DateTime TurnTime;
                if (DateTime.TryParse(StuNo, out TurnTime))
                {
                    return dao.PagingQuery<string>(PageIndex, PageSize, true, s => s.ExamTime.ToLongDateString() == TurnTime.ToLongDateString(), s => s.StuNo, out Pages);
                }
                else
                {
                    return dao.PagingQuery<string>(PageIndex, PageSize, true, s => s.ExamTime.ToLongDateString() == StuNo, s => s.StuNo, out Pages);
                }
               
            }
            else
            {
                return dao.PagingQuery<string>(PageIndex, PageSize, true, s => s.StuNo == StuNo, s => s.StuNo, out Pages);
            }
        }

        //分级分页查询；
        public List<Record> GetRecords(int PageIndex, int PageSize, int DID, int GID, int SubID, double Score, out int Pages)
        {
            IRecordDAO dao = session.CreateDAO<IRecordDAO>();
            if (GID == 0)
            {
                if (SubID == 0)
                {
                    if (Score<0)
                    {
                        return dao.PagingQuery<string>(PageIndex, PageSize, true, s => (s.Student.DId == DID && s.Score==null), s => s.StuNo, out Pages);
                    }
                    else if (Score==0 || Score==100)
                    {
                        return dao.PagingQuery<string>(PageIndex, PageSize, true, s => (s.Student.DId == DID && s.Score != null && s.Score == Score), s => s.StuNo, out Pages);
                    }
                    else if (Score>10 && Score < 60)
                    {
                        return dao.PagingQuery<string>(PageIndex, PageSize, true, s => (s.Student.DId == DID && s.Score!=null && s.Score < 60), s => s.StuNo, out Pages);
                    }
                    else if (Score >= 60 && Score<80)
                    {
                        return dao.PagingQuery<string>(PageIndex, PageSize, true, s => (s.Student.DId == DID && s.Score != null && (s.Score >= 60 && s.Score < 80)), s => s.StuNo, out Pages);
                    }
                    else if (Score >= 80)
                    {
                        return dao.PagingQuery<string>(PageIndex, PageSize, true, s => (s.Student.DId == DID && s.Score != null && (s.Score>=Score && s.Score<Score+10) ), s => s.StuNo, out Pages);
                    }
                    else
                    {
                        return dao.PagingQuery<string>(PageIndex, PageSize, true, s => (s.Student.DId == DID), s => s.StuNo, out Pages);
                    }
                }

                if (Score < 0)
                {
                    return dao.PagingQuery<string>(PageIndex, PageSize, true, s => (s.SubId == SubID && s.Student.DId == DID && s.Score == null), s => s.StuNo, out Pages);
                }
                else if (Score == 0 || Score == 100)
                {
                    return dao.PagingQuery<string>(PageIndex, PageSize, true, s => (s.SubId == SubID && s.Student.DId == DID && s.Score != null && s.Score == Score), s => s.StuNo, out Pages);
                }
                else if (Score > 10 && Score < 60)
                {
                    return dao.PagingQuery<string>(PageIndex, PageSize, true, s => (s.SubId == SubID && s.Student.DId == DID && s.Score != null && s.Score < 60), s => s.StuNo, out Pages);
                }
                else if (Score >= 60 && Score < 80)
                {
                    return dao.PagingQuery<string>(PageIndex, PageSize, true, s => (s.SubId == SubID && s.Student.DId == DID && s.Score != null && (s.Score >= 60 && s.Score < 80)), s => s.StuNo, out Pages);
                }
                else if (Score >= 80)
                {
                    return dao.PagingQuery<string>(PageIndex, PageSize, true, s => (s.SubId == SubID && s.Student.DId == DID && s.Score != null && (s.Score >= Score && s.Score < Score + 10)), s => s.StuNo, out Pages);
                }
                else
                {
                    return dao.PagingQuery<string>(PageIndex, PageSize, true, s => (s.SubId == SubID && s.Student.DId == DID), s => s.StuNo, out Pages);
                }
            }
            else
            {
                if (SubID == 0)
                {
                    if (Score < 0)
                    {
                        return dao.PagingQuery<string>(PageIndex, PageSize, true, s => (s.Student.DId == DID && s.Student.GId == GID && s.Score == null), s => s.StuNo, out Pages);
                    }
                    else if (Score == 0 || Score == 100)
                    {
                        return dao.PagingQuery<string>(PageIndex, PageSize, true, s => (s.Student.DId == DID && s.Student.GId == GID && s.Score != null && s.Score == Score), s => s.StuNo, out Pages);
                    }
                    else if (Score > 10 && Score < 60)
                    {
                        return dao.PagingQuery<string>(PageIndex, PageSize, true, s => (s.Student.DId == DID && s.Student.GId == GID && s.Score != null && s.Score < 60), s => s.StuNo, out Pages);
                    }
                    else if (Score >= 60 && Score < 80)
                    {
                        return dao.PagingQuery<string>(PageIndex, PageSize, true, s => (s.Student.DId == DID && s.Student.GId == GID && s.Score != null && (s.Score >= 60 && s.Score < 80)), s => s.StuNo, out Pages);
                    }
                    else if (Score >= 80)
                    {
                        return dao.PagingQuery<string>(PageIndex, PageSize, true, s => (s.Student.DId == DID && s.Student.GId == GID && s.Score != null && (s.Score >= Score && s.Score < Score + 10)), s => s.StuNo, out Pages);
                    }
                    else
                    {
                        return dao.PagingQuery<string>(PageIndex, PageSize, true, s => (s.Student.DId == DID && s.Student.GId == GID), s => s.StuNo, out Pages);
                    }
                }

                if (Score < 0)
                {
                    return dao.PagingQuery<string>(PageIndex, PageSize, true, s => (s.SubId == SubID && s.Student.DId == DID && s.Student.GId == GID && s.Score == null), s => s.StuNo, out Pages);
                }
                else if (Score == 0 || Score == 100)
                {
                    return dao.PagingQuery<string>(PageIndex, PageSize, true, s => (s.SubId == SubID && s.Student.DId == DID && s.Student.GId == GID && s.Score != null && s.Score == Score), s => s.StuNo, out Pages);
                }
                else if (Score > 10 && Score < 60)
                {
                    return dao.PagingQuery<string>(PageIndex, PageSize, true, s => (s.SubId == SubID && s.Student.DId == DID && s.Student.GId == GID && s.Score != null && s.Score < 60), s => s.StuNo, out Pages);
                }
                else if (Score >= 60 && Score < 80)
                {
                    return dao.PagingQuery<string>(PageIndex, PageSize, true, s => (s.SubId == SubID && s.Student.DId == DID && s.Student.GId == GID && s.Score != null && (s.Score >= 60 && s.Score < 80)), s => s.StuNo, out Pages);
                }
                else if (Score >= 80)
                {
                    return dao.PagingQuery<string>(PageIndex, PageSize, true, s => (s.SubId == SubID && s.Student.DId == DID && s.Student.GId == GID && s.Score != null && (s.Score >= Score && s.Score < Score + 10)), s => s.StuNo, out Pages);
                }
                else
                {
                    return dao.PagingQuery<string>(PageIndex, PageSize, true, s => (s.SubId == SubID && s.Student.DId == DID && s.Student.GId == GID), s => s.StuNo, out Pages);
                }
            }
        }

    }
}
