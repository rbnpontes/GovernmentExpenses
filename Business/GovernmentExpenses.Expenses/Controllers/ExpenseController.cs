using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace GovernmentExpenses.Expenses.Controllers
{
    [ApiController]
    [Route("api/expenses")]
    public class ExpenseController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Its working Expense Controller";
        }
    }
}
