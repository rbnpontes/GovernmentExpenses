using GovernmentExpenses.Expenses.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace GovernmentExpenses.Expenses.Controllers
{
    [ApiController]
    [Route("api/expenses")]
    public class ExpenseController : ControllerBase
    {
        public ExpenseController(ILogger logger)
        {
        }
        [HttpGet]
        public string Get()
        {
            return "Its working Expense Controller";
        }
        [HttpGet("categories")]
        public IReadOnlyList<ExpensePair> GetCategories()
        {
            List<ExpensePair> result = new List<ExpensePair>();
            result.Add(new ExpensePair(0, "Hello"));
            return result;
        }
    }
}
