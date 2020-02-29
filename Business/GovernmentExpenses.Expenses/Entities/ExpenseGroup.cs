using System;
using System.Collections.Generic;
using System.Text;

namespace GovernmentExpenses.Expenses.Entities
{
    public interface IExpenseGroup
    {
        object Id { get; }
        int ItemsCount { get; }
    }
    internal class ExpenseGroup : IExpenseGroup
    {
        public object Id { get; set; }
        public int ItemsCount { get; set; }
    }
}
