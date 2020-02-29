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
        IEnumerable<string> FetchExpensesOrderKeys(bool? orderDesc);
        IEnumerable<string> FetchExpensesKeys(bool? orderDesc);
        IReadOnlyList<ExpenseDTO> FetchAll(int page = 0, int pageSize = 10, string orderBy = null, bool? orderDesc = null);
        IDictionary<string, IReadOnlyList<ExpenseDTO>> FetchExpensesGroupByProp(string prop, int page = 0, int pageSize = 10, string orderBy = null, bool? orderDesc = null);
        IReadOnlyList<IExpensePair> FetchEnum(string prop, string orderBy = null, bool? orderDesc = null);
        IEnumerable<string> FetchEnumKeys(bool? orderDesc);
        int ExpensesCount { get; }
    }
    internal partial class ExpenseService : IExpenseService
    {
        private IEnumerable<Expense> TryOrderList(IEnumerable<Expense> list, string orderBy = null, bool? orderDesc = null)
        {
            if (string.IsNullOrEmpty(orderBy) || !OrderKeyPairs.ContainsKey(orderBy))
                orderDesc = orderDesc ?? false;
            else if (orderDesc != null)
                orderBy = "id";
            return list.Order(orderDesc, OrderKeyPairs[orderBy]);
        }
        public int ExpensesCount { get => Repository.Count; }
        public IReadOnlyList<ExpenseDTO> FetchAll(int page = 0, int pageSize = 10, string orderBy = null, bool? orderDesc = null)
        {
            var values = TryOrderList(Repository.All(), orderBy, orderDesc).Page(page, pageSize);
            return values.Select(x => new ExpenseDTO(x)).ToList().AsReadOnly();
        }
        public IEnumerable<string> FetchExpensesOrderKeys(bool? orderDesc = null)
        {
            return OrderKeyPairs.Keys.Order(orderDesc, x => x);
        }
        public IEnumerable<string> FetchExpensesKeys(bool? orderDesc = null)
        {
            return ExpensesKeyPairs.Keys.Order(orderDesc, x => x);
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
            return values.Order(orderDesc, predicate).ToList().AsReadOnly();
        }
        public IEnumerable<string> FetchEnumKeys(bool? orderDesc)
        {
            return EnumKeyPairs.Keys.Order(orderDesc, x => x);
        }
        public IDictionary<string, IReadOnlyList<ExpenseDTO>> FetchExpensesGroupByProp(string prop, int page = 0, int pageSize = 10, string orderBy = null, bool? orderDesc = null)
        {
            throw new NotImplementedException();
        }
        public IExpenseResult FetchTotalExpenses()
        {
            float totalCommited = 0;
            float totalSettled = 0;
            float totalPayed = 0;
            foreach (var x in Repository.All())
            {
                totalCommited += Utils.ParseCurrency(x.ValorEmpenhado);
                totalSettled += Utils.ParseCurrency(x.ValorLiquidado);
                totalPayed += Utils.ParseCurrency(x.ValorPago);
            }
            return new ExpenseResult
            {
                TotalCommited = totalCommited,
                TotalSettled = totalSettled,
                TotalPayed = totalPayed
            };
        }
        public IDictionary<string, IExpenseResult> FetchTotalExpensesByProp(string prop)
        {
            Dictionary<string, IExpenseResult> result = new Dictionary<string, IExpenseResult>();
            if (!ExpensesKeyPairs.ContainsKey(prop))
                return result;
            var enums = FetchEnum(prop);
            var codes = enums.Select(x => x.Code);
            float totalCommited = 0;
            float totalSettled = 0;
            float totalPayed = 0;
            ExpenseResult expenseResult = null;
            var expenses = Repository.Where(ExpensesKeyPairs[prop](codes));
            foreach (var x in expenses)
            {
                var expPair = EnumKeyPairs[prop](x);
                totalCommited = Utils.ParseCurrency(x.ValorEmpenhado);
                totalSettled = Utils.ParseCurrency(x.ValorLiquidado);
                totalPayed = Utils.ParseCurrency(x.ValorPago);
                if (!result.ContainsKey(expPair.Name))
                {
                    result[expPair.Name] = new ExpenseResult
                    {
                        TotalCommited = totalCommited,
                        TotalSettled = totalSettled,
                        TotalPayed = totalPayed
                    };
                }
                else
                {
                    expenseResult = result[expPair.Name] as ExpenseResult;
                    expenseResult.TotalCommited += totalCommited;
                    expenseResult.TotalSettled += totalSettled;
                    expenseResult.TotalPayed += totalPayed;
                }

            }
            return result;
        }

    }
}
