using GovernmentExpenses.Core;
using GovernmentExpenses.Expenses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GovernmentExpenses.Expenses.Services
{
    public interface IExpenseService
    {
        IExpenseResult FetchTotalExpenses();
        IDictionary<string, IExpenseResult> FetchTotalExpensesByProp(string prop);
        IDictionary<string, IReadOnlyList<ExpenseDTO>> FetchExpensesGroupByProp(string prop, int page = 0, int pageSize = 10, string orderBy = "", bool orderDesc = false);
        IReadOnlyList<ExpenseDTO> FetchAll(int page = 0, int pageSize = 10, string orderBy = "id", bool orderDesc = false);
        IReadOnlyList<ExpensePair> FetchEnum(string prop);
    }
    internal partial class ExpenseService : IExpenseService
    {
        private IEnumerable<Expense> TryOrderList(IEnumerable<Expense> list, string orderBy, bool desc){
            if (string.IsNullOrEmpty(orderBy) || !OrderKeyPairs.ContainsKey(orderBy))
                return list;
            if (desc)
                return list.OrderByDescending(OrderKeyPairs[orderBy]);
            return list.OrderBy(OrderKeyPairs[orderBy]);
        }
        public IReadOnlyList<ExpenseDTO> FetchAll(int page = 0, int pageSize = 10, string orderBy = "id", bool orderDesc = false)
        {
            var values = TryOrderList(Repository.All().Page(page, pageSize), orderBy, orderDesc);
            return values.Select(x => new ExpenseDTO(x)).ToList().AsReadOnly();
        }
        public IReadOnlyList<ExpensePair> FetchEnum(string prop)
        {
            throw new NotImplementedException();
        }
        public IDictionary<string, IReadOnlyList<ExpenseDTO>> FetchExpensesGroupByProp(string prop, int page = 0, int pageSize = 10, string orderBy = "", bool orderDesc = false)
        {
            throw new NotImplementedException();
        }
        public IExpenseResult FetchTotalExpenses()
        {
            throw new NotImplementedException();
        }
        public IDictionary<string, IExpenseResult> FetchTotalExpensesByProp(string prop)
        {
            throw new NotImplementedException();
        }
    }
}
