using GovernmentExpenses.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace GovernmentExpenses.Expenses.Controllers
{
    public partial class ExpenseController
    {
        public readonly IList<RouterDesc> RoutersDesc = new List<RouterDesc>
        {
            new RouterDesc{ Route = "/api/expenses/enums", Desc = "Retrieve Descriptions of Routes"},
            new RouterDesc{ Route = "/api/expenses/enums/:prop", Desc="Retrieve all enums by specific property", QueryParams="(OPTIONAL)orderBy=string;(OPTIONAL)orderDesc=boolean"},
            new RouterDesc{ Route = "/api/expenses/enums/keys", Desc="Retrieve all keys used for Group entities or Enums", QueryParams="(OPTIONAL)desc=boolean"}
        };
    }
}
