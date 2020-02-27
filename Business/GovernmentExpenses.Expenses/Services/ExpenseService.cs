using GovernmentExpenses.Expenses.Entities;
using System;
using System.Collections.Generic;

namespace GovernmentExpenses.Expenses.Services
{
    public interface IExpenseService
    {
        IDictionary<string, ExpenseDTO> FetchGroupItems();
        IDictionary<string, ExpenseDTO> FetchGroupItemsPerMonth();

    }
    internal class ExpenseService
    {
    }
}
