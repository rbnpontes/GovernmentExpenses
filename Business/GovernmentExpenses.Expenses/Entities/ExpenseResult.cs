using GovernmentExpenses.Expenses.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GovernmentExpenses.Expenses.Entities
{
    internal sealed class ExpenseResult : IExpenseResult
    {
        public float TotalCommited { get; set; }
        public float TotalPayed { get; set; }
        public float TotalSettled { get; set; }
    }
}
