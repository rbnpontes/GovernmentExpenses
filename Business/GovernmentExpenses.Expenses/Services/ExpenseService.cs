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
        IReadOnlyList<IExpensePair> FetchEnum(string prop, string orderBy = null, bool? orderDesc = null);
        IEnumerable<string> FetchEnumKeys(bool? orderDesc);
    }
    internal partial class ExpenseService : IExpenseService
    {
        private IEnumerable<Expense> TryOrderList(IEnumerable<Expense> list, string orderBy, bool desc)
        {
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
        public IReadOnlyList<IExpensePair> FetchEnum(string prop, string orderBy = null, bool? orderDesc = null)
        {
            if (string.IsNullOrEmpty(prop) || !EnumKeyPairs.ContainsKey(prop))
                return new List<IExpensePair>().AsReadOnly();
            var values = Repository.All().Select(EnumKeyPairs[prop]).DistinctBy(x => x.Code);

            if (!string.IsNullOrEmpty(orderBy) && (orderBy == "code" || orderBy == "name"))
                orderDesc = orderDesc ?? false;
            else if (orderDesc != null)
                orderBy = "code";

            Func<IExpensePair, object> predicate = (x) => orderBy == "code" ? x.Code : x.Name;
            if(orderDesc != null)
            values = orderDesc.Value ? values.OrderByDescending(predicate) : values.OrderBy(predicate);

            return values.ToList().AsReadOnly();
        }
        public IEnumerable<string> FetchEnumKeys(bool? orderDesc)
        {
            var keys = EnumKeyPairs.Keys;
            if (orderDesc == null)
                return keys;
            if (orderDesc.Value)
                return keys.OrderByDescending(x => x);
            return keys.OrderBy(x => x);
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
