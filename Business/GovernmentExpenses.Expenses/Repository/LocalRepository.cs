using GovernmentExpenses.Core;
using GovernmentExpenses.Expenses.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GovernmentExpenses.Expenses.Repository
{
    internal class LocalRepository : IRepository<Expense>
    {
        private ILogger logger_;
        private IList<InternalExpense> expenses_;
        private IList<InternalExpenseType> fields_;
        private readonly string[] MonthNames = {
            "January","February", "March",
            "April", "May", "June",
            "July", "August", "September",
            "October", "November", "December"
        };

        public int Count => expenses_.Count;

        public LocalRepository(ILogger logger)
        {
            logger_ = logger;
            Initialize();
        }
        // Read Database values and save at memory
        private InternalExpenseData ReadInternalData()
        {
            return Utils.DeserializeFile<InternalExpenseData>($"{AppDomain.CurrentDomain.BaseDirectory}Artifacts\\db.json");
        }
        private IList<InternalExpense> CreatingExpenses(InternalExpenseData data)
        {
            InternalExpense[] expenses = new InternalExpense[data.Records.Count];
            fields_ = data.Fields;
            Parallel.ForEach(data.Records, (item, state, idx) =>
            {
                // Set this new object to list of results
                expenses[idx] = (InternalExpense)ExpenseUtils.GetExpenseFromArray(item);
            });
            return expenses.ToList();
        }
        private void SaveExpenses()
        {
            InternalExpenseData data = new InternalExpenseData();
            data.Fields = fields_;
            data.Records = (new List<object>[expenses_.Count]);
            // Make same step again for recreating rows
            Parallel.ForEach(expenses_, (item, state, idx) =>
            {
                data.Records[(int)idx] = item.Data = ExpenseUtils.GetArrayFromExpense(item);
            });
            Utils.SerializeToFile($"{AppDomain.CurrentDomain.BaseDirectory}Artifacts\\db.json", data);
        }
        private void Initialize()
        {
            try
            {
                logger_.LogInformation($"Reading Database at: \"{AppDomain.CurrentDomain.BaseDirectory}Artifacts\\db.json\"");
                InternalExpenseData data = ReadInternalData();
                logger_.LogInformation("Creating Expenses Objects");
                expenses_ = CreatingExpenses(data);
                logger_.LogInformation("Repository Created with Success!");
            }catch(Exception e){
                logger_.LogError(e, "Error has ocurred at initialization of Repository");
                throw e; 
            }
        }
        public IEnumerable<Expense> All()
        {
            return expenses_.Cast<Expense>().ToList();
        }

        public void Commit()
        {
            logger_.LogInformation("Saving Database");
            SaveExpenses();
            logger_.LogInformation("Saved with Success!");
        }

        public void Insert(Expense item)
        {
            throw new NotImplementedException();
        }

        public void Remove(Expense item)
        {
            throw new NotImplementedException();
        }

        public void Update(Expense item)
        {
            // Does nothing in this case
        }

        public IEnumerable<Expense> Where(Func<Expense, bool> predicate)
        {
            return expenses_.Where(predicate).ToList();
        }

    }
}
