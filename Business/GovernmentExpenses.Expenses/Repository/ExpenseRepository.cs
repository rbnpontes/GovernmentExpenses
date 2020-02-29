using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
// If use firebase, use Firebase Repository instead local.
#if SPREADSHEET
using InternalRepository = GovernmentExpenses.Expenses.Repository.SpreadsheetRepository;
#else
using InternalRepository = GovernmentExpenses.Expenses.Repository.LocalRepository;
#endif
namespace GovernmentExpenses.Expenses.Repository
{
    internal class ExpenseRepository : InternalRepository
    {
        public ExpenseRepository(ILogger logger) : base(logger)
        {
        }
    }
}
