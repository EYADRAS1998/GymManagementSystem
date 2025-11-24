using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }

        public PagedResult(IEnumerable<T> items, int totalCount)
        {
            Items = items;
            TotalCount = totalCount;
        }
    }
}
