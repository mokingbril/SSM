using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SSM.Models;
using System.Data.Entity;

namespace SSM.EFDAL
{
    public class BaseDAO<T> where T:class,new()
    {
        private StuScoreManagerContext context = EFDbContextFactory.GetContext();

        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            context.Entry<T>(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            context.Entry<T>(entity).State = EntityState.Deleted;
        }

        public List<T> Query(Func<T, bool> lambdaWhere)
        {
            if (lambdaWhere == null)
            {
                return context.Set<T>().ToList();
            }
            else
            {
                return context.Set<T>().Where(lambdaWhere).ToList();
            }
        }

        public List<T> PagingQuery<S>(int pageIndex, int pageSize, bool IsAsc, Func<T, bool> lambdaWhere, Func<T, S> lambdaOrder, out int pages)
        {
            var tmp = context.Set<T>().Where(lambdaWhere);
            pages = tmp.Count() % pageSize == 0 ? tmp.Count() / pageSize : tmp.Count() / pageSize + 1;

            List<T> list = null;
            if (IsAsc)
            {
                list = tmp.OrderBy(lambdaOrder)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
            else
            {
                list = tmp.OrderByDescending(lambdaOrder)
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
            }

            return list;
        }
    }
}
