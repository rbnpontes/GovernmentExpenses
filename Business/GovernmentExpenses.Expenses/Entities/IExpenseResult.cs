using System;
using System.Collections.Generic;
using System.Text;

namespace GovernmentExpenses.Expenses.Entities
{
    public interface IExpenseResult
    {
        float TotalCommited { get; }
        float TotalPayed { get; }
        float TotalSettled { get; }
    }
    internal sealed class ExpenseResult : IExpenseResult
    {
        public float TotalCommited { get; set; }
        public float TotalPayed { get; set; }
        public float TotalSettled { get; set; }
    }
}
