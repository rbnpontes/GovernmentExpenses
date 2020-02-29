using System;
using System.Collections.Generic;
using System.Text;

namespace GovernmentExpenses.Core
{
    public sealed class Pager<T>
    {
        public int Page { get; set; }
        public int PageCount { get; set; }
        public int TotalItems { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
