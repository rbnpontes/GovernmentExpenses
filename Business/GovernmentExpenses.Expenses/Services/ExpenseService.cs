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
        public IEnumerable<ExpenseDTO> FetchAll(string orderBy = null, bool? orderDesc = null)
        {
            return FetchSearch(null, null, orderBy, orderDesc);
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
        public IDictionary<string, IExpenseGroup> FetchExpensesGroupByProp(string prop)
        {
            Dictionary<string, IExpenseGroup> result = new Dictionary<string, IExpenseGroup>();
            if (!EnumKeyPairs.ContainsKey(prop))
                return result;
            foreach (var expense in Repository.All())
            {
                var pair = EnumKeyPairs[prop](expense);
                if (result.ContainsKey(pair.Name))
                    (result[pair.Name] as ExpenseGroup).ItemsCount++;
                else
                    result[pair.Name] = new ExpenseGroup { ItemsCount = 1, Id = pair.Code };
            }
            return result;
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
        /// <summary>
        /// Try Search a Expense by a specific string
        /// </summary>
        /// <param name="criteria">Search String</param>
        /// <param name="prop">Property that been search</param>
        /// <returns>Returns a Delegate for inkove on a LINQ Queries</returns>
        private Func<Expense, bool> TrySearch(string criteria, string prop)
        {
            // Prevent unhandled strings like: ç, é, ã, í, é, ...
            criteria = criteria.RemoveDiacritics();
            return (x) =>
            {
                // If prop is null or empty, search for all possible members
                if (string.IsNullOrEmpty(prop))
                {
                    // Store Enum Cases
                    var enumCases = new string[EnumKeyPairs.Count];
                    // Store All non-enum cases, like Currency values.
                    var otherCases = new string[]
                    {
                    x.EmpenhoAno.ToString(),
                    x.EmpenhoNumero.ToString(),
                    x.SubEmpenho.ToString(),
                    x.IndicadorSubEmpenho.ToLower(),
                    x.ValorEmpenhado,
                    x.ValorPago,
                    x.ValorLiquidado
                    };
                    var keys = EnumKeyPairs.Keys;
                    for (int i = 0; i < keys.Count; i++)
                        enumCases[i] = EnumKeyPairs[keys.ElementAt(i)](x).Name.ToLower().RemoveDiacritics();
                    // Check if criteria matches in cases
                    return enumCases.Contains(criteria) || otherCases.Contains(criteria);
                } else {
                    if (!EnumKeyPairs.ContainsKey(prop))
                        return true;
                    return EnumKeyPairs[prop](x).Name.ToLower().RemoveDiacritics().Contains(criteria);
                }
            };
        }
        public IEnumerable<ExpenseDTO> FetchSearch(string criteria, string prop = null, string orderBy = null, bool? orderDesc = null)
        {
            var values = string.IsNullOrEmpty(criteria) ? Repository.All() : Repository.Where(TrySearch(criteria, prop));

            return TryOrderList(values, orderBy, orderDesc).Select(x => new ExpenseDTO(x));
        }
        public IEnumerable<ExpenseDTO> FetchExpenseGroupItems(string prop, object[] groupCode, string orderBy = null, bool? orderDesc = null)
        {
            if (!ExpensesKeyPairs.ContainsKey(prop))
                return new List<ExpenseDTO>();
            return TryOrderList(Repository.Where(ExpensesKeyPairs[prop](groupCode)), orderBy, orderDesc).Select(x => new ExpenseDTO(x));
        }
        public IEnumerable<ExpenseDTO> FetchExpenseGroupItems(string prop, object groupId, string orderBy = null, bool? orderDesc = null)
        {
            return FetchExpenseGroupItems(prop, new object[] { groupId }, orderBy, orderDesc);
        }

        public ExpensePair<T> NullCoalescingForPair<T>(ExpensePair<T> first, ExpensePair<T> second)
        {
            if (second == null)
                return first;
            first.Code = second.Code ?? first.Code;
            first.Name = NullCoalescingForString(first.Name, second.Name);
            return first;
        }
        public string NullCoalescingForString(string first, string second)
        {
            return string.IsNullOrEmpty(second) ? first : second;
        }
        public ExpenseDTO TryEditExpense(int id, ExpenseForm form)
        {
            var expense = Repository.Where(x => x.Id == id).FirstOrDefault();
            if (expense == null)
                throw new KeyNotFoundException($"Not found Expense with this Id(${id}).");
            expense.AnoMovimentacao = form.AnoMovimentacao ?? expense.AnoMovimentacao;
            expense.MesMovimentacao = NullCoalescingForPair(expense.MesMovimentacao, form.MesMovimentacao);
            expense.Orgao = NullCoalescingForPair(expense.Orgao, form.Orgao);
            expense.Unidade = NullCoalescingForPair(expense.Unidade, form.Unidade);
            expense.GrupoDespesa = NullCoalescingForPair(expense.GrupoDespesa, form.GrupoDespesa);
            expense.ModalidadeAplicacao = NullCoalescingForPair(expense.ModalidadeAplicacao, form.ModalidadeAplicacao);
            expense.Elemento = NullCoalescingForPair(expense.Elemento, form.Elemento);
            expense.SubElemento = NullCoalescingForPair(expense.SubElemento, form.SubElemento);
            expense.Funcao = NullCoalescingForPair(expense.Funcao, form.Funcao);
            expense.SubFuncao = NullCoalescingForPair(expense.SubFuncao, form.SubFuncao);
            expense.Programa = NullCoalescingForPair(expense.Programa, form.Programa);
            expense.Acao = NullCoalescingForPair(expense.Acao, form.Acao);
            expense.FonteRecurso = NullCoalescingForPair(expense.FonteRecurso, form.FonteRecurso);
            expense.EmpenhoAno = form.EmpenhoAno ?? expense.EmpenhoAno;
            return new ExpenseDTO(expense);  
        }
    }
}
