using GovernmentExpenses.Expenses.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GovernmentExpenses.Expenses.Interfaces
{
    public interface IExpenseService
    {
        IExpenseResult FetchTotalExpenses();
        IDictionary<string, IExpenseResult> FetchTotalExpensesByProp(string prop);
        IEnumerable<string> FetchExpensesOrderKeys(bool? orderDesc);
        IEnumerable<string> FetchExpensesKeys(bool? orderDesc);
        IEnumerable<ExpenseDTO> FetchSearch(string criteria, string prop = null, string orderBy = null, bool? orderDesc = null);
        IEnumerable<ExpenseDTO> FetchAll(string orderBy = null, bool? orderDesc = null);
        IDictionary<string, IExpenseGroup> FetchExpensesGroupByProp(string prop);
        IEnumerable<ExpenseDTO> FetchExpenseGroupItems(string prop, object[] groupCode, string orderBy = null, bool? orderDesc = null);
        IEnumerable<ExpenseDTO> FetchExpenseGroupItems(string prop, object groupCode, string orderBy = null, bool? orderDesc = null);
        IReadOnlyList<IExpensePair> FetchEnum(string prop, string orderBy = null, bool? orderDesc = null);
        IEnumerable<string> FetchEnumKeys(bool? orderDesc);
        ExpenseDTO TryEditExpense(int id, ExpenseForm form);
        int ExpensesCount { get; }
    }
}
