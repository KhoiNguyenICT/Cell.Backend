using System.Collections.Generic;

namespace Cell.Helpers.Models
{
    public class PagingResult<T>
    {
        public int TotalRecord { get; set; }

        public int PageSize { get; set; }

        public IEnumerable<T> Items { get; set; }
    }
}