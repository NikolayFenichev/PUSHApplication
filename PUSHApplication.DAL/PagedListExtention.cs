using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Paging;
using System.Collections.Generic;

namespace PUSHApplication.DAL
{
    class PagedListExtention<T>: PagedList<T> where T : class, new()
    {
        public static PagedList<T> ToPagedList(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
