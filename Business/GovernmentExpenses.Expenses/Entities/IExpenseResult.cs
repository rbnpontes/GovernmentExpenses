using System;
using System.Collections.Generic;
using System.Text;

namespace GovernmentExpenses.Expenses.Entities
{
    public interface IExpenseResult
    {
        int TotalCommited { get; }
        int TotalPayed { get; }
        int TotalSettled { get; }
    }
    internal sealed class ExpenseResult : IExpenseResult
    {
        public int TotalCommited { get; set; }
        public int TotalPayed { get; set; }
        public int TotalSettled { get; set; }
    }
}
