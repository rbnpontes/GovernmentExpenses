using System;
using System.Collections.Generic;
using System.Text;

namespace GovernmentExpenses.Core
{
    public class RouterDesc
    {
        public string Route { get; set; }
        public string Desc { get; set; }
        public string Method { get; set; } = "GET";
        public string QueryParams { get; set; } = string.Empty;
    }
}
