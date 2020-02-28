using GovernmentExpenses.Expenses.Entities;
using GovernmentExpenses.Expenses.Services;
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
        private readonly IExpenseService service_;
        public ExpenseController(IExpenseService service)
        {
            service_ = service;
        }
        [HttpGet]
        public string Get()
        {
            return "Its working Expense Controller";
        }
        [HttpGet("enums/{prop}")]
        public IReadOnlyList<IExpensePair> GetEnums(string prop)
        {
            return service_.FetchEnum(prop);
        }
    }
}
