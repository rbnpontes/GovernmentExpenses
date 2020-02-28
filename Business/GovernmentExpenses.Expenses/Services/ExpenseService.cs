using GovernmentExpenses.Expenses.Entities;
using System;
using System.Collections.Generic;

namespace GovernmentExpenses.Expenses.Services
{
    public interface IExpenseService
    {
        ExpenseDTO FetchTotalExpenses();
        IDictionary<string, ExpenseDTO> FetchTotalExpensesByProp();
        IDictionary<string, ExpenseDTO> FetchGroupItemsPerMonth();
        
    }
    internal class ExpenseService : IExpenseService
    {
    }
}
