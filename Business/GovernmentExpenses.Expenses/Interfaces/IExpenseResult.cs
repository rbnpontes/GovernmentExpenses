using System;
using System.Collections.Generic;
using System.Text;

namespace GovernmentExpenses.Expenses.Interfaces
{
    public interface IExpenseResult
    {
        object GroupCode { get; }
        float TotalCommited { get; }
        float TotalPayed { get; }
        float TotalSettled { get; }
    }
}
